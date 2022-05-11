using UnityEngine;

public class MazeData
{
    public static char Road = ' ';
    public static char Wall = '#';
    

    public char[,] maze;
    public bool[,] visted;
    
    private int N, M;
    private int entranceX, entranceY;
    private int exitX, exitY;
    
    public MazeData(int N, int M){

        if( N%2 == 0 || M%2 == 0)
            throw new UnityException("Our Maze Generalization Algorihtm requires the width and height of the maze are odd numbers");

        this.N = N;
        this.M = M;

        maze = new char[N,M];
        visted = new bool[N, M];
        
        //默认迷宫状态
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if(i%2 == 1 && j%2 == 1)
                    maze[i,j] = Road;
                else
                    maze[i,j] = Wall;
                
                visted[i, j] = false;
            }
        }


        entranceX = 1;
        entranceY = 0;
        exitX = N - 2;
        exitY = M - 1;

        maze[entranceX,entranceY] = Road;
        maze[exitX,exitY] = Road;
    }

    public object Visited { get; set; }

    public int GetN(){return N;}
    public int GetM(){return M;}
    public int GETEntranceX(){return entranceX;}
    public int GETEntranceY(){return entranceY;}
    public int GETExitX(){return exitX;}
    public int GETExitY(){return exitY;}
    public bool InArea(int x, int y){
        return x >= 0 && x < N && y >= 0 && y < M;
    }
}
