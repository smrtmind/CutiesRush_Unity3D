using UnityEngine;

namespace Scripts.Player
{
    public class SwipeManager : MonoBehaviour
    {
        private bool _swipeLeft;
        private bool _swipeRight;
        private bool _swipeUp;
        private bool _swipeDown;
        private bool _isDraging = false;
        private Vector2 _startTouch;
        private Vector2 _swipeDelta;

        public bool SwipeLeft => _swipeLeft;
        public bool SwipeRight => _swipeRight;
        public bool SwipeUp => _swipeUp;
        public bool SwipeDown => _swipeDown;

        private void Update()
        {
            _swipeDown = _swipeUp = _swipeLeft = _swipeRight = false;
            #region Standalone Inputs
            if (Input.GetMouseButtonDown(0))
            {
                _isDraging = true;
                _startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isDraging = false;
                Reset();
            }
            #endregion

            #region Mobile Input
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _isDraging = true;
                    _startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    _isDraging = false;
                    Reset();
                }
            }
            #endregion

            _swipeDelta = Vector2.zero;
            if (_isDraging)
            {
                if (Input.touches.Length > 0)
                    _swipeDelta = Input.touches[0].position - _startTouch;
                else if (Input.GetMouseButton(0))
                    _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }

            if (_swipeDelta.magnitude > 125)
            {
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                        _swipeLeft = true;
                    else
                        _swipeRight = true;
                }
                else
                {
                    if (y < 0)
                        _swipeDown = true;
                    else
                        _swipeUp = true;
                }

                Reset();
            }
        }

        private void Reset()
        {
            _startTouch = _swipeDelta = Vector2.zero;
            _isDraging = false;
        }
    }
}
