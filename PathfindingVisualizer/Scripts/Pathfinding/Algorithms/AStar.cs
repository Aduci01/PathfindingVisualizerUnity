using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class AStar : MonoBehaviour, IPathfinder
    {
        int stepCount;
        int[,] manhattanDistance;
        public void FindPath(ICell[,] cells, ICell start, ICell target)
        {
            stepCount = 0;
            manhattanDistance = new int[cells.GetLength(0), cells.GetLength(1)];

            foreach (ICell c in cells)
            {
                c.SetParentCell(null);
                c.SetIsVisited(false);
                c.SetHelperNum(99999); //Minimum distance from start
                manhattanDistance[c.GetX(), c.GetY()] = 2 * Mathf.Abs(target.GetX() - c.GetX()) + Mathf.Abs(target.GetY() - c.GetY());

                if (c.GetCellType() == CellTools.CellType.EMPTY)
                    c.SetColor(Color.white, true);

            }

            start.SetHelperNum(0);

            StartCoroutine(DelayedPathing(cells, start, target));
        }
        IEnumerator DelayedPathing(ICell[,] cells, ICell start, ICell target)
        {
            List<ICell> list = new List<ICell>();
            list.Add(start);

            while (list.Count != 0)
            {
                GameManager._Instance.ChangeStepCount(++stepCount);

                ICell c = GetCellWithSmallestF(list);
                c.SetIsVisited(true);

                list.Remove(c);

                if (c != start && c != target)
                    PathfindingTools.SetCellColorByDistance(c, c.GetHelperNum());

                if (c == target) break;

                List<ICell> neighbours = PathfindingTools.GetVisitedNeighbours(c, cells, false);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    ICell neighbour = neighbours[i];

                    if (neighbour.GetCellType() == CellTools.CellType.WALL) continue;

                    neighbour.SetIsVisited(true);
                    neighbour.SetParentCell(c);
                    neighbour.SetHelperNum(c.GetHelperNum() + 1); //helperNum measures the distance from start
                    list.Add(neighbour);
                }

                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }


            yield return StartCoroutine(PathfindingTools.HighlightPathWithParents(target, start));

            GameManager._Instance.SetShortestPath(PathfindingTools.GetPath(target, start).Count);
            GameManager._Instance.SetIsInteractable(true);
        }

        /**
         * Returns the cell with the lowest ManhattanDistance + DistanceFromStart
         *
         * @return
         */
        private ICell GetCellWithSmallestF(List<ICell> cells)
        {
            int min = 999999;
            ICell minCell = null;

            foreach (ICell c in cells)
            {
                int m = manhattanDistance[c.GetX(), c.GetY()];
                if (c.GetHelperNum() + m < min)
                {
                    min = c.GetHelperNum() + m;
                    minCell = c;
                }
            }

            return minCell;
        }

        public string GetName()
        {
            return "A*";
        }

    }
}