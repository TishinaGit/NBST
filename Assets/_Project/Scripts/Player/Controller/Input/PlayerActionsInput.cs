using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Controller
{
    [DefaultExecutionOrder(-2)]
    public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerAttackActions
    {
        public bool AttackPressed;
        private PlayerControls PlayerControls;
        [SerializeField] private AudioSource _pistolAttack;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _pistolTarget;
        [SerializeField] private Transform _aimTarget;


       
        private void OnEnable()
        {
            PlayerControls = new PlayerControls();
            PlayerControls.Enable();

            PlayerControls.PlayerAttack.Enable();
            PlayerControls.PlayerAttack.SetCallbacks(this);
        }

        private void OnDisable()
        {
            PlayerControls.PlayerAttack.Disable();
            PlayerControls.PlayerAttack.RemoveCallbacks(this);
        }

        private void Update()
        {
            if (AttackPressed == true)
            {
                transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
            }
        }


        public void PlaySound()
        {
            _pistolAttack.Play();
            
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                AttackPressed = true;
                
                Shooting();

            }
            if (context.canceled)
            {
                AttackPressed = false;
            }
        }

        public void Shooting()
        {
            
            Vector3 aimPos = (_aimTarget.position - _pistolTarget.position).normalized;

            Instantiate(_bulletPrefab, _pistolTarget.position, Quaternion.LookRotation(aimPos));
            
        }
    }
}
