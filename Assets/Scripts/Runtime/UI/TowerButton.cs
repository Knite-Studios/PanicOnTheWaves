using Managers;
using NaughtyAttributes;
using Scriptable.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TowerInfo towerInfo;
        [SerializeField, Required] private Image cooldownImage;

        private Button _button;
        private float _cooldownTimer;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Update()
        {
            if (!(_cooldownTimer > 0)) return;
            _cooldownTimer -= Time.deltaTime;
            cooldownImage.fillAmount = _cooldownTimer / towerInfo.cooldown;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickEvent);
            TowerManager.OnTowerPlaced += OnTowerPlacedEvent;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClickEvent);
            TowerManager.OnTowerPlaced -= OnTowerPlacedEvent;
        }

        private void OnTowerPlacedEvent()
        {
            _cooldownTimer = towerInfo.cooldown;
            cooldownImage.fillAmount = 1;
        }

        private void OnClickEvent()
        {
            if (_cooldownTimer > 0) return;
            TowerManager.Instance.SelectTower(towerInfo);
        }
    }
}