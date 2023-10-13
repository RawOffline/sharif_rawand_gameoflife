using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    [Header ("Cell and Grid")]
    public GameObject cellPrefab;
    Cell[,] cells;
    float cellSize = 0.3f;
    int numberOfColums, numberOfRows;
    int spawnChancePercentage = 40;

    public InputManager InputManager;

    void Start()
    {
        QualitySettings.vSyncCount = 0;

        if (PlayerPrefs.GetInt("isGamePaused")  == 1)
            Application.targetFrameRate = 8;
        else
            Application.targetFrameRate = 60;

        numberOfColums = (int)Mathf.Floor((Camera.main.orthographicSize *
            Camera.main.aspect * 2) / cellSize);
        numberOfRows = (int)Mathf.Floor(Camera.main.orthographicSize *
            2 / cellSize);

        cells = new Cell[numberOfColums, numberOfRows];

        CreateCells();
    }

    void Update()
    {
        if (!InputManager.isGamePaused)
        {
            CountNeighbors();
            ControlPopulation();
        }

        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                cells[x, y].UpdateStatus();
            }
        }
    }

    void CreateCells()
    {
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize * Camera.main.aspect,
                    y * cellSize - Camera.main.orthographicSize);

                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells[x, y] = newCell.GetComponent<Cell>();

                if (!InputManager.isGamePaused)
                {
                    if (Random.Range(0, 100) < spawnChancePercentage)
                    {
                        cells[x, y].isAlive = true;
                    }
                }

                cells[x, y].UpdateStatus();
            }
        }
    }

    void CountNeighbors()
    {
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                int numNeighbors = 0;

                //Check each direction + Edge case
                if (y + 1 < numberOfRows)
                    if (cells[x, y + 1].isAlive)
                        numNeighbors++;

                if (x + 1 < numberOfColums && y + 1 < numberOfRows)
                    if (cells[x + 1, y + 1].isAlive)
                        numNeighbors++;

                if (x + 1 < numberOfColums)
                    if (cells[x + 1, y].isAlive)
                        numNeighbors++;

                if (x + 1 < numberOfColums && y - 1 >= 0)
                    if (cells[x + 1, y - 1].isAlive)
                        numNeighbors++;

                if (y - 1 >= 0)
                    if (cells[x, y - 1].isAlive)
                        numNeighbors++;

                if (x - 1 >= 0 && y - 1 >= 0)
                    if (cells[x - 1, y - 1].isAlive)
                        numNeighbors++;

                if (x - 1 >= 0)
                    if (cells[x - 1, y].isAlive)
                        numNeighbors++;

                if (x - 1 >= 0 && y + 1 < numberOfRows)
                    if (cells[x - 1, y + 1].isAlive)
                        numNeighbors++;

                cells[x, y].numNeighbors = numNeighbors;
            }
        }
    }

    void ControlPopulation()
    {
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                if (cells[x, y].isAlive)
                {
                    if (cells[x, y].numNeighbors != 2 && cells[x, y].numNeighbors != 3)
                    {
                        cells[x, y].isAlive = false;
                        cells[x, y].nextGenAlive = false;
                    }
                    else
                        cells[x, y].nextGenAlive = true;
                }
                else
                {
                    if (cells[x, y].numNeighbors == 3)
                        cells[x, y].isAlive = true;
                }
            }
        }
    }
}
