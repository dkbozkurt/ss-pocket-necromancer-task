using System;
using DG.Tweening;
using Game.Scripts.Behaviours;
using Game.Scripts.Managers;
using PlayableAdsKit.Scripts.PlaygroundConnections;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class GhostsController : MonoBehaviour
    {
        [SerializeField] private Ghost[] _ghosts = new Ghost[] { };

        private int _ghostsLevel = 1;

        private void OnEnable()
        {
            ScoreManager.OnUpgradeAchieved += Upgrade;
        }

        private void OnDisable()
        {
            ScoreManager.OnUpgradeAchieved -= Upgrade;
        }

        void Start()
        {
            AnimateRotation();
        }

        private void AnimateRotation()
        {
            transform.DOLocalRotate(Vector3.up * 360, 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        private void Upgrade()
        {
            _ghostsLevel++;

            if (_ghostsLevel == 2)
            {
                _ghosts[1].gameObject.SetActive(true);
            }
            else if (_ghostsLevel == 3)
            {
                _ghosts[2].gameObject.SetActive(true);
                RelocateGhosts();
            }
            else
            {
                EndCardController.Instance.OpenEndCard();
            }
        }

        private void RelocateGhosts()
        {
            _ghosts[0].transform.localPosition = new Vector3(3, 0, 0);
            _ghosts[2].transform.localPosition = new Vector3(-1.5f, 0, 2.598f);
            _ghosts[1].transform.localPosition =new Vector3(-1.5f, 0, -2.598f);
        }
    }
}
