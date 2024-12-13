using System;
using PlayableAdsKit.Scripts.Helpers;
using PlayableAdsKit.Scripts.PlaygroundConnections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Managers
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public static event Action OnGameStarted;
        public static event Action OnGameStop;
        protected override void OnAwake() { }

        public bool CanGetInput = true;

        public Transform PlayerTransform;
        [HideInInspector] public bool IsFirstClickOccured = false;
        
        private void Update()
        {
            if (!IsFirstClickOccured && Input.GetMouseButtonDown(0))
            {
                OnGameStarted?.Invoke();
                IsFirstClickOccured = true;
                TutorialController.Instance.Deactivate();
            }
        }

        public void Stop()
        {
            OnGameStop?.Invoke();
            CanGetInput = false;

        }
        
    }
}
