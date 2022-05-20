using Scripts.Player;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Objects
{
    public class ObstacleComponent : MonoBehaviour
    {
        private Collider _collider;
        private GameSession _gameSession;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _gameSession = FindObjectOfType<GameSession>();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                _gameSession.ModifyHealth(-1);
            }
        }
    }
}
