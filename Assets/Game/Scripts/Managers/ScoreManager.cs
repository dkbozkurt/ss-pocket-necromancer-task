using System;
using DG.Tweening;
using PlayableAdsKit.Scripts.Helpers;
using PlayableAdsKit.Scripts.PlaygroundConnections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Managers
{
    public class ScoreManager : SingletonBehaviour<ScoreManager>
    {
        public static event Action OnUpgradeAchieved;
        
        protected override void OnAwake() { }
        
        [SerializeField] private TextMeshProUGUI _killCountText;
        [SerializeField] private Image fillBar;
        
        [SerializeField] private int _gameEndEnemyCount = 30;
        [SerializeField] protected float fillDuration = 0.15f;

        private bool IsFullFilled => fillBar.fillAmount >= 1;

        private float filledPercentage = 0f;
        private int _killedEnemeyCount = 0;
        private int _upgradeCount = 0;

        private void Awake()
        {
            Reset();
        }
        
        public void FillBar()
        {
            fillBar.DOKill();
            fillBar.fillAmount = filledPercentage;
            
            if (IsFullFilled)
            {
                BarFilled();
                return;
            }
			
            var calculatedIncrementPercentage = 1f / (float)_gameEndEnemyCount;

            _killedEnemeyCount++;
            SetKilledEnemyCount();
            
            filledPercentage += calculatedIncrementPercentage;
            CheckForUpgrades();
			
            fillBar.DOFillAmount(filledPercentage, fillDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                fillBar.fillAmount = filledPercentage;
                
                if (IsFullFilled) BarFilled();
            });

        }

        private void CheckForUpgrades()
        {
            if ((_upgradeCount == 0 && _killedEnemeyCount >= 10) ||
                (_upgradeCount == 1 && _killedEnemeyCount >= 20))
            {
                _upgradeCount++;
                OnUpgradeAchieved?.Invoke();
            }
        }

        private void BarFilled()
        {
            EndCardController.Instance.OpenEndCard();
            Debug.Log("Deactivate input and spawn system");
        }

        private void Reset()
        {
            _killedEnemeyCount = 0;
            fillBar.fillAmount = 0f;
            filledPercentage = 0f;

            SetKilledEnemyCount();
        }
        
        private void SetKilledEnemyCount()
        {
            _killCountText.text = _killedEnemeyCount.ToString();
        }
        
    }
}
