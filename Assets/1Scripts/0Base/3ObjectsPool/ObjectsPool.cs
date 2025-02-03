using System.Collections.Generic;
using UnityEngine;

public static class ObjectsPool
{
    private static Dictionary<string, Queue<GameObject>> _spawnDict;
    private static Transform _spawnContainer;


    private static void Init()
    {
        if (_spawnContainer) return;
        _spawnDict = new Dictionary<string, Queue<GameObject>>();
        _spawnContainer = new GameObject("SpawnContainer").transform;
    }

    public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Object
    {
        Init();
        if (!_spawnDict.ContainsKey(prefab.name))
        {
            _spawnDict.Add(prefab.name, new Queue<GameObject>());
        }

        if (_spawnDict[prefab.name].TryDequeue(out var currentItem))
        {
            currentItem.transform.SetParent(null);
            currentItem.transform.SetPositionAndRotation(position, rotation);
            currentItem.SetActive(true);
        }
        else
        {
            if (prefab is GameObject prefabGameObject)
            {
                currentItem = Object.Instantiate(prefabGameObject, position, rotation);
            }
            else
            {
                currentItem = Object.Instantiate((prefab as MonoBehaviour).gameObject, position, rotation);
            }
        }

        if (prefab is GameObject)
        {
            return (T)(Object)currentItem;
        }

        return currentItem.GetComponent<T>();
    }

    public static void Despawn(GameObject go)
    {
        Init();
        go.SetActive(false);
        go.transform.SetParent(_spawnContainer);
        var goName = go.name.Replace("(Clone)", string.Empty);
        if (!_spawnDict.ContainsKey(goName))
        {
            _spawnDict.Add(goName, new Queue<GameObject>());
        }

        _spawnDict[goName].Enqueue(go);
    }

    public static void Despawn(GameObject go, float delay)
    {
        var poolDespawner = go.GetComponent<PoolDespawner>();
        if (!poolDespawner)
        {
            poolDespawner = go.AddComponent<PoolDespawner>();
        }

        poolDespawner.Delay = delay;
    }
}