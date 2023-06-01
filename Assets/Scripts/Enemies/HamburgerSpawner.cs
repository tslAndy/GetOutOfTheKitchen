using System.Collections;
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
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < 4; i++)
            {
                Hamburger hamburger = PoolsManager.Instance.HamburgerPool.GetPoolObject(hamburgerPrefab);
                hamburger.transform.position = transform.position;
                yield return new WaitForSeconds(spawnRate);
            }
            yield return new WaitForSeconds(5f);
            PoolsManager.Instance.HamburgerPool.ClearObjects(hamburgerPrefab);
        }
    }
}