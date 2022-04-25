using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class MazeTools
    {
        public static void SetAllCellTypes(CellTools.CellType type, ICell[,] cells)
        {
            foreach (ICell ic in cells)
            {
                ic.SetCellType(type, true);
            }
        }

        public static List<ICell> GetNeighbours(ICell c, ICell[,] cells)
        {
            List<ICell> neighbours = new List<ICell>();
            int x = c.GetX(), y = c.GetY();

            if (x >= 2)
            {
                neighbours.Add(cells[x - 2, y]);
            }

            if (x < cells.GetLength(0) - 2)
            {
                neighbours.Add(cells[x + 2, y]);
            }

            if (y >= 2)
            {
                neighbours.Add(cells[x, y - 2]);
            }

            if (y < cells.GetLength(1) - 2)
            {
                neighbours.Add(cells[x, y + 2]);
            }

            return neighbours;
        }

        public static List<ICell> GetNeighboursWithType(ICell c, ICell[,] cells, CellTools.CellType type)
        {
            List<ICell> neighbours = new List<ICell>();
            int x = c.GetX(), y = c.GetY();

            if (x >= 2)
            {
                ICell cell = cells[x - 2, y];
                if (cell.GetCellType() == type)
                    neighbours.Add(cell);
            }

            if (x < cells.GetLength(0) - 2)
            {
                ICell cell = cells[x + 2, y];
                if (cell.GetCellType() == type)
                    neighbours.Add(cell);
            }

            if (y >= 2)
            {
                ICell cell = cells[x, y - 2];
                if (cell.GetCellType() == type)
                    neighbours.Add(cell);
            }

            if (y < cells.GetLength(1) - 2)
            {
                ICell cell = cells[x, y + 2];
                if (cell.GetCellType() == type)
                    neighbours.Add(cell);
            }

            return neighbours;
        }
    }
}