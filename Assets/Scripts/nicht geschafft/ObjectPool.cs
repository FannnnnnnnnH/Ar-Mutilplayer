using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// blog.csdn.net/Brave_boy666/article/details/118700069
/// </summary>
public class ObjectPool : MonoBehaviour,IPunPrefabPool
{
    public List<GameObject> shellOjbect = new List<GameObject>();
    //游戏预制体
    public GameObject GoPrefab;
    //对象池中的最大个数
    public int MaxCount = 10;

    //将物体回收到池子里面
    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        Debug.LogWarning("Instantiate Prefab: " + prefabId);

        GameObject go = shellOjbect[0];
        go.transform.position = position;
        go.transform.rotation = rotation;

        return go;
    }

    public void Destroy(GameObject gameObject)
    {
        //SmartPool.Despawn(gameObject);

    }

    public void Push(GameObject go)
    {
        //如果池子还能装，就往里面加，满了就不往里面加
        if (shellOjbect.Count <= MaxCount)
        {
            shellOjbect.Add(go);
            Debug.Log("我们这边把烟花加进去了" + go.name);
            // go.SetActive(false);
        }
        else 
        { 
            PhotonNetwork.Destroy(go);
            Debug.Log("我们这边把烟花删了" + go.name);
        }
    }

    //从对象池中取出对象(返回值就要标记为GameObject)
    public GameObject Get()
    {
        GameObject go;
        if (shellOjbect.Count > 0)//判断列表不为空
        {
            go = shellOjbect[0];//如果有的话，就把第一项取出来
            //go.SetActive(true);
            shellOjbect.RemoveAt(0);//在list中把第一项删除
            Debug.Log("列表的长度"+shellOjbect.Count);

        }
        else 
        { 
            //子弹
            //go = PhotonNetwork.InstantiateRoomObject("shell", new Vector3(0, 0, 0), Quaternion.identity);
            //烟花
            go = PhotonNetwork.Instantiate("Boom", new Vector3(0, 0, 0), Quaternion.identity);
            Debug.Log("我们这边生成了盐花" + go.name);
        }
        //后期产生的子弹位置还要改。
        return go;//没有的话就直接创建一个新的预制体
    }

   

}
