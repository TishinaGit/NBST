using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller
{
    [DefaultExecutionOrder(-1)]
    public class PlayerController : MonoBehaviour
    {
        private const float ZeroF = 0f;

        [Header("Components")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private Transform _mainCam;


        [Header("Settings")]
        public float _rotationSpeed = 15f;
        public float _verticalVelocity = 0f;
        public float _jumpSpeed = 2.0f;
        public float _antiBump;
        public float _gravity = 25f;

        public float _runSpeed = 4f;
        public float _sprintSpeed = 6f;
        public float _drag = 20f;

        private PlayerLocomotionInput _playerLocomotionInput;
        private PlayerState _playerState;

        private void Awake()
        {
             _antiBump = _sprintSpeed;
            _playerState = GetComponent<PlayerState>();
            _mainCam = _playerCamera.transform;
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();

        }


        private void Update()
        {
            UpdateMovementState();
            HandleVerticalVelocity();
            Movement();
            
        }

        public void UpdateMovementState()
        {
            bool isMovementInput = _playerLocomotionInput.MovementInput != Vector2.zero; 
            bool isSprinting = _playerLocomotionInput.isSprintToggledOn;
            bool isGrounded = IsGrounded();

            PlayerMovementState SprintAndRunState = isSprinting ? PlayerMovementState.Sprinting :  isMovementInput ? PlayerMovementState.Running : PlayerMovementState.Idling;
            _playerState.SetPlayerMovementState(SprintAndRunState);
            
            if (!isGrounded && _characterController.velocity.y > 0f)
            {
                _playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
            }
            else if (!isGrounded && _characterController.velocity.y <= 0f)
            {
                _playerState.SetPlayerMovementState(PlayerMovementState.Falling);
            }
        }

        public void HandleVerticalVelocity()
        {
            bool isGrounded = _playerState.InGroundedState();

            _verticalVelocity -= _gravity * Time.deltaTime;

            if (isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = -_antiBump;;
            }

            if (_playerLocomotionInput.JumpPressed && isGrounded)
            {
                _verticalVelocity += Mathf.Sqrt(_jumpSpeed * 3 * _gravity);
            }
        }

        public void Movement()
        {
            bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
           
            float clampLateralMagnitude = isSprinting ? _sprintSpeed : _runSpeed;

            var movementDirection = new Vector3(_playerLocomotionInput.MovementInput.x, 0f, _playerLocomotionInput.MovementInput.y).normalized;
            var adjustedDirection = Quaternion.AngleAxis(_mainCam.eulerAngles.y, Vector3.up) * movementDirection;
            Vector3 movementDelta = movementDirection * Time.deltaTime;
            Vector3 newVelocity = _characterController.velocity + movementDelta;

            Vector3 currentDrag = newVelocity.normalized * _drag * Time.deltaTime;
            newVelocity = (newVelocity.magnitude > _drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
            newVelocity = Vector3.ClampMagnitude(newVelocity, clampLateralMagnitude);
            newVelocity.y += _verticalVelocity;
            _characterController.Move(newVelocity * Time.deltaTime);

            if (adjustedDirection.magnitude > ZeroF)
            {
                var adjusteMovement = adjustedDirection * (clampLateralMagnitude * Time.deltaTime);
                adjusteMovement.y += _verticalVelocity * Time.deltaTime;
                _characterController.Move(adjusteMovement);
                HandleRotation(adjustedDirection);
            }
        }

        public void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            transform.LookAt(transform.position + adjustedDirection);
        }

        private bool IsGrounded()
        {
            return _characterController.isGrounded;
        }
    }

}
