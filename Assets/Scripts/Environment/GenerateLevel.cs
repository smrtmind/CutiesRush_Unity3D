﻿using System.Collections;
using UnityEngine;

namespace Scripts.Environment
{
    public class GenerateLevel : MonoBehaviour
    {
        [SerializeField] private GameObject[] _sections;

        [Space]
        [SerializeField] private int _spawnSectionsOnStart;
        [SerializeField] private float _spawnSectionsDelay = 2f;
        [SerializeField] private float _sectionWidth;

        private float _zPosition;
        private bool _readyToSpawn;

        private void Awake()
        {
            for (int i = 0; i < _spawnSectionsOnStart - 1; i++)
            {
                SpawnSection();
                _readyToSpawn = true;
            }
        }

        private void Update()
        {
            if (_readyToSpawn)
            {
                _readyToSpawn = false;
                StartCoroutine(GenerateSection());
            }
        }

        private IEnumerator GenerateSection()
        {
            SpawnSection();

            yield return new WaitForSeconds(_spawnSectionsDelay);

            _readyToSpawn = true;
        }

        private void SpawnSection()
        {
            var randomIndex = Random.Range(0, _sections.Length);

            Instantiate(_sections[randomIndex], new Vector3(0, 0, _zPosition), Quaternion.identity);
            _zPosition += _sectionWidth;
        }
    }
}