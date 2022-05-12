using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MazeVisual
{
    private IEnumerator GenerateByNonRecurDfs()
    {
        //stack遍历
        
        Stack<Vector2> stack = new Stack<Vector2>();
        var first = new Vector2(
            _data.GETEntranceX(),
            _data.GETEntranceY() + 1
        );
        stack.Push(first);
        _data.Visited[(int)first.x,(int)first.y] = true;
        
        while (stack.Count != 0)
        {
            Vector2 curPos = stack.Pop();
            for(int i = 0 ; i < 4  ; i ++)
            {
                int newX = (int)curPos.x + _direction[i,0]*2;
                int newY = (int)curPos.y + _direction[i,1]*2;

                if(_data.InArea(newX, newY) && ! _data.Visited[newX,newY]){
                    stack.Push(new Vector2(newX, newY));
                    _data.Visited[newX,newY] = true;
                    _data.maze[
                        (int) curPos.x + _direction[i, 0],
                        (int) curPos.y + _direction[i, 1]] = MazeData.Road;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        yield return null;
    }
}