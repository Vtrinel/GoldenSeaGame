using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SpawnObjectPooler : MonoBehaviour
{
    public static SpawnObjectPooler SpawnPoolerInstance;
    public List<ObjectPoolItem> itemsToPool;


    public List<List<SpawnableObject>> pooledObjectsList;
    public List<SpawnableObject> pooledObjects;
    private List<int> positions;
    

    void Awake()
    {

        SpawnPoolerInstance = this;

        pooledObjectsList = new List<List<SpawnableObject>>();
        pooledObjects = new List<SpawnableObject>();
        positions = new List<int>();


        for (int i = 0; i < itemsToPool.Count; i++)
        {
            ObjectPoolItemToPooledObject(i);
        }

    }


    public SpawnableObject GetPooledObject(int index)
    {

        int curSize = pooledObjectsList[index].Count;
        for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
        {

            if (!pooledObjectsList[index][i % curSize].gameObject.activeInHierarchy)
            {
                positions[index] = i % curSize;
                return pooledObjectsList[index][i % curSize];
            }
        }

        if (itemsToPool[index].shouldExpand)
        {

            SpawnableObject spawnObj = Instantiate(itemsToPool[index].objectToPool).GetComponent<SpawnableObject>();
            spawnObj.gameObject.SetActive(false);
            spawnObj.transform.parent = this.transform;
            pooledObjectsList[index].Add(spawnObj);
            return spawnObj;

        }
        return null;
    }

    public List<SpawnableObject> GetAllPooledObjects(int index)
    {
        return pooledObjectsList[index];
    }


    public int AddObject(GameObject GO, int amt = 3, bool exp = true)
    {
        ObjectPoolItem item = new ObjectPoolItem(GO, amt, exp);
        int currLen = itemsToPool.Count;
        itemsToPool.Add(item);
        ObjectPoolItemToPooledObject(currLen);
        return currLen;
    }


    void ObjectPoolItemToPooledObject(int index)
    {
        ObjectPoolItem item = itemsToPool[index];

        pooledObjects = new List<SpawnableObject>();
        for (int i = 0; i < item.amountToPool; i++)
        {
            SpawnableObject spawnObj = Instantiate(item.objectToPool).GetComponent<SpawnableObject>();
            spawnObj.gameObject.SetActive(false);
            spawnObj.transform.parent = this.transform;
            pooledObjects.Add(spawnObj);
        }
        pooledObjectsList.Add(pooledObjects);
        positions.Add(0);

    }
}
