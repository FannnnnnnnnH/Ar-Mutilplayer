using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    private ObjectPools objectPools;
    private PhotonView photonView;

    private void Start()
    {
        objectPools = FindObjectOfType<ObjectPools>();
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            // 在本地客户端上，使用对象池来生成网络玩家子弹
            GameObject obj = objectPools.InstantiateObject(transform.position, transform.rotation);
            PhotonView view = obj.GetComponent<PhotonView>();
        }
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            // 在本地客户端上，使用 PhotonNetwork.Destroy() 方法来销毁网络子弹
            PhotonNetwork.Destroy(photonView);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
        {
            // 在子弹与其他对象发生碰撞时，销毁子弹
            PhotonNetwork.Destroy(photonView);
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (photonView.IsMine)
        {
            // 如果是本地客户端上的子弹，则将其位置和旋转同步到其他客户端上
            objectPools.ReturnToPool(gameObject);
        }
    }
}
