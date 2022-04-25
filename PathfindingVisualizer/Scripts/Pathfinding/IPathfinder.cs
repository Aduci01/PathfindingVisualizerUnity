using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public interface IPathfinder
    {
        void FindPath(ICell[,] cells, ICell start, ICell target);

        string GetName();
    }

}