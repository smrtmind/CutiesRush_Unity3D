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
        }

        private void Update()
        {
            var playerWidth = _player.gameObject.transform.localScale.x;

            _groundBorder = (_ground.transform.localScale.x / 2f) - playerWidth / 4f;
        }
    }
}
