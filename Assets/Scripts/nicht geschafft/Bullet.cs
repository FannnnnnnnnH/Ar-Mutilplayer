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
            // �ڱ��ؿͻ����ϣ�ʹ�ö������������������ӵ�
            GameObject obj = objectPools.InstantiateObject(transform.position, transform.rotation);
            PhotonView view = obj.GetComponent<PhotonView>();
        }
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            // �ڱ��ؿͻ����ϣ�ʹ�� PhotonNetwork.Destroy() ���������������ӵ�
            PhotonNetwork.Destroy(photonView);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
        {
            // ���ӵ���������������ײʱ�������ӵ�
            PhotonNetwork.Destroy(photonView);
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (photonView.IsMine)
        {
            // ����Ǳ��ؿͻ����ϵ��ӵ�������λ�ú���תͬ���������ͻ�����
            objectPools.ReturnToPool(gameObject);
        }
    }
}
