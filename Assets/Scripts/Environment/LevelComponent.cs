using System.Collections;
using UnityEngine;

namespace Scripts.Environment
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _sections;

        [Space]
        [SerializeField] private Transform _ground;

        [Space]
        [SerializeField] private bool _startSpawn = true;
        [SerializeField] private int _spawnSectionsOnStart;
        [SerializeField] private float _spawnSectionsDelay = 2f;
        [SerializeField] private float _sectionLong;
        [SerializeField] private float _indentOnStart;
        [SerializeField] private float _distanceBetweenLanes;

        public float DistanceBetweenLanes => _distanceBetweenLanes;
        public bool StartSpawn
        {
            get => _startSpawn;
            set => _startSpawn = value;
        }

        private float _zPosition;
        private bool _readyToSpawn;
        private const int _lanesAmount = 3;

        private void Awake()
        {
            _zPosition += _indentOnStart;

            if (_distanceBetweenLanes == 0)
                _distanceBetweenLanes = _ground.transform.localScale.x / _lanesAmount;

            for (int i = 0; i < _spawnSectionsOnStart; i++)
            {
                SpawnSection();
                _readyToSpawn = true;
            }
        }

        private void Update()
        {
            if (_startSpawn)
            {
                if (_readyToSpawn)
                {
                    _readyToSpawn = false;
                    StartCoroutine(GenerateSection());
                }
            }
        }

        private IEnumerator GenerateSection()
        {
            SpawnSection();

            yield return new WaitForSeconds(_spawnSectionsDelay);

            _readyToSpawn = true;
        }

        public void SpawnSection()
        {
            var randomIndex = Random.Range(0, _sections.Length);

            Instantiate(_sections[randomIndex], new Vector3(0, 0, _zPosition), Quaternion.identity);
            _zPosition += _sectionLong;
        }
    }
}
