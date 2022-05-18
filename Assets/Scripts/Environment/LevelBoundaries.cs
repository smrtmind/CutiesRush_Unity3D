using Scripts.Player;
using UnityEngine;

namespace Scripts.Environment
{
    public class LevelBoundaries : MonoBehaviour
    {
        [SerializeField] private float _groundBorder;
        [SerializeField] private Transform _ground;

        public float GroundBorder => _groundBorder;

        private PlayerController _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();

            var playerWidth = _player.gameObject.transform.localScale.x;
            var groundWidth = _ground.transform.localScale.x;

            _groundBorder = (groundWidth / 2f) - playerWidth;
        }
    }
}
