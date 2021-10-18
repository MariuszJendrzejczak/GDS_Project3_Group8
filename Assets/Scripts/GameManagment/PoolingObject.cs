using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    void Start()
    {
        pooledObjects = new List<GameObject>();
        AddObjectsToPool();
    }
    private void AddObjectsToPool()
    {
        GameObject temp;
        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(objectToPool);
            temp.transform.SetParent(transform);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count - 1; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                Debug.Log("pooled list: " + i);
                return pooledObjects[i];
            }
        }
        return null;
    }
}
