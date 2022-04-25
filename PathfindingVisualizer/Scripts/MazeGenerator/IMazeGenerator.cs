using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public interface IMazeGenerator
    {
        string GetName();

        void Generate(ICell[,] cells);
    }
}