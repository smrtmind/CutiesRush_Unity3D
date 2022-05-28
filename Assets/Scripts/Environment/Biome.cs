using UnityEngine;

namespace Scripts.Environment
{
    public class Biome : MonoBehaviour
    {
        [SerializeField] private GameObject _biome;

        private LevelComponent _level;
        private bool _levelSpawned = false;

        private void Awake()
        {
            _level = FindObjectOfType<LevelComponent>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (!_levelSpawned)
                {
                    _level.SpawnSection();
                    _levelSpawned = true;
                }

                Destroy(_biome);
            }
        }
    }
}
