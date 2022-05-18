using Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Utils
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text _coinsValue;
        [SerializeField] private Text _distanceValue;
        [SerializeField] private GameObject _timerToStart;

        private GameSession _gameSession;
        private PlayerController _playerController;
        private Text _timer;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _playerController = FindObjectOfType<PlayerController>();
            _timer = _timerToStart.GetComponent<Text>();
        }

        private void Update()
        {
            _coinsValue.text = $"{_gameSession.Coins}";

            if ((int)_gameSession.OnStartDelay <= 0)
            {
                _timer.color = Color.green;
                _timer.text = "Go";
            }
            else
            {
                _timer.text = $"{(int)_gameSession.OnStartDelay}";
            }

            if (_playerController.IsRunning)
            {
                _timerToStart.SetActive(false);
                _distanceValue.text = $"{_gameSession.Distance}";
            }
                
        }
    }
}
