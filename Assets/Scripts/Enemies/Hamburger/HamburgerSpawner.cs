using System.Collections;
using Pooling;
using UnityEngine;

namespace Enemies.Hamburger
{
    public class HamburgerSpawner : MonoBehaviour
    {
        [SerializeField] private Hamburger hamburgerPrefab;
        [SerializeField] private float spawnRate;

        private PoolMemory<Hamburger> _hamburgerPoolMemory;

        private void Start()
        {
            _hamburgerPoolMemory = PoolsManager.Instance.GetPoolMemory<Hamburger>();
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < 4; i++)
            {
                Hamburger hamburger = _hamburgerPoolMemory.GetPoolObject(hamburgerPrefab);
                hamburger.transform.position = transform.position;
                yield return new WaitForSeconds(spawnRate);
            }
            yield return new WaitForSeconds(5f);
            _hamburgerPoolMemory.ClearObjects(hamburgerPrefab);
        }
    }
}