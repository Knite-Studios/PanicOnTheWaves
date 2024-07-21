using Common;
using Scriptable.Scriptable;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        // TODO: Replace this later when we have a currency system.
        [SerializeField] private int initialHype = 100;

        public UnityEvent<int> onHypeChange;

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

        public void PlaceTower(Vector3 position)
        {
            if (!_isPlacingTower || _selectedTower == null) return;
            if (_currentHype < _selectedTower.cost) return;

            PrefabManager.Create(_selectedTower.towerPrefab, position);
            _currentHype -= _selectedTower.cost;
            onHypeChange?.Invoke(_currentHype);
            
            _isPlacingTower = false;
            _selectedTower = null;
        }
    }
}