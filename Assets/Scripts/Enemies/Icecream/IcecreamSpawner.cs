using System.Collections;
using Pooling;
using UnityEngine;


namespace Enemies.Icecream
{
    public class IcecreamSpawner : MonoBehaviour
    {
        [SerializeField] private Icecream icecreamPrefab;
        [SerializeField] private float spawnRate;

        private PoolMemory<Icecream> _icecreamPoolMemory;

        private void Start()
        {
            _icecreamPoolMemory = PoolsManager.Instance.GetPoolMemory<Icecream>();
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {

            Icecream icecream = _icecreamPoolMemory.GetPoolObject(icecreamPrefab);
            icecream.transform.position = transform.position;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}