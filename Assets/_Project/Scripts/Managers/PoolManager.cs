using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        [HideInInspector]
        public List<GameObject> pool;
    }

    public static PoolManager Instance;
    public List<Pool> pools;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (Pool pool in pools)
        {
            pool.pool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                pool.pool.Add(obj);
            }
        }
    }

    private GameObject GetObject(string tag)
    {
        Pool pool = pools.Find(p => p.tag == tag);

        if (pool == null)
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = pool.pool.Find(obj => !obj.activeInHierarchy);

        if (objectToSpawn == null)
        {
            objectToSpawn = Instantiate(pool.prefab);
            objectToSpawn.SetActive(false);
            pool.pool.Add(objectToSpawn);
        }

        objectToSpawn.SetActive(true);
        return objectToSpawn;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = GetObject(tag);

        if (objectToSpawn != null)
        {
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
        }

        return objectToSpawn;
    }
}