
public class MazePos
{
    public int X;
    public int Y;
    public MazePos PrePos;

    public MazePos(int a, int b)
    {
        X = a;
        Y = b;
    }
    
    public MazePos(int a, int b,MazePos prev)
    {
        X = a;
        Y = b;
        PrePos = prev;
    }
}
