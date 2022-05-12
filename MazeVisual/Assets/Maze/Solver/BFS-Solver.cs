using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MazeVisual
{
    private IEnumerator SolveBfs()
    {
        yield return StartCoroutine(GenerateRandom(true));
        //将访问清空
        _data.ClearVisited();   
        
        LinkedList<MazePos> queue = new LinkedList<MazePos>();
        MazePos enter = new MazePos(_data.GETEntranceX(), _data.GETEntranceY());
        queue.AddLast(enter);
        _data.Visited[enter.X, enter.Y] = true;

        while (queue.Count != 0)
        {
            MazePos cur = queue.First.Value;
            queue.RemoveFirst();
            SetPathData(cur.X,cur.Y,true);
            yield return new WaitForSeconds(0.05f);
            
            //判断是否为出口
            if (cur.X == _data.GETExitX() && cur.Y == _data.GETExitY())
            {
                //find path
                MazePos target = cur;
                while (target != null)
                {
                    _data.TruePath[target.X, target.Y] = true;
                    target = target.PrePos;
                }
                break;
            }
            
            //遍历四个方向
            for (int i = 0; i < 4; i++)
            {
                int newX = cur.X + _direction[i,0];
                int newY = cur.Y + _direction[i,1];

                if (_data.InArea(newX,newY) )
                {
                    if (!_data.Visited[newX,newY] && _data.maze[newX,newY] == MazeData.Road)
                    {
                        queue.AddLast(new MazePos(newX, newY, cur));
                        _data.Visited[newX, newY] = true;
                    }
                }
            }
            
        }
        
        yield return null;
    }
}