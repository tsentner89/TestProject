using UnityEngine;

namespace PlayerAbilities
{
    [RequireComponent(typeof(GroundChecker))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed,
            _jumpSpeed = 18f,
            _gravityFactor = 1f;
        
        private CharacterController _character;
        private GroundChecker _groundChecker;
        private Vector3 _moveDirection;
        private float _ySpeed;

        private void Awake()
        {
            _character = GetComponent<CharacterController>();
            _groundChecker = GetComponent<GroundChecker>();
        }

        private void Update()
        {
            var inputDirection = new Vector3(Input.GetAxis("Horizontal"), 
                0f, Input.GetAxis("Vertical"));
            var isJump = Input.GetButtonDown("Jump");
            Move(inputDirection, isJump);
        }

        private void Move(Vector3 inputDirection, bool isJump)
        {
            _moveDirection = GetMoveDirection(inputDirection);
            var distance = _moveDirection * _speed;
            distance += Vector3.up * CalculateYSpeed(isJump);
            _character.Move(distance * Time.deltaTime);
            _ySpeed = _character.velocity.y;
        }
        
        private float CalculateYSpeed(bool isJump)
        {
            if (_groundChecker.IsGround && isJump)
            {
                _ySpeed = _jumpSpeed;
            }
            
            _ySpeed += _gravityFactor * Physics.gravity.y * Time.deltaTime;

            return _ySpeed;
        }

        private Vector3 GetMoveDirection(Vector3 inputDirection)
        {
            Vector3 moveDirectionForward = transform.forward * inputDirection.z;
            Vector3 moveDirectionSide = transform.right * inputDirection.x;

            return (moveDirectionForward + moveDirectionSide).normalized;
        }
    }
}
