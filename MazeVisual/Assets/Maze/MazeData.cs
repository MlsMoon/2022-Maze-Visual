using UnityEngine;

public class MazeData
{
    public static char Road = ' ';
    public static char Wall = '#';
    
    private int N, M;
    public char[,] maze;

    private int entranceX, entranceY;
    private int exitX, exitY;
    
    public MazeData(int N, int M){

        if( N%2 == 0 || M%2 == 0)
            throw new UnityException("Our Maze Generalization Algorihtm requires the width and height of the maze are odd numbers");

        this.N = N;
        this.M = M;

        maze = new char[N,M];
        for(int i = 0 ; i < N ; i ++)
        for(int j = 0 ; j < M ; j ++)
            if(i%2 == 1 && j%2 == 1)
                maze[i,j] = Road;
            else
                maze[i,j] = Wall;

        entranceX = 1;
        entranceY = 0;
        exitX = N - 2;
        exitY = M - 1;

        maze[entranceX,entranceY] = Road;
        maze[exitX,exitY] = Road;
    }
    
    public int GetN(){return N;}
    public int GetM(){return M;}
    public int GETEntranceX(){return entranceX;}
    public int GETEntranceY(){return entranceY;}
    public int GETExitX(){return exitX;}
    public int GETExitY(){return exitY;}
}
