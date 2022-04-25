using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public interface ICell
    {
        void SetGridPos(int x, int y);

        int GetX();
        int GetY();

        void SetCellType(CellTools.CellType type, bool forceChange);
        void SetColor(Color color, bool forceChange);
        CellTools.CellType GetCellType();

        int GetHelperNum();
        void SetHelperNum(int n);

        bool GetIsVisited();
        void SetIsVisited(bool b);

        ICell GetParentCell();
        void SetParentCell(ICell ic);
    }
}