using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class MazeSettings : MonoBehaviour
    {
        [SerializeField] private MazeSetting[] settings;

        [SerializeField] private IMazeGenerator[] generators;


        private void Awake()
        {
            generators = new IMazeGenerator[6];

            generators[0] = gameObject.AddComponent<RandomWalls>();
            generators[1] = gameObject.AddComponent<BinaryTreeMaze>();
            generators[2] = gameObject.AddComponent<Prim>();
            generators[3] = gameObject.AddComponent<RecursiveBacktracking>();
            generators[4] = gameObject.AddComponent<Kruskal>();
            generators[5] = gameObject.AddComponent<RecursiveDivision>();
        }

        public MazeSetting[] GetSettings()
        {
            return settings;
        }

        public IMazeGenerator[] GetGenerators()
        {
            return generators;
        }
    }

    [System.Serializable]
    public class MazeSetting
    {
        public int xSize, ySize;
        public float cellSize;
        public Vector3 fieldPos;
        public float gapBetweenCells;
    }
}