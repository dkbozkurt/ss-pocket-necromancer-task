using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class CharacterMovementController : MonoBehaviour
    {
        public bool IsMoving = false;

        [SerializeField] private float speed = 10f;
        [SerializeField] private float turnSmoothTime = 0.1f;
        [SerializeField] private Animator _playerAnimator;
        
        private CharacterController _characterController;
        private float turnSmoothVelocity;
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float inputX = JoystickManager.Instance.InputHorizontal();
            float inputZ = JoystickManager.Instance.InputVertical();
        
            Vector3 direction = new Vector3(inputX, 0f, inputZ).normalized;
        
            if (direction.magnitude >= 0.1f)
            {
                Rotate(direction);
                _characterController.Move(direction * (speed * Time.deltaTime));
                SetBoolAnim("Move",true);
                IsMoving = true;
            
                return;
            }
        
            SetBoolAnim("Move",false);
            IsMoving = false;
        }

        private void Rotate(Vector3 direction)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        
            // Adding smooth turning
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        
            transform.rotation = Quaternion.Euler(0f,angle,0f);
        }

        private void SetBoolAnim(string name,bool status)
        {
             _playerAnimator.SetBool(name,status);
        }
    }
}
