using Scripts.Utils;
using UnityEngine;

namespace Scripts.Objects
{
    public class CollectableComponent : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private bool _isCoin;

        private AudioSource _audioSource;
        private GameSession _gameSession;

        private void Awake()
        {
            _audioSource = FindObjectOfType<AudioSource>();
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
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
