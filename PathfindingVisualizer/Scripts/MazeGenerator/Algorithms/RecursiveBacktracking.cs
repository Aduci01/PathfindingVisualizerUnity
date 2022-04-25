using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class RecursiveBacktracking : MonoBehaviour, IMazeGenerator
    {
        public void Generate(ICell[,] cells)
        {
            MazeTools.SetAllCellTypes(CellTools.CellType.WALL, cells);

            ICell startCell = cells[Random.Range(0, cells.GetLength(0)), Random.Range(0, cells.GetLength(1))];
            startCell.SetCellType(CellTools.CellType.EMPTY, false);

            List<ICell> visitedCells = new List<ICell>();
            visitedCells.Add(startCell);

            StartCoroutine(StartGeneration(cells, visitedCells, startCell));
        }

        IEnumerator StartGeneration(ICell[,] cells, List<ICell> visitedCells, ICell currentCell)
        {
            yield return DoMaze(cells, visitedCells, currentCell);

            GameManager._Instance.SetIsInteractable(true);
        }


        IEnumerator DoMaze(ICell[,] cells, List<ICell> visitedCells, ICell currentCell)
        {
            int x = currentCell.GetX();
            int y = currentCell.GetY();

            int[] a = { 0, 1, 0, -1 };
            int[] b = { 1, 0, -1, 0 };

            int[] intArray = { 0, 1, 2, 3 };
            List<int> intList = new List<int>(intArray);
            intList = intList.OrderBy(item => Random.Range(0, 1000f)).ToList();

            for (int i = 0; i < 4; i++)
            {
                int nx = a[intList[i]];
                int ny = b[intList[i]];


                if (nx + ny > 0)
                {
                    if (y + ny * 2 < cells.GetLength(1) && x + nx * 2 < cells.GetLength(0) && !visitedCells.Contains(cells[x + nx, y + ny]) && !visitedCells.Contains(cells[x + nx * 2, y + ny * 2]))
                    {
                        HandleCarving(cells[x + nx, y + ny], cells[x + nx * 2, y + ny * 2], visitedCells);

                        if (GameManager.delay != 0)
                            yield return new WaitForSeconds(GameManager.delay);

                        yield return StartCoroutine(DoMaze(cells, visitedCells, cells[x + nx * 2, y + ny * 2]));
                    }
                }
                else
                {
                    if (y + ny * 2 >= 0 && x + nx * 2 >= 0 && !visitedCells.Contains(cells[x + nx, y + ny]) && !visitedCells.Contains(cells[x + nx * 2, y + ny * 2]))
                    {
                        HandleCarving(cells[x + nx, y + ny], cells[x + nx * 2, y + ny * 2], visitedCells);

                        if (GameManager.delay != 0)
                            yield return new WaitForSeconds(GameManager.delay);

                        yield return StartCoroutine(DoMaze(cells, visitedCells, cells[x + nx * 2, y + ny * 2]));
                    }
                }
            }
        }

        public static void HandleCarving(ICell c1, ICell c2, List<ICell> visitedCells)
        {
            c1.SetCellType(CellTools.CellType.EMPTY, false);
            c2.SetCellType(CellTools.CellType.EMPTY, false);

            visitedCells.Add(c1);
            visitedCells.Add(c2);
        }

        public string GetName()
        {
            return "Recursive Backtracking";
        }
    }
}