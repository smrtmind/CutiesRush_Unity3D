using Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Utils
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text _coinsValue;
        [SerializeField] private Text _distanceValue;

        private GameSession _gameSession;
        private PlayerController _playerController;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _playerController = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            _coinsValue.text = $"{_gameSession.Coins}";

            if (_playerController.IsRunning)
                _distanceValue.text = $"{_gameSession.Distance}";
        }
    }
}
