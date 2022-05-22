using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Utils
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainSettings;
        [SerializeField] private GameObject _playerType;
        [SerializeField] private GameObject _chooseLevel;
        [SerializeField] private GameObject _loadingOverlay;
        [SerializeField] private float _loadingDelay = 3f;

        private bool _startPressed;

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
                    SceneManager.LoadScene("EndlessForest");
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
            _mainSettings.SetActive(false);
            _playerType.SetActive(true);
            _chooseLevel.SetActive(false);
        }

        public void OnLevelSettings()
        {
            _mainSettings.SetActive(false);
            _playerType.SetActive(false);
            _chooseLevel.SetActive(true);
        }

        public void OnExit()
        {
            Application.Quit();
        }

        public void OnBack()
        {
            _mainSettings.SetActive(true);
            _playerType.SetActive(false);
            _chooseLevel.SetActive(false);
        }
    }
}
