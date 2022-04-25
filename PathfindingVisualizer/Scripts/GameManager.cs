using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager _Instance { get { return _instance; } }
        private bool isInteractable = true;

        [SerializeField] private FieldGenerator cellGenerator;
        private ICell targetCell, startCell;

        [SerializeField] private MazeSettings mazeSettings;

        [SerializeField] private CellTypeProps[] cellTypeProps;

        [SerializeField] UIManager uIManager;

        public static float delay = 0.2f;


        private void Awake()
        {
            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            SetIsInteractable(true);
            GenerateField(mazeSettings.GetSettings()[0]);
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        #region Input
        private void HandleInput()
        {
            if (!isInteractable) return;
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider == null) return;

            ICell cell = hit.collider.GetComponent<ICell>();
            if (cell == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseButtonDown(cell);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RightMouseButtonDown(cell);
            }
            else if (Input.GetMouseButton(0))
            {
                LeftMouseButtonDown(cell);
            }
        }

        internal void SetDelay(float value)
        {
            delay = 1 - value;
        }

        private void LeftMouseButtonDown(ICell cell)
        {
            if (!isInteractable) return;

            if (Input.GetKey(KeyCode.LeftControl))
                cell.SetCellType(CellTools.CellType.EMPTY, false);
            else cell.SetCellType(CellTools.CellType.WALL, false);
        }

        private void RightMouseButtonDown(ICell cell)
        {
            if (!isInteractable) return;

            if (Input.GetKey(KeyCode.LeftControl))
                SetTargetCell(cell);
            else SetStartCell(cell);
        }

        public void ChangeStepCount(int steps)
        {
            uIManager.SetSteps(steps);
        }

        public void SetShortestPath(int steps)
        {
            uIManager.SetShortestPath(steps);
        }

        public ICell SetStartCell(ICell c)
        {
            if (startCell != null) startCell.SetCellType(CellTools.CellType.EMPTY, false);

            startCell = c;

            if (c != null)
                c.SetCellType(CellTools.CellType.START, false);

            return startCell;
        }

        public ICell SetTargetCell(ICell c)
        {
            if (targetCell != null) targetCell.SetCellType(CellTools.CellType.EMPTY, false);

            targetCell = c;

            if (c != null)
                c.SetCellType(CellTools.CellType.TARGET, false);

            return targetCell;
        }


        #endregion

        public void GenerateField(MazeSetting setting)
        {
            if (!isInteractable) return;

            targetCell = null;
            startCell = null;

            cellGenerator.ClearCells();

            cellGenerator.GenerateMap(setting);
        }

        public void GenerateMaze(IMazeGenerator generator)
        {
            if (!isInteractable) return;

            targetCell = null;
            startCell = null;

            SetIsInteractable(false);
            generator.Generate(cellGenerator.GetCells());
        }

        public void FindPath(IPathfinder pathfinder)
        {
            if (!isInteractable) return;

            ICell[,] cells = cellGenerator.GetCells();

            if (startCell == null)
            {
                startCell = SetStartCell(cells[UnityEngine.Random.Range(0, cells.GetLength(0)), UnityEngine.Random.Range(0, cells.GetLength(1))]);
            }

            if (targetCell == null)
            {
                targetCell = SetTargetCell(cells[UnityEngine.Random.Range(0, cells.GetLength(0)), UnityEngine.Random.Range(0, cells.GetLength(1))]);
            }


            SetIsInteractable(false);

            pathfinder.FindPath(cells, startCell, targetCell);
        }

        public void SetIsInteractable(bool b)
        {
            isInteractable = b;
        }

        public CellTypeProps GetCellTypeProps(int n)
        {
            return cellTypeProps[n];
        }
    }

}