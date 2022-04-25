using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class Cell : MonoBehaviour, ICell
    {
        private int x, y;
        private int helperNum;
        private bool isVisited;
        private ICell parentCell;

        [SerializeField] private CellTools.CellType type;

        private SpriteRenderer spriteRenderer;


        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetCellType(CellTools.CellType.EMPTY, true);
        }

        public void SetGridPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        private IEnumerator ColorChangeAnimation(Color newColor, float time)
        {
            Color prevColor = spriteRenderer.color;

            float t = 0;
            while (t < time)
            {
                t += Time.deltaTime;
                spriteRenderer.color = Color.Lerp(prevColor, newColor, t / time);

                yield return null;
            }

            spriteRenderer.color = newColor;
        }

        public void SetCellType(CellTools.CellType type, bool forceChange)
        {
            this.type = type;

            CellTypeProps cellType = GameManager._Instance.GetCellTypeProps((int)type);
            Color newColor = cellType.GetColor();
            spriteRenderer.sprite = cellType.GetSprite();

            SetColor(newColor, forceChange);
        }

        public void SetColor(Color color, bool forceChange)
        {
            StopAllCoroutines();

            if (!forceChange)
                StartCoroutine(ColorChangeAnimation(color, 0.5f));
            else
            {
                StartCoroutine(ColorChangeAnimation(color, 0.0f));
            }
        }

        public CellTools.CellType GetCellType()
        {
            return type;
        }

        public int GetHelperNum()
        {
            return helperNum;
        }

        public void SetHelperNum(int n)
        {
            helperNum = n;
        }

        public bool GetIsVisited()
        {
            return isVisited;
        }

        public void SetIsVisited(bool b)
        {
            isVisited = b;
        }

        public ICell GetParentCell()
        {
            return parentCell;
        }

        public void SetParentCell(ICell ic)
        {
            parentCell = ic;
        }
    }
}