using UnityEngine;

namespace Scripts.Environment
{
    public class Section : MonoBehaviour
    {
        [SerializeField] private float _lifeSpan = 30f;

        private void Awake()
        {
            Destroy(gameObject, _lifeSpan);
        }
    }
}
