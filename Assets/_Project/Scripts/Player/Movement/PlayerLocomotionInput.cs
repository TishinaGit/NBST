using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    [DefaultExecutionOrder(-2)]
    public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
    { 
        public PlayerControls PlayerControls { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public bool IsSprint { get; private set; }
        public bool IsJump { get; private set; }

        private bool _isHoldToSprint = true;

        private void OnEnable()
        {
            PlayerControls = new PlayerControls();
            PlayerControls.Enable();

            PlayerControls.PlayerLocomotionMap.Enable();
            PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            PlayerControls.PlayerLocomotionMap.Disable();
            PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
        }

         
        private void LateUpdate()
        {
            IsJump = false;
        }

         
        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            IsJump = true;
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsSprint = _isHoldToSprint || !IsSprint;
            }
            else if (context.canceled)
            {
                IsSprint = !_isHoldToSprint && IsSprint;
            }
        }
    }
}
