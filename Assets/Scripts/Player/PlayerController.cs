using Scripts.Environment;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _turnSpeed = 5f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private Animator _animator;

        [Header("Checkers")]
        [SerializeField] private LayerCheck _groundCheck;

        private static readonly int RunKey = Animator.StringToHash("run");
        private static readonly int JumpKey = Animator.StringToHash("jump");
        private static readonly int FallKey = Animator.StringToHash("fall");
        private static readonly int LoseKey = Animator.StringToHash("lose");

        public bool LeftTurn { get; set; }
        public bool RightTurn { get; set; }
        public bool Jump { get; set; }

        private LevelBoundaries _lvlBounds;
        private bool _isRunning;
        private GameSession _gameSession;
        private Rigidbody _rigidbody;
        private bool _isGrounded;
        private bool _gameIsStarted;

        public bool GameIsStarted => _gameIsStarted;
        public bool IsRunning => _isRunning;
        public bool IsGrounded => _isGrounded;

        private void Awake()
        {
            _lvlBounds = FindObjectOfType<LevelBoundaries>();
            _gameSession = FindObjectOfType<GameSession>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _isGrounded = _groundCheck.IsTouchingLayer;

            if (!_gameIsStarted)
            {
                if (_gameSession.OnStartDelay > 0)
                {
                    _gameSession.OnStartDelay -= Time.deltaTime;
                }
                else
                {
                    _gameSession.OnStartDelay = 0;

                    _isRunning = true;
                    _gameIsStarted = true;
                }
            }

            if (_isRunning)
            {
                _animator.SetBool(RunKey, true);
                transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.World);

                if (LeftTurn)
                {
                    if (transform.position.x > _lvlBounds.GroundBorder * -1)
                    {
                        transform.Translate(Vector3.left * _turnSpeed * Time.deltaTime);
                    }
                }
                if (RightTurn)
                {
                    if (transform.position.x < _lvlBounds.GroundBorder)
                    {
                        transform.Translate(Vector3.right * _turnSpeed * Time.deltaTime);
                    }
                }
                if (Jump && _isGrounded)
                {
                    _animator.SetTrigger(JumpKey);
                    _rigidbody.velocity = Vector3.up * _jumpForce;
                }
                else if (!_isGrounded)
                {
                    _animator.SetBool(FallKey, true);
                }
                else if (_isGrounded)
                {
                    _animator.SetBool(FallKey, false);
                }
            }

            if (_gameSession.Health <= 0)
            {
                _isRunning = false;
                _animator.SetBool(LoseKey, true);
            }

            //if (transform.position.y < -5f)
            //{
            //    Debug.Log("Game Over");
            //}
        }

        public void SetRunningState(bool state)
        {
            _isRunning = state;
            _animator.SetBool(RunKey, state);
        }
    }
}
