using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    [DefaultExecutionOrder(-2)]
    public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerActionMapActions
    {
        [SerializeField] private PlayerShooter _playerShooter;
        public PlayerControls PlayerControls { get; private set; }

        public bool AttackPressed { get; private set; }
        private void OnEnable()
        {
            PlayerControls = new PlayerControls();
            PlayerControls.Enable();

            PlayerControls.PlayerActionMap.Enable();
            PlayerControls.PlayerActionMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            PlayerControls.PlayerActionMap.Disable();
            PlayerControls.PlayerActionMap.RemoveCallbacks(this);
        }
  
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                AttackPressed = true;
                _playerShooter.Shoot();

            }
            else if (context.canceled)
            {
                AttackPressed = false ;
            }
        }
    }
}
