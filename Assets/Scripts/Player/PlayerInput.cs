using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            _playerController.LeftTurn = Input.GetKeyDown(KeyCode.LeftArrow);
            _playerController.RightTurn = Input.GetKeyDown(KeyCode.RightArrow);
            _playerController.Jump = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
