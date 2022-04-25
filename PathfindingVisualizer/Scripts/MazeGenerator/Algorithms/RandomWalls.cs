using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class RandomWalls : MonoBehaviour, IMazeGenerator
    {
        public void Generate(ICell[,] cells)
        {
            MazeTools.SetAllCellTypes(CellTools.CellType.EMPTY, cells);

            foreach (ICell ic in cells)
            {
                if (Random.Range(0, 100) < 40)
                {
                    ic.SetCellType(CellTools.CellType.WALL, false);
                }
            }

            GameManager._Instance.SetIsInteractable(true);
        }

        public string GetName()
        {
            return "Random Walls";
        }
    }
}