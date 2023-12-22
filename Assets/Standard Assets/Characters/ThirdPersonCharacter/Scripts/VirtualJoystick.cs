using UnityEngine;
using UnityEngine.EventSystems;

    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [Header("References")]
        public RectTransform joystickBase;
        public RectTransform joystickHandle;

        [Header("Joystick Parameters")]
        public float joystickRadius = 50f; // Adjust this based on your UI size

        private Vector2 joystickCenter;
        private Vector2 inputDirection = Vector2.zero;
        private bool isDragging = false;

        private void Start()
        {
            // Calculate the center of the joystick base
            joystickCenter = joystickBase.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetJoystick();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pointerPosition = eventData.position;

            // Calculate the direction from the center of the joystick to the pointer
            inputDirection = (pointerPosition - joystickCenter).normalized;

            // Limit the input distance to the joystick's radius
            float inputMagnitude = (pointerPosition - joystickCenter).magnitude;
            inputDirection *= Mathf.Clamp(inputMagnitude / joystickRadius, -1f, 1f);

            // Update the position of the joystick handle
            joystickHandle.anchoredPosition = inputDirection * joystickRadius;

            isDragging = true;
        }

        private void ResetJoystick()
        {
            // Reset the input direction and joystick handle position
            inputDirection = Vector2.zero;
            joystickHandle.anchoredPosition = Vector2.zero;
            isDragging = false;
        }

        public Vector2 GetInputDirection()
        {
            return isDragging ? inputDirection : Vector2.zero;
        }
    }