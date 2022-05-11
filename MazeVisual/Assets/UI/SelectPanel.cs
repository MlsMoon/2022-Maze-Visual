using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    public InputField InputField;
    public MazeVisual MazeVisual;

    public void EnterMaze(int mazeType)
    {
        try
        {
            int.Parse(InputField.text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        int size = int.Parse(InputField.text);
        Debug.Log(size);
        MazeType type = (MazeType) mazeType;
        MazeVisual.RunVisual(type,size);
        this.gameObject.SetActive(false);
    }
}
