using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MazeVisual : MonoBehaviour
{
    private MazeData _data;
    private Image[,] _dataVisualObjects;
    
    private readonly int[] _canvasSize = new int[2]
    {
        1600,
        1600
    };
    private readonly Color[] _colors = new Color[4]
    {
        new Color(1,1,1,1),
        new Color(0,0.5f,1,1),
        new Color(0.9f,0.9f,0,1),
        new Color(0.8f,0.3f,0.1f,1)
    };
    private readonly int[,] _direction = new int[4,2]
    {
        {-1, 0},
        {0, 1},
        {1, 0},
        {0, -1},
    };

    /// <summary>
    /// 每帧执行
    /// </summary>
    private void Update()
    {
        //Render Data
        for (int i = 0; i < _data.GetN(); i++)
        {
            for (int j = 0; j < _data.GetM(); j++)
            {
                if (_data.maze[i,j] == MazeData.Wall)
                    _dataVisualObjects[i, j].color = _colors[1];
                else
                    _dataVisualObjects[i, j].color = _colors[0];
                if (_data.path[i, j])
                    _dataVisualObjects[i, j].color = _colors[2];
                if (_data.TruePath[i,j])
                    _dataVisualObjects[i, j].color = _colors[3];
            }
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="size"></param>
    private void Initial(int size)
    {
        _data = new MazeData(size, size);
        _dataVisualObjects = new Image[size, size];
        //获得Canvas的Width和Height
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject data = new GameObject();
                data.transform.SetParent(this.transform);
                data.name = i.ToString() + "," + j.ToString();
                var img = data.AddComponent<Image>();
                RectTransform rect =(RectTransform) data.transform;
                rect.sizeDelta = new Vector2(
                    (float)_canvasSize[0]/size,
                    (float)_canvasSize[1]/size
                );
                rect.anchorMin = new Vector2(0f,1f);
                rect.anchorMax = new Vector2(0f,1f);
                rect.pivot = new Vector2(0f,1f);
                rect.anchoredPosition = new Vector2(
                    _canvasSize[0]/size *i,   
                    -_canvasSize[1]/size *j 
                );
                _dataVisualObjects[i, j] = img;
            }
        }
    }

    
    public void RunVisual(MazeType type,int size)
    {
        this.gameObject.SetActive(true);
        Initial(size);
        switch (type)
        {
            case MazeType.GDfs1:
                StartCoroutine(GenerateByDfs());
                break;
            case MazeType.GDfs2:
                StartCoroutine(GenerateByNonRecurDfs());
                break;
            case MazeType.GBfs:
                StartCoroutine(GenerateByBfs());
                break;
            case MazeType.GRandom:
                StartCoroutine(GenerateRandom());
                break;
            case MazeType.SDfs:
                StartCoroutine(SolveDfs());
                break;
            case MazeType.SDfs2:
                StartCoroutine(SolveDfsNonRecursive());
                break;
        }
    }

    private void SetPathData(int x, int y, bool isPath)
    {
        _data.path[x,y] = isPath;
    }
}
