
using System;
using UnityEngine;
using Pathfinding;

namespace PointNSheep.Common.Pathfinding
{
    public class PathFindingManager : MonoBehaviour {
        private bool dirty;
        [SerializeField]
        private Vector3 center;

        public static string[] ObstacleLayers = new[] { "Obstacle", "BatteryHitbox" };

        void Start() {
            // CreatePath();

            AstarData data = AstarPath.active.data;

            GridGraph gg = data.gridGraph;

            // Setup a grid graph with some values

            gg.center = center;
            gg.collision.mask = LayerMask.GetMask(ObstacleLayers);


            // Scans all graphs
            AstarPath.active.Scan();

            gg.collision.collisionCheck = true;
            gg.collision.diameter = 0.2f;
        }

        public void TriggerRefresh() {
            dirty = true;
        }
        private void LateUpdate()
        {
            if(dirty)
            {
                AstarPath.active.Scan();
                dirty = false;
            }
        }

        private void CreatePath()
        {
            GameObject pathGraph = new GameObject("GraphPath");
            pathGraph.AddComponent<AstarPath>();
        }
    }
}