using System;
using Managers;
using UnityEngine;

namespace World
{
    [DefaultExecutionOrder(100)]
    public class GridBehaviour : MonoBehaviour
    {
        [SerializeField] private int rows = 6;
        [SerializeField] private int columns = 10;
        [SerializeField] private Vector3 gridDimensions = new Vector3(20, 1, 10);
#if UNITY_EDITOR
        [SerializeField] private bool showGrid = true;
#endif

        public int Rows => rows;

        private Vector3 _cellSize;
        private Cell[,] _grid;

        private void Start()
        {
            // Add interaction handlers.
            InputManager.Instance.StartSelectGrid += OnGridSelect;
            
            CalculateCellSize();
            CreateGrid();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            CalculateCellSize();

            if (showGrid)
            {
                Gizmos.color = Color.magenta;
                var startPos = transform.position - new Vector3(gridDimensions.x / 2, 0, gridDimensions.z / 2);

                for (var x = 0; x <= columns; x++)
                {
                    var start = startPos + new Vector3(x * _cellSize.x, 0, 0);
                    var end = start + new Vector3(0, 0, rows * _cellSize.z);
                    Gizmos.DrawLine(start, end);
                }

                for (var z = 0; z <= rows; z++)
                {
                    var start = startPos + new Vector3(0, 0, z * _cellSize.z);
                    var end = start + new Vector3(columns * _cellSize.x, 0, 0);
                    Gizmos.DrawLine(start, end);
                }
            }
        }
#endif
        
        private void OnGridSelect()
        {
            var ray = Camera.main!.ScreenPointToRay(InputManager.MousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.collider.GetComponent<GridBehaviour>()) return;
            
            var gridPosition = GetGridPosition(hit.point);
            var cell = GetCell(gridPosition.x, gridPosition.y);
            Debug.Log($"<color=yellow>Cell: {cell.X}, {cell.Z}, Center:{cell.Center}</color>");
            TowerManager.Instance.PlaceTower(cell.Center);
        }

        private void CalculateCellSize()
            => _cellSize = new Vector3(gridDimensions.x / columns, gridDimensions.y, gridDimensions.z / rows);
        
        private void CreateGrid()
        {
            _grid = new Cell[columns, rows];
            for (var x = 0; x < columns; x++)
            {
                for (var z = 0; z < rows; z++)
                {
                    var center = GetCellCenter(x, z);
                    _grid[x, z] = new Cell(center, x, z);
                }
            }
        }

        public Vector3 GetCellCenter(int x, int z)
        {
            var startPos = transform.position - new Vector3(gridDimensions.x / 2, 0, gridDimensions.z / 2);
            var halfCellX = _cellSize.x * 0.5f;
            var halfCellZ = _cellSize.z * 0.5f;
            var cellX = startPos.x + x * _cellSize.x + halfCellX;
            var cellZ = startPos.z + z * _cellSize.z + halfCellZ;
            return new Vector3(cellX, transform.position.y, cellZ);
        }

        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            var startPos = transform.position - new Vector3(gridDimensions.x / 2, 0, gridDimensions.z / 2);
            var x = Mathf.FloorToInt((worldPosition.x - startPos.x) / _cellSize.x);
            var z = Mathf.FloorToInt((worldPosition.z - startPos.z) / _cellSize.z);
            return new Vector2Int(x, z);
        }

        public Cell GetCell(int x, int z)
        {
            if (x >= 0 && x < columns && z >= 0 && z < rows)
                return _grid[x, z];

            return null;
        }

        [Serializable]
        public class Cell
        {
            public Vector3 Center { get; private set; }
            public int X { get; private set; }
            public int Z { get; private set; }
            
            public Cell(Vector3 center, int x, int z)
            {
                Center = center;
                X = x;
                Z = z;
            }
        }
    }
}