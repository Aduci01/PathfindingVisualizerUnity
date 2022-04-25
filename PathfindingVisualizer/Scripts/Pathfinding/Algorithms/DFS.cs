using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class DFS : MonoBehaviour, IPathfinder
    {
        bool coroutineBool;

        int stepCount;

        public void FindPath(ICell[,] cells, ICell start, ICell target)
        {
            stepCount = 0;

            foreach (ICell c in cells)
            {
                c.SetParentCell(null);
                c.SetIsVisited(false);
                c.SetHelperNum(0);

                if (c.GetCellType() == CellTools.CellType.EMPTY)
                    c.SetColor(Color.white, true);

            }

            StartCoroutine(DelayedPathing(cells, start, target));
        }

        IEnumerator DelayedPathing(ICell[,] cells, ICell start, ICell target)
        {
            yield return (RecursiveDFS(cells, start, target));

            yield return (PathfindingTools.HighlightPathWithParents(target, start));

            GameManager._Instance.SetShortestPath(PathfindingTools.GetPath(target, start).Count);
            GameManager._Instance.SetIsInteractable(true);
        }

        IEnumerator RecursiveDFS(ICell[,] cells, ICell current, ICell target)
        {
            if (current == target)
            {
                coroutineBool = true;
                yield break;
            }

            current.SetIsVisited(true);
            PathfindingTools.SetCellColorByDistance(current, current.GetHelperNum());

            GameManager._Instance.ChangeStepCount(++stepCount);

            if (GameManager.delay != 0)
                yield return new WaitForSeconds(GameManager.delay);

            foreach (ICell c in PathfindingTools.GetVisitedNeighbours(current, cells, false))
            {
                if (c.GetCellType() == CellTools.CellType.WALL) continue;
                if (c.GetIsVisited()) continue;

                c.SetParentCell(current);
                c.SetHelperNum(current.GetHelperNum() + 1);

                yield return StartCoroutine(RecursiveDFS(cells, c, target));

                if (coroutineBool)
                {
                    yield break;
                }
            }

            coroutineBool = false;
            yield break;
        }

        public string GetName()
        {
            return "Depth-First Search";
        }
    }
}