using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class MazeVisual
{
    private IEnumerator SolveDfsNonRecursive()
    {
        yield return StartCoroutine(GenerateRandom(true));
        //将访问清空
        _data.ClearVisited();   
        
        Stack<MazePos> stack = new Stack<MazePos>();
        MazePos enter = new MazePos(_data.GETEntranceX(), _data.GETEntranceY());
        stack.Push(enter);
        _data.Visited[enter.X, enter.Y] = true;
        
        while (stack.Count != 0)
        {
            MazePos cur = stack.Pop();
            SetPathData(cur.X,cur.Y,true);
            yield return new WaitForSeconds(0.05f);

            //判断是否达到终点
            if (cur.X == _data.GETExitX() && cur.Y == _data.GETExitY())
            {
                MazePos target = cur;
                while (target != null)
                {
                    _data.TruePath[target.X, target.Y] = true;
                    target = target.PrePos;
                }
                break;
            }

            //遍历4个方向
            for (int i = 0; i < 4; i++)
            {
                int newX = cur.X + _direction[i,0];
                int newY = cur.Y + _direction[i,1];
                if (_data.InArea(newX,newY))
                {
                    if (!_data.Visited[newX,newY] && _data.maze[newX,newY] == MazeData.Road)
                    {
                        stack.Push(new MazePos(newX,newY,cur));
                        _data.Visited[newX, newY] = true;
                    }
                }
            }
        }
        
        
        
        
        yield return null;
    }
}