using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MazeVisual
{
    private IEnumerator GenerateByBfs()
    {
        LinkedList<MazePos> queue = new LinkedList<MazePos>();
        var first = new MazePos(
            _data.GETEntranceX(),
            _data.GETEntranceY() + 1);
        queue.AddLast(first);
        _data.Visited[first.X, first.Y] = true;

        while (queue.Count != 0)
        {
            MazePos cur = queue.First.Value;
            queue.RemoveFirst();
            for(int i = 0 ; i < 4  ; i ++)
            {
                int newX = cur.X + _direction[i,0]*2;
                int newY = cur.Y + _direction[i,1]*2;

                if(_data.InArea(newX, newY)
                   && !_data.Visited[newX,newY]
                   && _data.maze[newX,newY] == MazeData.Road)
                {
                    queue.AddLast(new MazePos(newX, newY));
                    _data.Visited[newX,newY] = true;
                    _data.maze[cur.X + _direction[i, 0], cur.Y + _direction[i, 1]] 
                        = MazeData.Road;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        
        
        yield return null;
    }
}