using UnityEngine;

namespace Scripts.Utils
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private bool _isTouchingLayer;
        [SerializeField] private float _beamLength = 1f;
        [SerializeField] private Direction _direction;
        [SerializeField] private Color _color = Color.green;

        public bool IsTouchingLayer => _isTouchingLayer;

        private Collider _collider;
        private Vector3 _defaultColliderBounds;
        private Vector3 _raycastDirection;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _defaultColliderBounds = _collider.bounds.extents;
        }

        private void Update()
        {
            if (_direction == Direction.up)
                _raycastDirection = Vector3.up;

            if (_direction == Direction.down)
                _raycastDirection = Vector3.down;

            if (_direction == Direction.left)
                _raycastDirection = Vector3.left;

            if (_direction == Direction.right)
                _raycastDirection = Vector3.right;

            _isTouchingLayer = Physics.Raycast(transform.position, transform.TransformDirection(_raycastDirection), _beamLength);
            Debug.DrawRay(transform.position, transform.TransformDirection(_raycastDirection) * _beamLength, _color);
        }
        private enum Direction
        {
            up,
            down,
            left,
            right
        }
    }
}
