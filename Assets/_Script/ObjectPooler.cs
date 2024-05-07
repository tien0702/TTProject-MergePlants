using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTag
{
    public static readonly string ZOMBIE_TAG = "Zombie";
    public static readonly string FLOWER_TAG = "Flower";
}

[System.Serializable]
public class Pool
{
    [field: SerializeField] public string Tag { set; get; }
    [field: SerializeField] public int Size { set; get; }
    [field: SerializeField] public GameObject prefab { set; get; }
}
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { private set; get; }

    [SerializeField] public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        pools.ForEach(p => InitForPool(p));
    }

    public GameObject GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarningFormat("Tag {0} not exists!", tag);
            return null;
        }

        GameObject obj = null;
        if (poolDictionary[tag].Count == 0)
            obj = Instantiate(pools.Find(p => p.Tag == tag).prefab, transform);
        else
            obj = poolDictionary[tag].Dequeue();

        obj.transform.SetParent(null);
        return obj;
    }

    public bool PutInPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarningFormat("Tag {0} not exists!", tag);
            return false;
        }
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        poolDictionary[tag].Enqueue(obj);
        return true;
    }

    public bool InitForPool(Pool pool)
    {
        if (poolDictionary.ContainsKey(pool.Tag)) return false;
        pools.Add(pool);
        Queue<GameObject> objects = new Queue<GameObject>();
        for (int i = 0; i < pool.Size; i++)
        {
            var obj = Instantiate(pool.prefab, transform);
            obj.SetActive(false);
            objects.Enqueue(obj);
        }
        poolDictionary.Add(pool.Tag, objects);

        return true;
    }
    public bool ContainsPool(string tag) => poolDictionary.ContainsKey(tag);
}
