using UnityEngine;

namespace Controller
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _locomotionBlendSpeed = 5f;

        private PlayerLocomotionInput _playerLocomotionInput;
        private PlayerActionsInput _playerActionsInput;
        private PlayerState _playerState; 

        private static int inputXHash = Animator.StringToHash("inputX");
        private static int inputYHash = Animator.StringToHash("inputY");
        private static int inputMagnitudeHash = Animator.StringToHash("inputMagnitude"); 
        private static int isGroundedHash = Animator.StringToHash("isGrounded");
        private static int isFallingHash = Animator.StringToHash("isFalling");
        private static int isJumpingHash = Animator.StringToHash("isJumping");

        private static int isAttackingHash = Animator.StringToHash("isAttacking");

        private Vector3 _currentBlendInput = Vector3.zero;

        private void Awake()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            _playerActionsInput = GetComponent<PlayerActionsInput>();
            _playerState = GetComponent<PlayerState>(); 
        }

        private void Update()
        {
            UpdateAnimationState();
        }

        private void UpdateAnimationState()
        { 
            bool isRunning = _playerState.CurrentPlayerMovementState == PlayerMovementState.Running;
            bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
            bool isJumping = _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping;
            bool isFalling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling;
            bool isGrounded = _playerState.InGroundedState();

            Vector2 inputTarget = isSprinting ? _playerLocomotionInput.MovementInput * 1.5f :
                                  isRunning ? _playerLocomotionInput.MovementInput * 1f : _playerLocomotionInput.MovementInput * 0.5f;

            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, _locomotionBlendSpeed * Time.deltaTime);

            _animator.SetBool(isGroundedHash, isGrounded); 
            _animator.SetBool(isFallingHash, isFalling);
            _animator.SetBool(isJumpingHash, isJumping);
            _animator.SetBool(isAttackingHash, _playerActionsInput.AttackPressed);

            _animator.SetFloat(inputXHash, _currentBlendInput.x);
            _animator.SetFloat(inputYHash, _currentBlendInput.y);
            _animator.SetFloat(inputMagnitudeHash, _currentBlendInput.magnitude);
        }
    }

}

