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

        private LevelComponent _levelComponent;
        private bool _isRunning;
        private GameSession _gameSession;
        private Rigidbody _rigidbody;
        private bool _isGrounded;
        private bool _gameIsStarted;
        private bool _playerLose;
        private float _currentLane;

        public bool GameIsStarted => _gameIsStarted;
        public bool IsRunning => _isRunning;
        public bool IsGrounded => _isGrounded;
        public bool PlayerLose => _playerLose;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _rigidbody = GetComponent<Rigidbody>();
            _levelComponent = FindObjectOfType<LevelComponent>();
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

                if (LeftTurn)
                {
                    if (_currentLane > -1)
                        _currentLane -= _levelComponent.DistanceBetweenLanes;
                }

                if (RightTurn)
                {
                    if (_currentLane < 1)
                        _currentLane += _levelComponent.DistanceBetweenLanes;
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
                _playerLose = true;

                _isRunning = false;
                _animator.SetBool(LoseKey, true);
            }

            //if (transform.position.y < -5f)
            //{
            //    Debug.Log("Game Over");
            //}
        }

        private void FixedUpdate()
        {
            if (_isRunning)
            {
                _rigidbody.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
            }

            Vector3 newPosition = _rigidbody.position;
            newPosition.x = Mathf.MoveTowards(newPosition.x, _currentLane, _turnSpeed * Time.deltaTime);
            _rigidbody.position = newPosition;
        }
        public void SetRunningState(bool state)
        {
            _isRunning = state;
            _animator.SetBool(RunKey, state);
        }
    }
}
