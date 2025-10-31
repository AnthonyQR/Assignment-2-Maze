using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface _mazeNavMeshSurface;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _enemyPrefab;

    [Header("Maze")]
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField] 
    private int _mazeDepth;

    private MazeCell[,] _mazeGrid;

    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid [x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);
        Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Spawn enemy at random position in the maze, other than where the player starts.
        int enemyX = Random.Range(1, _mazeWidth);
        int enemyZ = Random.Range(1, _mazeDepth);
        Instantiate(_enemyPrefab, new Vector3(enemyX, 0, enemyZ), Quaternion.identity);

        // Rebuild NavMesh after maze generation.
        _mazeNavMeshSurface.BuildNavMesh();
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);
            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        }
        while (nextCell != null);  
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        else if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        else if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        else if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        List<MazeCell> unvisitedCells = GetUnivisitedCells(currentCell);
        if (unvisitedCells.Count > 0)
        {
            int randomDirection = Random.Range(0, unvisitedCells.Count);
            return unvisitedCells[randomDirection];
        }

        else
        {
            return null;
        }
    }

    private List<MazeCell> GetUnivisitedCells(MazeCell currentCell)
    {
        List<MazeCell> unvisitedCells = new List<MazeCell>();
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazeWidth)
        {
            MazeCell cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.IsVisited == false)
            {
                unvisitedCells.Add(cellToRight);
            }
        }

        if (x - 1 >= 0)
        {
            MazeCell cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                unvisitedCells.Add(cellToLeft);
            }
        }

        if (z + 1 < _mazeDepth)
        {
            MazeCell cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                unvisitedCells.Add(cellToFront);
            }
        }

        if (z - 1 >= 0)
        {
            MazeCell cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                unvisitedCells.Add(cellToBack);
            }
        }

        return unvisitedCells;
    }
}
