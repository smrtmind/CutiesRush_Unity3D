using Scripts.Player;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Objects
{
    public class CollectableComponent : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private bool _isCoin;

        private PlayerController _player;
        private AudioSource _audioSource;
        private GameSession _gameSession;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
            _audioSource = FindObjectOfType<AudioSource>();
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_player)
                FindObjectOfType<PlayerController>();

            if (_player)
            {
                _audioSource.PlayOneShot(_clip);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_isCoin)
                _gameSession.AddCoin(1);
        }
    }
}
