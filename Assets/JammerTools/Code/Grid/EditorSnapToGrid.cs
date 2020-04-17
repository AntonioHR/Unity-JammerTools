using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JammerTools.Common.Grids
{
    [ExecuteInEditMode]
    public class EditorSnapToGrid : MonoBehaviour
    {
        private WorldGrid grid;


        private void OnValidate()
        {
            grid = FindObjectOfType<WorldGrid>();
        }
        private void Start()
        {
            if (Application.isPlaying)
                enabled = false;
        }
        private void Update()
        {
            if (!grid)
                return;
            transform.position = grid.SnapToWorld(transform.position);
        }

    }
}
