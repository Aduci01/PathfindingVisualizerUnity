using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class Kruskal : MonoBehaviour, IMazeGenerator
    {
        List<List<ICell>> sets;
        public void Generate(ICell[,] cells)
        {
            StartCoroutine(DelayedGeneration(cells));
        }

        IEnumerator DelayedGeneration(ICell[,] cells)
        {
            MazeTools.SetAllCellTypes(CellTools.CellType.EMPTY, cells);

            List<Tuple<ICell, int>> edges = new List<Tuple<ICell, int>>();
            Dictionary<int, List<ICell>> sets = new Dictionary<int, List<ICell>>();

            int setNum = 0;
            for (int i = 1; i < cells.GetLength(0) - 1; i += 2)
            {
                for (int j = 1; j < cells.GetLength(1) - 1; j += 2)
                {
                    ICell c2 = cells[i, j];

                    edges.Add(new Tuple<ICell, int>(c2, 0));
                    edges.Add(new Tuple<ICell, int>(c2, 1));


                    c2.SetHelperNum(setNum);

                    List<ICell> list = new List<ICell>();
                    list.Add(c2);

                    sets.Add(setNum++, list);
                }
            }

            edges = edges.OrderBy(item => UnityEngine.Random.Range(0, 1000f)).ToList();

            while (edges.Count != 0)
            {
                ICell c = edges[0].Item1;
                int orientation = edges[0].Item2;

                edges.RemoveAt(0);

                if (c.GetCellType() == CellTools.CellType.WALL)
                {
                    continue;
                }

                if (orientation == 0)
                { //vertical
                    int x = c.GetX(), y = c.GetY();
                    if (x >= 0 && x < cells.GetLength(0) - 2)
                    {
                        ICell up = cells[x + 2, y];

                        if (up.GetHelperNum() == c.GetHelperNum()) continue;

                        UnifySets(c.GetHelperNum(), up.GetHelperNum(), sets);

                        up.SetCellType(CellTools.CellType.WALL, false);
                        cells[x + 1, y].SetCellType(CellTools.CellType.WALL, false);
                        cells[x, y].SetCellType(CellTools.CellType.WALL, false);
                    }
                }
                else
                { //horizontal
                    int x = c.GetX(), y = c.GetY();
                    if (y >= 0 && y < cells.GetLength(1) - 2)
                    {
                        ICell right = cells[x, y + 2];

                        if (right.GetHelperNum() == c.GetHelperNum()) continue;

                        UnifySets(c.GetHelperNum(), right.GetHelperNum(), sets);

                        right.SetCellType(CellTools.CellType.WALL, false);
                        cells[x, y + 1].SetCellType(CellTools.CellType.WALL, false);
                        cells[x, y].SetCellType(CellTools.CellType.WALL, false);
                    }
                }

                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }

            GameManager._Instance.SetIsInteractable(true);
        }

        void UnifySets(int s1Num, int s2Num, Dictionary<int, List<ICell>> set)
        {
            if (s1Num == s2Num) return;

            foreach (ICell c in set[s2Num])
            {
                set[s1Num].Add(c);
                c.SetHelperNum(s1Num);
            }

            set[s2Num].Clear();
        }

        public string GetName()
        {
            return "Kruskal";
        }
    }
}