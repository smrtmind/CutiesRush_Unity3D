using UnityEngine;

namespace Scripts.Environment
{
    public class Biome : MonoBehaviour
    {
        [SerializeField] private GameObject _biome;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(_biome);
            }
        }
    }
}
