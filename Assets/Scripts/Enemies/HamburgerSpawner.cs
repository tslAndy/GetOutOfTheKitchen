using System;
using System.Collections;
using System.Collections.Generic;
using Pooling;
using UnityEngine;

namespace Enemies
{
    public class HamburgerSpawner : MonoBehaviour
    {
        [SerializeField] private Hamburger hamburgerPrefab;
        [SerializeField] private float spawnRate;

        private void Start()
        {
            InvokeRepeating(nameof(Spawn), 0, spawnRate);
        }

        private void Spawn()
        {
            Hamburger hamburger = PoolsManager.Instance.HamburgerPool.GetPoolObject(hamburgerPrefab);
            hamburger.transform.position = transform.position;
        }
    }
}