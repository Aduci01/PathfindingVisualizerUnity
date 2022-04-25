using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class Prim : MonoBehaviour, IMazeGenerator
    {
        public void Generate(ICell[,] cells)
        {
            StartCoroutine(GenerationWithDelay(cells));
        }

        IEnumerator GenerationWithDelay(ICell[,] cells)
        {
            List<ICell> list = new List<ICell>();
            MazeTools.SetAllCellTypes(CellTools.CellType.EMPTY, cells);

            ICell startCell = cells[Random.Range(0, cells.GetLength(0)), Random.Range(0, cells.GetLength(1))];
            startCell.SetCellType(CellTools.CellType.WALL, false);

            list = MazeTools.GetNeighbours(startCell, cells);

            while (list.Count != 0)
            {
                ICell currentCell = list[Random.Range(0, list.Count)];
                list.Remove(currentCell);

                if (currentCell.GetCellType() == CellTools.CellType.WALL) continue;

                currentCell.SetCellType(CellTools.CellType.WALL, false);

                List<ICell> neighbours = MazeTools.GetNeighboursWithType(currentCell, cells, CellTools.CellType.WALL);

                if (neighbours.Count != 0)
                {
                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);

                    ICell c = neighbours[Random.Range(0, neighbours.Count)];
                    neighbours.Remove(c);

                    c.SetCellType(CellTools.CellType.WALL, false);
                    cells[c.GetX() - (c.GetX() - currentCell.GetX()) / 2, c.GetY() - (c.GetY() - currentCell.GetY()) / 2].SetCellType(CellTools.CellType.WALL, false);

                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);
                }

                List<ICell> emptyNeighbours = MazeTools.GetNeighboursWithType(currentCell, cells, CellTools.CellType.EMPTY);
                list.AddRange(emptyNeighbours);

                foreach (ICell c in emptyNeighbours)
                {
                    c.SetColor(Color.cyan, false);
                }

                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }

            GameManager._Instance.SetIsInteractable(true);
        }

        public string GetName()
        {
            return "Prim";
        }
    }
}