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
                if (_item == Item.Coin)
                {
                    _audioComponent.Play("coin", 0.2f);
                    _gameSession.AddCoin(1);

                    Destroy(gameObject);
                }
            }
        }

        private enum Item
        {
            Coin,
            SpeedBoost,
            Immortality
        }
    }
}
