using Scripts.Player;
using UnityEngine;

namespace Scripts.Objects
{
    public class ObstacleComponent : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                _playerController.PlayerLose = true;
            }
        }
    }
}
