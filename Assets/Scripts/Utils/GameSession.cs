using Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Utils
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private int _coins;
        [SerializeField] private int _distance;
        [SerializeField] private float _addDistanceDelay = 0.3f;
        [SerializeField] private float _onStartDelay = 3f;

        public float OnStartDelay
        {
            get => _onStartDelay;
            set => _onStartDelay = value;
        }
        public int Coins => _coins;
        public int Distance => _distance;

        private bool _addDistance = false;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _onStartDelay++;
        }

        private void Update()
        {
            if (_playerController.IsRunning)
            {
                if (!_addDistance)
                {
                    _addDistance = true;
                    StartCoroutine(AddDistance());
                }
            }
        }

        private IEnumerator AddDistance()
        {
            _distance++;

            yield return new WaitForSeconds(_addDistanceDelay -= _playerController.IncreaseSpeedPerSec * Time.deltaTime);

            _addDistance = false;
        }

        public void AddCoin(int value) => _coins += value;
    }
}
