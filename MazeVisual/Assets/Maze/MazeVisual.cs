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
    private readonly Color[] _colors = new Color[2]
    {
        new Color(1,1,1,1),
        new Color(0,0.5f,1,1),
    };
    private readonly int[,] _direction = new int[4,2]
    {
        {-1, 0},
        {0, 1},
        {1, 0},
        {0, -1},
    };

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
            }
        }
    }

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
        }
    }
}
