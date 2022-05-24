using UnityEngine;

namespace Scripts.Utils
{
    public class GameConstructor : MonoBehaviour
    {
        [SerializeField] private GameObject[] _playerSkin;
        [SerializeField] private string[] _levelName;

        private GameObject _player;
        public GameObject Player => _player;
        public GameObject[] PlayerSkin => _playerSkin;
        public string[] LevelName => _levelName;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void SetPlayer(int value) => _player = _playerSkin[value];
    }
}
