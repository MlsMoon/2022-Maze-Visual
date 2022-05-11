using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class MazeVisual
{
    
    private IEnumerator GenerateByDfs()
    {
        IEnumerator GO(int x ,int y)
        {
            Debug.Log("=================");
            Debug.Log("进入："+x+","+y);
            if (!_data.InArea(x,y))
            {
                throw new UnityException("不在范围内");
            }
            _data.visted[x, y] = true;
            //查看四个方向，如果是没访问过的就go一下
            List<int> randomNum = new List<int>()
            {
                0, 1, 2, 3
            };
            // //打乱顺序
            // for (int i = 0; i < 4; i++)
            // {
            //     int a = Random.Range(0, 4);
            //     int b = Random.Range(0, 4);
            //     var temp = randomNum[a];
            //     randomNum[a] = randomNum[b];
            //     randomNum[b] = temp;
            // }
            
            for (int t = 0; t < 4; t++)
            {
                int i = randomNum[t];
                int newX = x + _direction[i, 0] * 2;
                int newY = y + _direction[i, 1] * 2;
                Debug.Log("==坐标："+x+","+y);
                Debug.Log("=====探测坐标："+newX+","+newY);
                if (_data.InArea(newX,newY) && !_data.visted[newX,newY])
                {
                    //这个位置没去过，把墙变成路
                    _data.maze[x + _direction[i, 0], y + _direction[i, 1]] = MazeData.Road;
                    yield return new WaitForSeconds(0.1f);
                    yield return StartCoroutine(GO(newX, newY));
                }
            }
            yield return null;
        }

        int entranceX = _data.GETEntranceX();
        int entranceY = _data.GETEntranceY();
        yield return StartCoroutine(GO(entranceX, entranceY + 1));
        
        yield return null;
    }
}