using Scripts.Utils;
using UnityEngine;

namespace Scripts.Objects
{
    public class CollectableComponent : MonoBehaviour
    {
        [SerializeField] private Item _item;

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
            if (_item == Item.Coin)
                _gameSession.AddCoin(1);
        }

        private enum Item
        {
            Coin,
            SpeedBoost,
            Immortality
        }
    }
}
