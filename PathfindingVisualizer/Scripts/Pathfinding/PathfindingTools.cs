using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class PathfindingTools : MonoBehaviour
    {
        private static int distancePerColor = 25;
        private static Color[] blendColors = { Color.magenta, new Color(138 / 255f, 43 / 255f, 226 / 255f), Color.cyan, Color.green, Color.yellow };

        public static List<ICell> GetVisitedNeighbours(ICell c, ICell[,] cells, bool isVisited)
        {
            List<ICell> neighbours = new List<ICell>();
            int x = c.GetX(), y = c.GetY();

            if (x >= 1)
            {
                ICell cell = cells[x - 1, y];
                if (cell.GetIsVisited() == isVisited)
                    neighbours.Add(cell);
            }

            if (x < cells.GetLength(0) - 1)
            {
                ICell cell = cells[x + 1, y];
                if (cell.GetIsVisited() == isVisited)
                    neighbours.Add(cell);
            }

            if (y >= 1)
            {
                ICell cell = cells[x, y - 1];
                if (cell.GetIsVisited() == isVisited)
                    neighbours.Add(cell);
            }

            if (y < cells.GetLength(1) - 1)
            {
                ICell cell = cells[x, y + 1];
                if (cell.GetIsVisited() == isVisited)
                    neighbours.Add(cell);
            }

            return neighbours;

        }

        public static void SetCellColorByDistance(ICell c, int dist)
        {
            int idx = (dist / distancePerColor) % blendColors.Length;
            int idx2 = (idx + 1) % blendColors.Length;

            Color color = Color.Lerp(blendColors[idx], blendColors[idx2], (dist % distancePerColor) / (float)distancePerColor);
            c.SetColor(color, false);
        }

        public static IEnumerator HighlightPathWithParents(ICell c, ICell start)
        {
            while (c.GetParentCell() != null)
            {
                c = c.GetParentCell();

                if (c != start)
                    c.SetColor(new Color(255 / 255f, 165 / 255f, 0 / 255f), false);

                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }
        }

        public static List<ICell> GetPath(ICell target, ICell start)
        {
            List<ICell> cells = new List<ICell>();

            while (target.GetParentCell() != null)
            {
                cells.Add(target);
                target = target.GetParentCell();
            }

            return cells;
        }

        public static Color[] GetColors() => blendColors;

        public static void SetColors(Color[] c)
        {
            blendColors = c;
        }
    }
}