using Scripts.Player;
using UnityEngine;

namespace Scripts
{
    public class ObstacleComponent : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.IsHit = true;
            }
        }
    }
}
