using Scripts.Player;
using UnityEngine;

namespace Scripts.Objects
{
    public class CollectableComponent : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private PlayerController _player;
        private AudioSource _audioSource;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
            _audioSource = FindObjectOfType<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_player)
                FindObjectOfType<PlayerController>();

            if (_player)
            {
                _audioSource.PlayOneShot(_clip);
                Destroy(gameObject);
            }
        }
    }
}
