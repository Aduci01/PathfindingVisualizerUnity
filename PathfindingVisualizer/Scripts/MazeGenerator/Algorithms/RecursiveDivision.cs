using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class RecursiveDivision : MonoBehaviour, IMazeGenerator
    {
        bool yieldBool;

        public void Generate(ICell[,] cells)
        {
            foreach (ICell c in cells)
            {
                c.SetCellType(CellTools.CellType.EMPTY, true);

                if (c.GetX() == 0 || c.GetY() == 0 || c.GetX() == cells.GetLength(0) - 1 || c.GetY() == cells.GetLength(1) - 1)
                    c.SetCellType(CellTools.CellType.WALL, true);
            }

            StartCoroutine(StartPathing(0, cells.GetLength(0) - 1, 0, cells.GetLength(1) - 1, cells));
        }

        IEnumerator StartPathing(int lowerBoundaryX, int upperBoundaryX, int lowerBoundaryY, int upperBoundaryY, ICell[,] cells)
        {
            yield return RecursivePathing(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells);

            GameManager._Instance.SetIsInteractable(true);
        }

        IEnumerator RecursivePathing(int lowerBoundaryX, int upperBoundaryX, int lowerBoundaryY, int upperBoundaryY, ICell[,] cells)
        {
            if (lowerBoundaryX >= upperBoundaryX - 2 || lowerBoundaryY >= upperBoundaryY - 2) yield break;

            if (GameManager.delay != 0)
                yield return new WaitForSeconds(GameManager.delay);

            if (Random.Range(0, 2) == 0)
            {
                yield return StartCoroutine(Vertical(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells));

                if (!yieldBool)
                {
                    Horizontal(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells);
                }
            }
            else
            {
                yield return StartCoroutine(Horizontal(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells));

                if (!yieldBool)
                {
                    Vertical(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells);
                }

            }
        }

        IEnumerator Vertical(int lowerBoundaryX, int upperBoundaryX, int lowerBoundaryY, int upperBoundaryY, ICell[,] cells)
        {
            if (upperBoundaryX - lowerBoundaryX - 3 <= 0)
            {
                yieldBool = false;
                yield break;
            }
            else
            {
                int idx = Random.Range(0, upperBoundaryX - lowerBoundaryX - 3) + lowerBoundaryX + 2;
                int wallSpaceidx = Random.Range(0, upperBoundaryY - lowerBoundaryY - 1) + lowerBoundaryY + 1;

                for (int i = lowerBoundaryY + 1; i < upperBoundaryY; i++)
                {
                    cells[idx, i].SetCellType(CellTools.CellType.WALL, false);
                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);
                }

                cells[idx, wallSpaceidx].SetCellType(CellTools.CellType.EMPTY, false);

                yield return StartCoroutine(RecursivePathing(lowerBoundaryX, idx, lowerBoundaryY, upperBoundaryY, cells));
                yield return StartCoroutine(RecursivePathing(idx, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells));
            }

            yieldBool = true;
        }

        IEnumerator Horizontal(int lowerBoundaryX, int upperBoundaryX, int lowerBoundaryY, int upperBoundaryY, ICell[,] cells)
        {
            if (upperBoundaryY - lowerBoundaryY - 3 <= 0)
            {
                yieldBool = false;
                yield break;
            }

            int idx = Random.Range(0, upperBoundaryY - lowerBoundaryY - 3) + lowerBoundaryY + 2;
            int wallSpaceidx = Random.Range(0, upperBoundaryX - lowerBoundaryX - 1) + lowerBoundaryX + 1;

            for (int i = lowerBoundaryX + 1; i < upperBoundaryX; i++)
            {
                cells[i, idx].SetCellType(CellTools.CellType.WALL, false);
                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }

            cells[wallSpaceidx, idx].SetCellType(CellTools.CellType.EMPTY, false);

            yield return StartCoroutine(RecursivePathing(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, idx, cells));
            yield return StartCoroutine(RecursivePathing(lowerBoundaryX, upperBoundaryX, idx, upperBoundaryY, cells));

            yieldBool = true;
        }

        public string GetName()
        {
            return "Recursive Division";
        }

    }
}