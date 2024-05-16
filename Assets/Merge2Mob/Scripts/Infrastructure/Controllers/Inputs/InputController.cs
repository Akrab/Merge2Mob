using UnityEngine;
using UnityEngine.InputSystem;

namespace MergeTwoMob.Infrastructure.Controllers.Inputs
{
    public delegate void OnTouchEvent(Vector2 position);
    public class InputController
    {
        private NewInput newInput;
        
        public event OnTouchEvent OnStartTouchEvent;
        public event OnTouchEvent OnEndTouchEvent;
        public event OnTouchEvent OnMoveTouchEvent;
        
        public InputController()
        {
            newInput = new NewInput();
  
            newInput.Touch.TouchPress.started += TouchPressOnStarted;
            newInput.Touch.TouchPress.canceled += TouchPressOnCanceled;
            newInput.Touch.TouchInput.performed += TouchPressOnPerformed;
            
        }

        private void TouchPressOnPerformed(InputAction.CallbackContext obj)
        {
            if (OnMoveTouchEvent != null)
            {
                OnMoveTouchEvent(newInput.Touch.TouchPosition.ReadValue<Vector2>());
            }
        }

        public void Enable()
        {
            newInput.Enable();
        }

        private void TouchPressOnCanceled(InputAction.CallbackContext obj)
        {
            OnEndTouchEvent?.Invoke(newInput.Touch.TouchPosition.ReadValue<Vector2>());
        }

        private void TouchPressOnStarted(InputAction.CallbackContext obj)
        {
            OnStartTouchEvent?.Invoke(newInput.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }
}