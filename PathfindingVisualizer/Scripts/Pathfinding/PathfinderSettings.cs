using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMGames.Pathfinding
{
    public class PathfinderSettings : MonoBehaviour
    {
        [SerializeField] private IPathfinder[] pathfinders;

        private void Awake()
        {
            pathfinders = new IPathfinder[3];
            pathfinders[0] = gameObject.AddComponent<BFS>();
            pathfinders[1] = gameObject.AddComponent<DFS>();
            pathfinders[2] = gameObject.AddComponent<AStar>();
        }

        public IPathfinder[] GetPathfinders()
        {
            return pathfinders;
        }
    }
}