using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class FieldGenerator : MonoBehaviour
    {

        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private MazeSettings mazeSettings;

        private ICell[,] cells;

        public void GenerateMap(MazeSetting setting)
        {
            GenerateMap(setting.xSize, setting.ySize, setting.cellSize, setting.gapBetweenCells);
            transform.position = setting.fieldPos;
        }

        private void GenerateMap(int xSize, int ySize, float scale, float gap)
        {
            cells = new Cell[xSize, ySize];

            float offsetX = -xSize / 2f * scale;
            float offsetY = -ySize / 2f * scale;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {

                    GameObject cellObject = Instantiate(cellPrefab);
                    ICell ic = cellObject.GetComponent<ICell>();

                    cellObject.gameObject.name = i + "_" + j;

                    cellObject.transform.SetParent(transform);
                    cellObject.transform.localPosition = new Vector3(j * scale + j * gap + offsetX, i * scale + i * gap + offsetY, 0);
                    cellObject.transform.localScale = Vector3.one * scale;

                    ic.SetGridPos(i, j);

                    cells[i, j] = ic;
                }
            }
        }

        public void ClearCells()
        {
            if (cells != null)
            {
                foreach (ICell ic in cells)
                {
                    MonoBehaviour mb = ic as MonoBehaviour;
                    Destroy(mb.gameObject);
                }

                cells = null;
            }
        }

        public ICell[,] GetCells()
        {
            return cells;
        }
    }
}