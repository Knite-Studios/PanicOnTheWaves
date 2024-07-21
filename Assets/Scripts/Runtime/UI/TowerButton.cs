using Managers;
using Scriptable.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TowerInfo towerInfo;

        private void Start()
            => GetComponent<Button>().onClick.AddListener(() => TowerManager.Instance.SelectTower(towerInfo));
    }
}