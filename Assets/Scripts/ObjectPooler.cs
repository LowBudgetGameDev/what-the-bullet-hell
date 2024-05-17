using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public Transform objectToPool;
    public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [SerializeField] private List<ObjectPoolItem> itemsToPool;
    [SerializeField] private Dictionary<Transform, Queue<Transform>> poolDictionary;

    private Dictionary<Transform, List<Transform>> prefabIntsanceDictionary;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        prefabIntsanceDictionary = new Dictionary<Transform, List<Transform>>();

        poolDictionary = new Dictionary<Transform, Queue<Transform>>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            Queue<Transform> objectPool = new Queue<Transform>();
            List<Transform> instanceList = new List<Transform>();

            for (int i = 0; i < item.amountToPool; i++)
            {
                Transform obj = Instantiate(item.objectToPool);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);

                instanceList.Add(obj);
            }

            poolDictionary.Add(item.objectToPool, objectPool);
            prefabIntsanceDictionary.Add(item.objectToPool, instanceList);
        }
    }

    public Transform InstantiateWithPool(Transform pooledPrefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(pooledPrefab)) return null;

        for (int i = 0; i < poolDictionary.Count; i++)
        {
            if (poolDictionary[pooledPrefab].Count == 0) break;

            Transform objectToInstantiate = poolDictionary[pooledPrefab].Dequeue();

            if (!objectToInstantiate.gameObject.activeInHierarchy)
            {
                objectToInstantiate.gameObject.SetActive(true);
                objectToInstantiate.position = position;
                objectToInstantiate.rotation = rotation;

                return objectToInstantiate;
            }
        }

        ObjectPoolItem item = null;

        foreach (ObjectPoolItem poolItem in itemsToPool)
        {
            if (poolItem.objectToPool == pooledPrefab)
            {
                item = poolItem;
                break;
            }
        }

        if (item.shouldExpand)
        {
            Transform obj = Instantiate(item.objectToPool, position, rotation);
            prefabIntsanceDictionary[pooledPrefab].Add(obj);
            return obj;
        }

        return null;
    }

    public void DestoryWithPool(Transform objectToDestory)
    {
        objectToDestory.gameObject.SetActive(false);

        foreach (Transform prefab in prefabIntsanceDictionary.Keys)
        {
            if (prefabIntsanceDictionary[prefab].Contains(objectToDestory))
            {
                poolDictionary[prefab].Enqueue(objectToDestory);
            }
        }
    }
}
