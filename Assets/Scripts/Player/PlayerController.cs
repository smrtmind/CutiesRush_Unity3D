using Scripts.Environment;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _turnSpeed = 5f;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _onStartDelay = 3f;

        private static readonly int RunKey = Animator.StringToHash("run");
        private static readonly int JumpKey = Animator.StringToHash("jump");
        private static readonly int SlideKey = Animator.StringToHash("slide");
        private static readonly int HitKey = Animator.StringToHash("hit");

        public bool LeftTurn { get; set; }
        public bool RightTurn { get; set; }
        public bool Jump { get; set; }
        public bool IsHit { get; set; }

        private LevelBoundaries _lvlBounds;
        private Animator _animator;
        private bool _isRunning;

        private void Awake()
        {
            _lvlBounds = FindObjectOfType<LevelBoundaries>();
            _animator = FindObjectOfType<Animator>();
        }

        private void Update()
        {
            if (_onStartDelay > 0)
            {
                _onStartDelay -= Time.deltaTime;
            }

            if (_onStartDelay <= 0)
            {
                _onStartDelay = 0;

                _isRunning = true;
            }

            if (transform.position.y < -5f)
            {
                Debug.Log("Game Over");
            }
        }

        private void FixedUpdate()
        {
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
                if (Jump)
                {
                    _animator.SetTrigger(JumpKey);
                    //_playerBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                    //_playerBody.AddForce(0, (_strafeSpeed * 100) * Time.deltaTime, 0, ForceMode.VelocityChange);

                    Jump = false;
                }
            }

            if (IsHit)
            {
                _isRunning = false;
                _animator.SetBool(RunKey, false);

                _animator.SetTrigger(HitKey);
            }
        }
    }
}
