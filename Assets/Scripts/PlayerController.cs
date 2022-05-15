using UnityEngine;

namespace Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _strafeSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _onStartDelay = 3f;

        private static readonly int RunKey = Animator.StringToHash("run");
        private static readonly int JumpKey = Animator.StringToHash("jump");
        private static readonly int HitKey = Animator.StringToHash("hit");

        public bool LeftTurn { get; set; }
        public bool RightTurn { get; set; }
        public bool Jump { get; set; }

        private Rigidbody _playerBody;
        private Animator _animator;
        private bool _isRunning;

        public bool _isHit { get; set; }

        private void Awake()
        {
            _playerBody = GetComponent<Rigidbody>();
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
                _playerBody.AddForce(0, 0, (_speed * 10000) * Time.deltaTime);

                if (LeftTurn)
                {
                    _playerBody.AddForce((-_strafeSpeed * 100) * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                }
                if (RightTurn)
                {
                    _playerBody.AddForce((_strafeSpeed * 100) * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                }
                if (Jump)
                {
                    _animator.SetTrigger(JumpKey);
                    _playerBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

                    Jump = false;
                }
            }

            if (_isHit)
            {
                _isRunning = false;
                _animator.SetBool(RunKey, false);

                _animator.SetTrigger(HitKey);
            }
        }
    }
}
