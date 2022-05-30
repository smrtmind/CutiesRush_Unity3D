using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Utils
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainSettings;
        [SerializeField] private GameObject _playerType;
        [SerializeField] private GameObject _kittySkins;
        [SerializeField] private GameObject _doggySkins;
        [SerializeField] private GameObject _chooseLevel;
        [SerializeField] private GameObject _loadingOverlay;
        [SerializeField] private float _loadingDelay = 3f;

        private bool _startPressed;
        private GameConstructor _gameConstructor;
        private string _levelName;

        private void Awake()
        {
            _gameConstructor = FindObjectOfType<GameConstructor>();
        }

        private void Update()
        {
            if (_startPressed)
            {
                if (_loadingDelay > 0)
                {
                    _loadingDelay -= Time.deltaTime;
                }
                else
                {
                    if (_levelName == null)
                    {
                        var randomIndex = Random.Range(0, _gameConstructor.LevelName.Length);
                        _levelName = _gameConstructor.LevelName[randomIndex];
                    }

                    SceneManager.LoadScene(_levelName);
                }
            }
        }

        public void OnStartGame()
        {
            _startPressed = true;
            _loadingOverlay.SetActive(true);
        }

        public void OnPlayerEdit()
        {
            SetGoState(false, _mainSettings, _kittySkins, _doggySkins, _chooseLevel);
            _playerType.SetActive(true);
        }

        public void OnLevelSettings()
        {
            SetGoState(false, _mainSettings, _playerType, _kittySkins, _doggySkins);
            _chooseLevel.SetActive(true);
        }

        public void OnMainMenu()
        {
            SetGoState(false, _chooseLevel, _playerType, _kittySkins, _doggySkins);
            _mainSettings.SetActive(true);
        }

        public void OnExit() => Application.Quit();

        public void SetPlayerModel(int value)
        {
            _gameConstructor.SetPlayer(value);
        }

        public void SetLevel(string name) => _levelName = name;

        private void SetGoState(bool state, params GameObject[] gos)
        {
            foreach (var go in gos)
                go.SetActive(state);
        }
    }
}
