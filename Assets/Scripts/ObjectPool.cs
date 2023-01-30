using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private class PrefabInfo
    {
        public PrefabInfo(GameObject prefab)
        {
            this.prefab = prefab;
        }
        public GameObject           prefab;
        public List<GameObject>     spawned     = new List<GameObject>(8);
        public Queue<GameObject>    inactive    = new Queue<GameObject>(8);

        public GameObject GetOrCreateToken()
        {
            if (inactive.Count == 0)
            {
                bool existState = prefab.activeSelf;
                prefab.SetActive(false);
                var token = GameObject.Instantiate(prefab);
                prefab.SetActive(existState);
                inactive.Enqueue(token);
            }
            return inactive.Dequeue();
        }

        public void Return(GameObject token)
        {
            if (token == null)
                throw new System.NullReferenceException();

            if (!spawned.Remove(token))
                throw new System.Exception($"{token} isn't spawned by this object pool.");
            if (token.activeSelf)
                token.SetActive(false);
            inactive.Enqueue(token);
        }

        public bool IsSpawned(GameObject token)
        {
            return spawned.Contains(token);
        }
    }

    Dictionary<GameObject /* prefab */, PrefabInfo> dict = new Dictionary<GameObject, PrefabInfo>(8);
    List<GameObject /* token */> activeTokens = new List<GameObject>(8);

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!dict.TryGetValue(prefab, out var info))
        {
            dict.Add(prefab, info = new PrefabInfo(prefab));
        }
        var token = info.GetOrCreateToken();
        token.transform.SetPositionAndRotation(position, rotation);
        if (!token.activeSelf)
            token.SetActive(true);
        return token;
    }

    public bool IsSpawned(GameObject token)
    {
        return activeTokens.Contains(token);
    }

    public void Despawn(GameObject token)
    {
        if (!activeTokens.Remove(token))
            Debug.LogError($"{token} isn't spawned by this object pool.", this);
        
        foreach (var d in dict.Values)
        {
            if (d.IsSpawned(token))
            {
                d.Return(token);
                return;
            }
        }
        throw new System.Exception("Fail to return token");
    }
}
