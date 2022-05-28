using Scripts.Utils;
using UnityEngine;

namespace Scripts.Objects
{
    public class CollectableComponent : MonoBehaviour
    {
        [SerializeField] private bool _isCoin;

        private AudioComponent _audioComponent;
        private GameSession _gameSession;

        private void Awake()
        {
            _audioComponent = FindObjectOfType<AudioComponent>();
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                _audioComponent.Play("coin", 0.2f);
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
