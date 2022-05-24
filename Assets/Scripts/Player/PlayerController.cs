using Scripts.Environment;
using Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _maxSpeed = 20f;
        [SerializeField] private float _increaseSpeedPerSec = 0.05f;
        [SerializeField] private float _turnSpeed = 5f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _slideDuration = 0.5f;

        [Header("Checkers")]
        [SerializeField] private LayerCheck _groundCheck;

        private static readonly int RunKey = Animator.StringToHash("run");
        private static readonly int JumpKey = Animator.StringToHash("jump");
        private static readonly int SlideKey = Animator.StringToHash("slide");
        private static readonly int FallKey = Animator.StringToHash("fall");
        private static readonly int LoseKey = Animator.StringToHash("lose");

        //pc controls
        public bool LeftTurn { get; set; }
        public bool RightTurn { get; set; }
        public bool Jump { get; set; }
        public bool SlideDown { get; set; }

        private LevelComponent _levelComponent;
        private bool _isRunning;
        private bool _isSliding;
        private GameSession _gameSession;
        private Rigidbody _rigidbody;
        private bool _isGrounded;
        private bool _gameIsStarted;
        private bool _playerLose;
        private float _currentLane;
        private CapsuleCollider _collider;
        private Vector3 _defaultColliderCenter;
        private float _defaultColliderHeight;
        private HudController _hudController;
        private Animator _animator;

        public bool PlayerLose
        {
            get => _playerLose;
            set => _playerLose = value;
        }
        public bool GameIsStarted => _gameIsStarted;
        public bool IsRunning => _isRunning;
        public bool IsGrounded => _isGrounded;
        public Rigidbody Rigidbody => _rigidbody;

        private void Awake()
        {
            _animator = FindObjectOfType<Animator>();
            _collider = GetComponent<CapsuleCollider>();
            _defaultColliderCenter = _collider.center;
            _defaultColliderHeight = _collider.height;

            _hudController = FindObjectOfType<HudController>();
            _gameSession = FindObjectOfType<GameSession>();
            _rigidbody = GetComponent<Rigidbody>();
            _levelComponent = FindObjectOfType<LevelComponent>();
        }

        private void Update()
        {
            if (_speed < _maxSpeed && !_hudController.IsOnPause && _gameIsStarted)
                _speed += _increaseSpeedPerSec * Time.deltaTime;

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

                if (_isGrounded && !_isSliding)
                {
                    if (SlideDown)
                    {
                        StartCoroutine(Slide());
                    }

                    if (Jump)
                    {
                        _animator.SetTrigger(JumpKey);
                        _rigidbody.velocity = Vector3.up * _jumpForce;
                    }

                    else
                    {
                        _animator.SetBool(FallKey, false);
                    }
                }
                else
                {
                    _animator.SetBool(FallKey, true);
                }
            }

            if (_playerLose)
            {
                _isRunning = false;
                _animator.SetBool(LoseKey, true);
            }
        }

        private IEnumerator Slide()
        {
            _isSliding = true;
            _animator.SetBool(SlideKey, true);
            _collider.center = new Vector3(0f, 0.5f, 0f);
            _collider.height = 1f;

            yield return new WaitForSeconds(_slideDuration);

            _collider.center = _defaultColliderCenter;
            _collider.height = _defaultColliderHeight;
            _animator.SetBool(SlideKey, false);
            _isSliding = false;
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
