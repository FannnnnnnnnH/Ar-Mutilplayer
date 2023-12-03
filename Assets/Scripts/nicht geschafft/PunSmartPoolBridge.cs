using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

//https://forum.photonengine.com/discussion/8214/ipunprefabpool-working-sample-with-smartpool-free
//http://docs.poolmanager.path-o-logical.com/home
//https://gist.github.com/su10/19fc8b1b27ebbed7ef460953a289f232
//https://qiita.com/su10/items/4bae56bc814c4e6a2bc0
public class PunSmartPoolBridge : MonoBehaviour, IPunPrefabPool
{
    // Start is called before the first frame update
    private readonly List<ObjectPool> _objectPoolList = new List<ObjectPool>();

    public void AddObjectPool(ObjectPool _objectPool)
    {
        if (_objectPoolList.Contains(_objectPool) == false)
        {
            _objectPoolList.Add(_objectPool);
        }
    }

    public void RemoveObjectPool(ObjectPool _objectPool)
    {
        _objectPoolList.Remove(_objectPool);
    }

    GameObject IPunPrefabPool.Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        var objectPool = _objectPoolList.First(pool => pool.HasPrefab(prefabId));
        var go = objectPool.Rent().gameObject;
        go.transform.SetPositionAndRotation(position, rotation);
        return go;
    }

    void IPunPrefabPool.Destroy(GameObject gameObject)
    {
        foreach (var objectPool in _objectPoolList)
        {
            if (objectPool.Return(gameObject))
            {
                return;
            }
        }
    }

    public abstract class ObjectPool
    {
        public abstract bool HasPrefab(string prefabId);
        public abstract Component Rent();
        public abstract bool Return(GameObject gameObject);
    }
}

