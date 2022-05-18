using UnityEngine;

namespace Scripts.Utils
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private bool _isTouchingLayer;

        public bool IsTouchingLayer => _isTouchingLayer;

        private Collider _collider;
        private float _distToGround = 1f;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            var distToGround = _collider.bounds.extents.y;
        }

        private void Update()
        {
            _isTouchingLayer = Physics.Raycast(transform.position, -Vector3.up, _distToGround + 0.1f);
        }
    }
}
