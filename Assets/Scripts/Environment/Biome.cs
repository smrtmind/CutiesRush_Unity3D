using UnityEngine;

namespace Scripts.Environment
{
    public class Biome : MonoBehaviour
    {
        private LevelComponent _levelComponent;

        private void Awake()
        {
            _levelComponent = FindObjectOfType<LevelComponent>();

            Destroy(gameObject, _levelComponent.BiomeLifeSpan);
        }
    }
}
