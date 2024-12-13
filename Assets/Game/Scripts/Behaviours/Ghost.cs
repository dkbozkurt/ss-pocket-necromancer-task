using System;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class Ghost : MonoBehaviour
    {
        private float _damageAmount = 50f;
        private void FixedUpdate()
        {
            Quaternion desiredWorldRotation = Quaternion.LookRotation(GameManager.Instance.PlayerTransform.forward, Vector3.up);
            Quaternion desiredLocalRotation = Quaternion.Inverse(transform.parent.rotation) * desiredWorldRotation;
            transform.localRotation = desiredLocalRotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.ReceiveDamage(_damageAmount);
            }
        }
    }
}
