using Scripts.Environment;
using Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Utils
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text _coinsValue;
        [SerializeField] private Text _distanceValue;
        [SerializeField] private GameObject _timerToStart;

        [Space]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        [Space]
        [SerializeField] private Animator _runIconAnimator;

        private GameSession _gameSession;
        private PlayerController _playerController;
        private Text _timer;
        private FollowCamera _followCamera;
        private Vector3 _defaultCameraVector;
        private LevelComponent _levelComponent;

        private readonly static int StartRunIconKey = Animator.StringToHash("start");
        private readonly static int RunIconKey = Animator.StringToHash("run");

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _playerController = FindObjectOfType<PlayerController>();
            _timer = _timerToStart.GetComponent<Text>();
            _followCamera = FindObjectOfType<FollowCamera>();
            _levelComponent = FindObjectOfType<LevelComponent>();

            _defaultCameraVector = _followCamera.Offset;
        }

        private void Update()
        {
            _coinsValue.text = $"{_gameSession.Coins}";

            if ((int)_gameSession.OnStartDelay > 0)
            {
                _timer.text = $"{(int)_gameSession.OnStartDelay}";
            }
            else
            {
                _timer.color = Color.green;
                _timer.text = "GO!";
            }

            if (_playerController.IsRunning)
            {
                _runIconAnimator.SetBool(RunIconKey, false);

                _timerToStart.SetActive(false);
                _distanceValue.text = $"{_gameSession.Distance}";
            }

            if (_playerController.PlayerLose)
            {
                _runIconAnimator.SetBool(RunIconKey, true);

                _playButton.gameObject.SetActive(true);
                _playButton.interactable = false;

                _pauseButton.gameObject.SetActive(false);
                _restartButton.gameObject.SetActive(true);
                _backButton.gameObject.SetActive(true);
                _exitButton.gameObject.SetActive(true);

                _followCamera.Offset = new Vector3(0f, 2f, -3.5f);

                _levelComponent.StartSpawn = false;
                StopBiomeTimer(true);
            }

            if (_playerController.GameIsStarted)
            {
                _runIconAnimator.SetBool(StartRunIconKey, true);

                _pauseButton.interactable = true;
            }
        }

        public void OnPause()
        {
            //if (!_playerController.IsGrounded)
            //{
            //    _playerController.Animator.SetBool("fall", false);
            //}

            _levelComponent.StartSpawn = false;

            _runIconAnimator.SetBool(RunIconKey, true);

            _playerController.SetRunningState(false);
            SetRotation(0f, 180f, 0f);

            _followCamera.Offset = new Vector3(0f, 2f, -3.5f);

            StopBiomeTimer(true);
        }

        public void OnPlay()
        {
            _levelComponent.StartSpawn = true;

            _playerController.SetRunningState(true);
            SetRotation(0f, 0f, 0f);

            _followCamera.Offset = _defaultCameraVector;

            StopBiomeTimer(false);
        }

        public void OnRestart()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        public void OnBack()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnExit()
        {
            Application.Quit();
        }

        private void SetRotation(float x, float y, float z)
        {
            var rotation = new Vector3(x, y, z);
            _playerController.gameObject.transform.rotation = Quaternion.Euler(rotation);
        }

        private void StopBiomeTimer(bool state)
        {
            var biomes = FindObjectsOfType<Biome>();
            if (biomes.Length > 0)
            {
                foreach (var biome in biomes)
                {
                    biome.StopBiomeTimer = state;
                }
            }
        }
    }
}
