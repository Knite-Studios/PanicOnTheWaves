using System;
using System.Collections.Generic;
using Entity.Towers;
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
                var startPos = transform.position - new Vector3(gridDimensions.x / 2, -gridDimensions.y / 2, gridDimensions.z / 2);

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

            // Ensures that we're only placing towers when we have a selection.
            if (TowerManager.Instance.HasSelection && !cell.IsOccupied)
                TowerManager.Instance.PlaceTower(cell);

            Debug.Log($"<color=yellow>Cell:{cell.X}, {cell.Z}, Top Center:{cell.TopCenter}, Base:{cell.OccupyingTower.name}</color>");
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
                    var topCenter = GetCellTopCenter(x, z);
                    _grid[x, z] = new Cell(center, topCenter, x, z);
                }
            }
        }

        public Vector3 GetCellCenter(int x, int z)
        {
            var startPos = transform.position - new Vector3(gridDimensions.x / 2, -gridDimensions.y / 2, gridDimensions.z / 2);
            var halfCellX = _cellSize.x * 0.5f;
            var halfCellZ = _cellSize.z * 0.5f;
            var cellX = startPos.x + x * _cellSize.x + halfCellX;
            var cellZ = startPos.z + z * _cellSize.z + halfCellZ;
            return new Vector3(cellX, startPos.y, cellZ);
        }

        public Vector3 GetCellTopCenter(int x, int z)
        {
            var center = GetCellCenter(x, z);
            return new Vector3(center.x, center.y + _cellSize.y / 2, center.z);
        }

        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            var startPos = transform.position - new Vector3(gridDimensions.x / 2, -gridDimensions.y / 2, gridDimensions.z / 2);
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

        public List<Vector3> GetRowWaypoints(int idx)
        {
            // Check if the index is within bounds.
            if (idx < 0 || idx >= rows) return null;

            var waypoints = new List<Vector3>();

            for (var i = 0; i < columns; i++)
            {
                // var cell = GetCell(i, idx);
                waypoints.Add(GetCellTopCenter(i, idx));
            }
            
            return waypoints;
        }

        public List<Cell> GetCellsInRowRange(int startX, int startZ, int range)
        {
            var cells = new List<Cell>();
            
            for (var x = startX - range; x <= startX + range; x++)
            {
                var cell = GetCell(x, startZ);
                if (cell != null) cells.Add(cell);
            }

            return cells;
        }
        
        public List<Cell> GetCellsInColumnRange(int startX, int startZ, int range)
        {
            var cells = new List<Cell>();
            
            for (var z = startZ - range; z <= startZ + range; z++)
            {
                var cell = GetCell(startX, z);
                if (cell != null) cells.Add(cell);
            }

            return cells;
        }
        
        public void AddTowerInCell(int x, int z, BaseTower tower)
        {
            var cell = GetCell(x, z);
            if (cell == null) return;

            cell.IsOccupied = true;
            cell.OccupyingTower = tower;
        }
        
        public void RemoveTowerInCell(int x, int z)
        {
            var cell = GetCell(x, z);
            if (cell == null) return;

            cell.IsOccupied = false;
            cell.OccupyingTower = null;
        }

        [Serializable]
        public class Cell
        {
            public Vector3 Center { get; private set; }
            public Vector3 TopCenter { get; private set; }
            public int X { get; private set; }
            public int Z { get; private set; }
            public bool IsOccupied { get; set; }
            public BaseTower OccupyingTower { get; set; }

            public Cell(Vector3 center, Vector3 topCenter, int x, int z)
            {
                Center = center;
                TopCenter = topCenter;
                X = x;
                Z = z;
                IsOccupied = false;
                OccupyingTower = null;
            }
        }
    }
}