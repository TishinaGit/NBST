using UnityEngine; 

namespace Controller
{
    [DefaultExecutionOrder(-1)]
    public class PlayerController : MonoBehaviour
    {
        private const float Zero = 0f;
        

        [Header("Components")]
        [SerializeField] private CharacterController _characterController; 
        [SerializeField] private PlayerLocomotionInput _playerLocomotionInput;
        [SerializeField] private PlayerState _playerState;
        [SerializeField] private Transform _mainCam;

        [Header("MovementSettings")]
        [SerializeField] private float RunSpeed = 10f;
        [SerializeField] private float SprintSpeed = 20f;

        [SerializeField] private float _jumpSpeed = 1.0f;
        [SerializeField] private float _gravity = 25f;
        private float _verticalVelocity = 0f;
        private float _antiBump;
        private float _stepOffset;
         
        private bool _jumpedLastFrame = false;
        private PlayerMovementState _lastMovementState = PlayerMovementState.Falling;

        [Header("Environment Details")]
        [SerializeField] private LayerMask _groundLayers;
        private void Awake()
        {
            //_playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            //_playerState = GetComponent<PlayerState>();
            _antiBump = SprintSpeed;
            _stepOffset = _characterController.stepOffset;
        } 
  
        private void Update()
        {
            UpdateMovementState();
            HandleVerticalVelocity();
            PlayerMovement();
        }

        private void UpdateMovementState()
        {
            _lastMovementState = _playerState.CurrentPlayerMovementState;
             
            bool isMovementInput = _playerLocomotionInput.MovementInput != Vector2.zero;                                                    
            bool isSprinting = _playerLocomotionInput.IsSprint;            
            bool isGrounded = IsGrounded();

            PlayerMovementState PlayerState = isSprinting ? PlayerMovementState.Sprinting :
                                              isMovementInput ? PlayerMovementState.Running : PlayerMovementState.Idling;

            _playerState.SetPlayerMovementState(PlayerState);

            if ((!isGrounded || _jumpedLastFrame) && _characterController.velocity.y > 0f)
            {
                _playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
                _jumpedLastFrame = false;
                _characterController.stepOffset = 0f;
            }
            else if ((!isGrounded || _jumpedLastFrame) && _characterController.velocity.y <= 0f)
            {
                _playerState.SetPlayerMovementState(PlayerMovementState.Falling);
                _jumpedLastFrame = false;
                _characterController.stepOffset = 0f;
            }
            else
            {
                _characterController.stepOffset = _stepOffset;
            }
        }

        public void HandleVerticalVelocity()
        {
            bool isGrounded = _playerState.InGroundedState();

            _verticalVelocity -= _gravity * Time.deltaTime;

            if (isGrounded && _verticalVelocity < 0)
                _verticalVelocity = -_antiBump;

            if (_playerLocomotionInput.IsJump && isGrounded)
            {
                _verticalVelocity += Mathf.Sqrt(_jumpSpeed * 3 * _gravity);
                _jumpedLastFrame = true;
            }

            if (_playerState.IsStateGroundedState(_lastMovementState) && !isGrounded)
            {
                _verticalVelocity += _antiBump;
            }
        }

        private void PlayerMovement()
        {      
            bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
            bool isGrounded = _playerState.InGroundedState();
            bool isRuning = _playerState.CurrentPlayerMovementState == PlayerMovementState.Running;

            float clampLateralMagnitude = !isGrounded ? SprintSpeed :
                              isRuning ? RunSpeed :
                              isSprinting ? SprintSpeed : RunSpeed;
              
            var movementInput = new Vector3(_playerLocomotionInput.MovementInput.x, 0f, _playerLocomotionInput.MovementInput.y).normalized;  
            var adjustedDirection = Quaternion.AngleAxis(_mainCam.eulerAngles.y, Vector3.up) * movementInput; 

            var adjusteMovement = adjustedDirection * (clampLateralMagnitude * Time.deltaTime);
            adjusteMovement.y += _verticalVelocity * Time.deltaTime;
            adjusteMovement = !isGrounded ? HandleSteepWalls(adjusteMovement) : adjusteMovement;

            _characterController.Move(adjusteMovement);
            HandleRotation(adjustedDirection); 
        }

        private Vector3 HandleSteepWalls(Vector3 velocity)
        {
            Vector3 normal = CharacterControllerUtils.GetNormalWithSphereCast(_characterController, _groundLayers);
            float angle = Vector3.Angle(normal, Vector3.up);
            bool validAngle = angle <= _characterController.slopeLimit;

            if (!validAngle && _verticalVelocity < 0f)
                velocity = Vector3.ProjectOnPlane(velocity, normal);

            return velocity;
        }

        public void HandleRotation(Vector3 adjustedDirection )
        {
            if (adjustedDirection != Vector3.zero)
            {
                if (adjustedDirection.magnitude > Zero)
                { 
                    var targetRotation = Quaternion.LookRotation(adjustedDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime);
                    transform.LookAt(transform.position + adjustedDirection); 
                } 
            } 
        }

        private bool IsGrounded()
        {
            bool grounded = _playerState.InGroundedState() ? IsGroundedWhileGrounded() : IsGroundedWhileAirborne();

            return grounded;
        }
         
        private bool IsGroundedWhileGrounded()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _characterController.radius, transform.position.z);

            bool grounded = Physics.CheckSphere(spherePosition, _characterController.radius, _groundLayers, QueryTriggerInteraction.Ignore);

            return grounded;
        }

        private bool IsGroundedWhileAirborne()
        {
            Vector3 normal = CharacterControllerUtils.GetNormalWithSphereCast(_characterController, _groundLayers);
            float angle = Vector3.Angle(normal, Vector3.up); 
            bool validAngle = angle <= _characterController.slopeLimit;

            return _characterController.isGrounded && validAngle;
        }  
    }
}  

