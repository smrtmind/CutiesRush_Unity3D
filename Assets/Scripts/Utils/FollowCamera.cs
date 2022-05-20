using Scripts.Player;
using UnityEngine;

namespace Scripts.Utils
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        public Vector3 Offset
        {
            get => _offset;
            set => _offset = value;
        }

        private Transform _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>().gameObject.transform;
        }

        private void Update()
        {
            transform.position = _player.position + _offset;
        }
    }
}
