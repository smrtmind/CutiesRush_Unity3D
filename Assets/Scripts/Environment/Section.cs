using UnityEngine;

namespace Scripts.Environment
{
    public class Section : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 60f);
        }
    }
}
