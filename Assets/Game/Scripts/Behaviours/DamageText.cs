using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class DamageText : MonoBehaviour
    {
        private void OnEnable()
        {
            Animate();
        }

        private void OnDisable()
        {
            transform.position = Vector3.zero;   
        }

        private void Animate()
        {
            transform.DOMoveY(0.5f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
