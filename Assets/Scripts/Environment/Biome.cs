using UnityEngine;

namespace Scripts.Environment
{
    public class Biome : MonoBehaviour
    {
        private float _biomeLifeSpan;
        private bool _stopBiomeTimer;

        public bool StopBiomeTimer
        {
            get => _stopBiomeTimer;
            set => _stopBiomeTimer = value;
        }

        private void Awake()
        {
            _biomeLifeSpan = FindObjectOfType<LevelComponent>().BiomeLifeSpan;
        }

        private void Update()
        {
            if (!_stopBiomeTimer)
            {
                if (_biomeLifeSpan > 0)
                    _biomeLifeSpan -= Time.deltaTime;
                
                if (_biomeLifeSpan <= 0)
                    Destroy(gameObject);
            }
        }
    }
}
