using UnityEngine;

namespace Scripts
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

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
