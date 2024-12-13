using System;
using DG.Tweening;
using Game.Scripts.Managers;
using PlayableAdsKit.Scripts.Base;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [HideInInspector] public bool IsTargetable = true;
        [SerializeField] private bool _canMove = true;
        
        private float _health=100f;
        private float _currentHealth;
        private float _speed=2f;

        private void OnEnable()
        {
            _currentHealth = _health;
            IsTargetable = true;
            _canMove = true;
            
            SetBoolAnim("Move",true);
        }

        private void OnDisable()
        {
            IsTargetable = false;
            _canMove = false;
        }

        private void FixedUpdate()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        { 
            if(!_canMove) return;

            var step = _speed * Time.fixedDeltaTime;
            transform.position =
                Vector3.MoveTowards(transform.position, GameManager.Instance.PlayerTransform.position, step);
            
            SetRotation();
        }

        private void SetRotation()
        {
            Vector3 direction = GameManager.Instance.PlayerTransform.position - transform.position;
            direction.y = 0f; // Zero out the vertical component to ensure no tilt

            // If the direction vector isn't zero-length, we can face that direction
            if (direction != Vector3.zero)
            {
                // Create a target rotation purely on the horizontal plane
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

                // Optionally, smoothly interpolate the rotation for a more natural turn
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
            }
        }
        
        public void ReceiveDamage(float damageAmount)
        {
            _currentHealth -= damageAmount;
            Debug.Log("Call text with the value of "+ damageAmount);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if(!IsTargetable) return;
            IsTargetable = false;
            _canMove = false;

            ScoreManager.Instance.FillBar();
            SetBoolAnim("Move",false);
            SetBoolAnim("Attack",false);
            // ObjectPoolManager.Instance.GetPooledObject(ObjectName.Enemy_Die_Particle,transform.position,Quaternion.identity);
            gameObject.SetActive(false);
        }
        
        private void SetBoolAnim(string name,bool status)
        {
             _animator.SetBool(name,status);
        }
    }
}
