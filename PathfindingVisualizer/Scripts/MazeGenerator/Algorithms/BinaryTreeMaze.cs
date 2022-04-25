using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class BinaryTreeMaze : MonoBehaviour, IMazeGenerator
    {
        public void Generate(ICell[,] cells)
        {
            MazeTools.SetAllCellTypes(CellTools.CellType.WALL, cells);

            StartCoroutine(GenerationWithDelay(cells));
        }

        IEnumerator GenerationWithDelay(ICell[,] cells)
        {
            for (int i = 0; i < cells.GetLength(0); i += 2)
            {
                for (int j = 0; j < cells.GetLength(1); j += 2)
                {
                    ICell c = cells[i, j];
                    c.SetCellType(CellTools.CellType.EMPTY, false);

                    List<ICell> emptyNeighbours = new List<ICell>();

                    if (i != 0 && cells[i - 2, j].GetCellType() == CellTools.CellType.EMPTY)
                        emptyNeighbours.Add(cells[i - 1, j]);

                    if (j != 0 && cells[i, j - 2].GetCellType() == CellTools.CellType.EMPTY)
                        emptyNeighbours.Add(cells[i, j - 1]);

                    if (emptyNeighbours.Count != 0)
                    {
                        emptyNeighbours[Random.Range(0, emptyNeighbours.Count)].SetCellType(CellTools.CellType.EMPTY, false);
                    }

                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);
                }
            }

            GameManager._Instance.SetIsInteractable(true);
        }

        public string GetName()
        {
            return "Binary Tree";
        }
    }
}