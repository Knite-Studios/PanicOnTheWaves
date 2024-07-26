using Common;
using Entity.Towers;
using Scriptable.Scriptable;
using UnityEngine;
using UnityEngine.Events;
using World;

namespace Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        // TODO: Replace this later when we have a currency system.
        [SerializeField] private int initialHype = 100;

        public UnityEvent<int> onHypeChange;
        
        public bool HasSelection => _isPlacingTower;

        private int _currentHype;
        private TowerInfo _selectedTower;
        private bool _isPlacingTower;

        private void Start()
        {
            _currentHype = initialHype;
            onHypeChange?.Invoke(_currentHype);
        }

        public void SelectTower(TowerInfo towerInfo)
        {
            Debug.Log($"<color=green>Selected tower: {towerInfo.name}</color>");
            _selectedTower = towerInfo;
            _isPlacingTower = true;
        }

        public void PlaceTower(GridBehaviour.Cell cell)
        {
            if (!_isPlacingTower || _selectedTower == null) return;
            if (_currentHype < _selectedTower.cost) return;
            if (cell.IsOccupied) return;

            var tower = PrefabManager.Create<BaseTower>(_selectedTower.towerPrefab, cell.TopCenter);
            cell.IsOccupied = true;

            _currentHype -= _selectedTower.cost;
            onHypeChange?.Invoke(_currentHype);
            
            _isPlacingTower = false;
            _selectedTower = null;
            
            tower.OnTowerDestroyed += () => cell.IsOccupied = false;
        }
    }
}