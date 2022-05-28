using Scripts.Player;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Environment
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _sections;

        [Space]
        [SerializeField] private Transform _ground;

        [Space]
        [SerializeField] private int _spawnSectionsOnStart;
        [SerializeField] private float _sectionLong;
        [SerializeField] private float _indentOnStart;
        [SerializeField] private float _distanceBetweenLanes;

        public float DistanceBetweenLanes => _distanceBetweenLanes;

        private float _zPosition;
        private const int _lanesAmount = 3;
        private GameConstructor _gameConstructor;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _gameConstructor = FindObjectOfType<GameConstructor>();
            SpawnPlayer();

            _zPosition += _indentOnStart;

            if (_distanceBetweenLanes == 0)
                _distanceBetweenLanes = _ground.transform.localScale.x / _lanesAmount;

            for (int i = 0; i < _spawnSectionsOnStart - 1; i++)
                SpawnSection();
        }

        public void SpawnSection()
        {
            var randomIndex = Random.Range(0, _sections.Length);

            Instantiate(_sections[randomIndex], new Vector3(0, 0, _zPosition), Quaternion.identity);
            _zPosition += _sectionLong;
        }

        public void SpawnPlayer()
        {
            GameObject player;

            if (!_gameConstructor.Player)
            {
                var randomIndex = Random.Range(0, _gameConstructor.PlayerSkin.Length);
                player = _gameConstructor.PlayerSkin[randomIndex];
            }
            else
            {
                player = _gameConstructor.Player;
            }

            Instantiate(player, _playerController.transform);
        }
    }
}
