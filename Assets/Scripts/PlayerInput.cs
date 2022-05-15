using UnityEngine;

namespace Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            _playerController.LeftTurn = Input.GetKey(KeyCode.LeftArrow);
            _playerController.RightTurn = Input.GetKey(KeyCode.RightArrow);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerController.Jump = true;
            }
        }
    }
}
