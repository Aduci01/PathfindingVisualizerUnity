using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class BFS : MonoBehaviour, IPathfinder
    {
        int stepCount;

        public void FindPath(ICell[,] cells, ICell start, ICell target)
        {
            stepCount = 0;

            StartCoroutine(DelayedPathing(cells, start, target));
        }

        IEnumerator DelayedPathing(ICell[,] cells, ICell start, ICell target)
        {
            foreach (ICell c in cells)
            {
                c.SetParentCell(null);
                c.SetIsVisited(false);
                c.SetHelperNum(0);

                if (c.GetCellType() == CellTools.CellType.EMPTY)
                {
                    c.SetColor(Color.white, true);
                }
            }

            Queue<ICell> queue = new Queue<ICell>();
            queue.Enqueue(start);

            int currentDistance = 0;
            while (queue.Count != 0)
            {
                ICell c = queue.Dequeue();
                c.SetIsVisited(true);

                GameManager._Instance.ChangeStepCount(++stepCount);

                if (c == target) break;

                List<ICell> neighbours = PathfindingTools.GetVisitedNeighbours(c, cells, false);

                if (neighbours.Count == 0) continue;

                for (int i = 0; i < neighbours.Count; i++)
                {
                    ICell neighbour = neighbours[i];

                    if (neighbour.GetCellType() == CellTools.CellType.WALL) continue;

                    neighbour.SetIsVisited(true);
                    neighbour.SetParentCell(c);
                    neighbour.SetHelperNum(c.GetHelperNum() + 1); //helperNum measures the distance from start
                    queue.Enqueue(neighbour);

                    if (target != neighbour)
                        PathfindingTools.SetCellColorByDistance(neighbour, neighbour.GetHelperNum());
                }

                if (currentDistance < c.GetHelperNum())
                {
                    currentDistance++;

                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);
                }

            }

            yield return StartCoroutine(PathfindingTools.HighlightPathWithParents(target, start));

            GameManager._Instance.SetShortestPath(PathfindingTools.GetPath(target, start).Count);
            GameManager._Instance.SetIsInteractable(true);
        }

        public string GetName()
        {
            return "Breath-First Search";
        }
    }
}