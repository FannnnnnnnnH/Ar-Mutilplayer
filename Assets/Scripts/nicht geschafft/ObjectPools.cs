using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectPools : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> availableObjects = new List<GameObject>();
    private Dictionary<GameObject, PhotonView> instantiatedObjects = new Dictionary<GameObject, PhotonView>();

    private ObjectPools objectPools;
    private PhotonView photonView;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = InstantiateObject(Vector3.zero, Quaternion.identity);
                Debug.Log("11");
                ReturnToPool(obj);
                Debug.Log("22");
            }
        }
       
    }
    //当数量大于0的时候，
    public GameObject InstantiateObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        PhotonView view;

        if (availableObjects.Count > 0)
        {
            // 取出该列表中第一个游戏对象
            obj = availableObjects[0];
            // 删除列表的第一个游戏对象(因为被拿出去了)
            availableObjects.RemoveAt(0);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            view = obj.GetComponent<PhotonView>();
        }
        // 如果没有游戏对象
        else
        {
            // 那么实例化生成一个游戏对象
            obj = PhotonNetwork.Instantiate(prefab.name, position, rotation);
            view = obj.GetComponent<PhotonView>();
            // 放到对象池中
            instantiatedObjects.Add(obj, view);
        }
        // 将其激活
        obj.SetActive(true);
        // 返回结果
        return obj;
    }
    // 放东西
    public void ReturnToPool(GameObject obj)
    {
        // 将其隐藏
        obj.SetActive(false);
        // 将游戏对象添加到列表中
        availableObjects.Add(obj);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B)) 
        {InstantiateObject(transform.position, transform.rotation); Debug.Log("33"); }
        if (Input.GetKey(KeyCode.Q)) { ReturnToPool(gameObject); Debug.Log("44"); }
    }

}
