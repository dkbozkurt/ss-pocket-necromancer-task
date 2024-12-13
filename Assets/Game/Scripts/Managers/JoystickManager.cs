using PlayableAdsKit.Scripts.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Game.Scripts.Managers
{
    public class JoystickManager : SingletonBehaviour<JoystickManager>, IDragHandler, IPointerDownHandler,IPointerUpHandler
    {
        public RectTransform JoystickBackground;
        public RectTransform JoystickHandle;
    
        private Vector2 _input = Vector2.zero;
        private Vector2 _joyPosition = Vector2.zero;

        private float _thresholdValue = 0.2f;
    
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 joyDirection = eventData.position - _joyPosition;
            _input = (joyDirection.magnitude > JoystickBackground.sizeDelta.x / 2f)
                ? joyDirection.normalized
                : joyDirection / (JoystickBackground.sizeDelta.x / 2f);
            JoystickHandle.anchoredPosition = (_input * JoystickBackground.sizeDelta.x / 2f) * 1f;

        }
        public void OnPointerDown(PointerEventData eventData)
        {
            JoystickBackground.gameObject.SetActive(true);
            OnDrag(eventData);
            _joyPosition = eventData.position;
            JoystickBackground.position = eventData.position;
            JoystickHandle.anchoredPosition = Vector2.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            JoystickBackground.gameObject.SetActive(false);
            _input=Vector2.zero;
            JoystickHandle.anchoredPosition = Vector2.zero;
        }
    

        public float InputHorizontal()
        {
        
            if (_input.x != 0)
            {
                if (_input.x > _thresholdValue || _input.x < -_thresholdValue) return _input.x;
                else return 0f;
            }
            else
            {
                _input.x = 0f;
                return Input.GetAxis("Horizontal");
            }
            
        }

        public float InputVertical()
        {
        
            if (_input.y!= 0)
            {
                if (_input.y > _thresholdValue || _input.y < -_thresholdValue) return _input.y;
                else return 0f;
            }
            else
            {
                _input.y = 0f;
                return Input.GetAxis("Vertical");
            }
        }

        protected override void OnAwake() { }
    }
}