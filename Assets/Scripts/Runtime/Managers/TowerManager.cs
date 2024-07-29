using Common;
using Entity.Towers;
using Scriptable.Scriptable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using World;

namespace Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        public static UnityAction<int> OnHypeChange;
        public static UnityAction OnTowerPlaced;

        // TODO: Replace this later when we have a currency system.
        [SerializeField] private int initialHype = 100;

        private int _currentHype;
        private TowerInfo _selectedTower;
        private bool _isPlacingTower;
        private GridBehaviour _grid;

        public bool HasSelection => _isPlacingTower;

        private void Start()
        {
            _currentHype = initialHype;
            OnHypeChange?.Invoke(_currentHype);
            _grid = FindObjectOfType<GridBehaviour>();
        }

        protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _isPlacingTower = false;
            _selectedTower = null;
            _grid = FindObjectOfType<GridBehaviour>();
        }

        protected override void OnSceneUnloaded(Scene scene)
        {
            _isPlacingTower = false;
            _selectedTower = null;
            _grid = null;
        }

        public void SelectTower(TowerInfo towerInfo)
        {
            if (!_grid) _grid = FindObjectOfType<GridBehaviour>();

            Debug.Log($"<color=green>Selected tower: {towerInfo.name}</color>");
            _selectedTower = towerInfo;
            _isPlacingTower = true;
        }

        public void PlaceTower(GridBehaviour.Cell cell)
        {
            if (!_isPlacingTower || _selectedTower == null) return;
            if (_currentHype < _selectedTower.cost || _currentHype <= 0) return;
            if (cell.IsOccupied) return;

            if (!_grid) _grid = FindObjectOfType<GridBehaviour>();

            var tower = PrefabManager.Create<BaseTower>(_selectedTower.towerPrefab, cell.TopCenter);
            _grid.AddTowerInCell(cell.X, cell.Z, tower);

            _currentHype -= _selectedTower.cost;
            OnHypeChange?.Invoke(_currentHype);
            OnTowerPlaced?.Invoke();
            
            _isPlacingTower = false;
            _selectedTower = null;
            
            tower.OnTowerDestroyed += () => _grid.RemoveTowerInCell(cell.X, cell.Z);
        }
    }
}