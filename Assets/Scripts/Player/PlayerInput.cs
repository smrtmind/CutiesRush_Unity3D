using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController _playerController;
        private SwipeManager _swipeManager;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _swipeManager = GetComponent<SwipeManager>();
        }

        private void Update()
        {
            //mobile controls
            _playerController.LeftTurn = _swipeManager.SwipeLeft;
            _playerController.RightTurn = _swipeManager.SwipeRight;
            _playerController.Jump = _swipeManager.SwipeUp;
            _playerController.SlideDown = _swipeManager.SwipeDown;

            //pc controls
            //_playerController.LeftTurn = Input.GetKeyDown(KeyCode.LeftArrow);
            //_playerController.RightTurn = Input.GetKeyDown(KeyCode.RightArrow);
            //_playerController.Jump = Input.GetKeyDown(KeyCode.UpArrow);
            //_playerController.SlideDown = Input.GetKeyDown(KeyCode.DownArrow);
        }
    }
}
