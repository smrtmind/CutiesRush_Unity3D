using System.Collections;
using UnityEngine;

namespace Scripts.Utils
{
    public class GameSession : MonoBehaviour
    {
        //distance
        private int _distance;
        private bool _addDistance = false;

        private void Update()
        {
            if (!_addDistance)
            {
                _addDistance = true;
                StartCoroutine(AddDistance());
            }
        }

        private IEnumerator AddDistance()
        {
            _distance++;

            yield return new WaitForSeconds(0.5f);

            _addDistance = false;
        }
    }
}
