using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MazeVisual
{
    private IEnumerator SolveDfs()
    {
        //一个字典，存放了Solve问题的结果
        Dictionary<float, bool> boolDict = new Dictionary<float, bool>();

        //定义一个Solve方法
        IEnumerator Solve(float key,int x ,int y)
        {
            
            if (!_data.InArea(x,y))
            {
                throw new UnityException("所查询值不在区域内");
            }

            _data.Visited[x, y] = true;
            SetPathData(x,y,true);
            yield return new WaitForSeconds(0.05f);
            
            //如果是出口将查询值设置为true,并结束协程
            if (x == _data.GETExitX() && y == _data.GETExitY())
            {
                boolDict.Add(key,true);
                yield return null;
            }
            else
            {
                bool checkResult = false;
                //查询4个方向
                for (int i = 0; i < 4; i++)
                {
                    if (checkResult == true)
                    {
                        break;
                    }
                    int newX = x + _direction[i,0];
                    int newY = y + _direction[i,1];
                    //先检查该方向点位是否符合继续查询条件
                    if (!_data.InArea(newX, newY))
                    {
                        
                    }
                    else
                    {
                        //继续查询
                        if (
                            _data.maze[newX,newY] == MazeData.Road //是否是道路
                            &&_data.Visited[newX,newY]==false//没有访问过
                        )
                        {
                            //随机一个key
                            float varKey = Random.Range(0f, 1f);
                            yield return StartCoroutine(Solve(varKey, newX, newY));
                            //如果查询值为true ，则true否则false
                            boolDict.TryGetValue(varKey, out var res);
                            if (res)
                            {
                                boolDict.Add(key,true);
                                checkResult = true;
                            }
                        }
                    }
                    //如果四边都不行，返回false
                }
                if (!checkResult)
                {
                    boolDict.Add(key,false);
                    //并把isPath取消
                    SetPathData(x,y,false);
                }
            }
        }
        
        yield return StartCoroutine(GenerateRandom(true));
        //将访问清空
        _data.ClearVisited();   
        var resultKey = Random.Range(0f, 1f);
        yield return StartCoroutine(
            Solve(resultKey, _data.GETEntranceX(), _data.GETEntranceY())
            );
        
        yield return null;
    }
}