using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class CellTools
    {
        public static bool IsNeighbouring(ICell cell1, ICell cell2)
        {
            int x = Mathf.Abs(cell1.GetX() - cell2.GetX());
            int y = Mathf.Abs(cell1.GetY() - cell2.GetY());

            if (x <= 1 && y <= 1 && (x == 0 || y == 0))
                return true;

            return false;
        }

        public enum CellType { EMPTY, WALL, TARGET, START }
    }

    [System.Serializable]
    public class CellTypeProps
    {
        [SerializeField]
        private Color cellColor;
        [SerializeField]
        private Sprite sprite;

        public Color GetColor() => cellColor;

        public Sprite GetSprite() => sprite;
    }
}