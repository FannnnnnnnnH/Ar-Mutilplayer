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
    //��ϷԤ����
    public GameObject GoPrefab;
    //������е�������
    public int MaxCount = 10;

    //��������յ���������
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
        //������ӻ���װ����������ӣ����˾Ͳ��������
        if (shellOjbect.Count <= MaxCount)
        {
            shellOjbect.Add(go);
            Debug.Log("������߰��̻��ӽ�ȥ��" + go.name);
            // go.SetActive(false);
        }
        else 
        { 
            PhotonNetwork.Destroy(go);
            Debug.Log("������߰��̻�ɾ��" + go.name);
        }
    }

    //�Ӷ������ȡ������(����ֵ��Ҫ���ΪGameObject)
    public GameObject Get()
    {
        GameObject go;
        if (shellOjbect.Count > 0)//�ж��б�Ϊ��
        {
            go = shellOjbect[0];//����еĻ����Ͱѵ�һ��ȡ����
            //go.SetActive(true);
            shellOjbect.RemoveAt(0);//��list�аѵ�һ��ɾ��
            Debug.Log("�б�ĳ���"+shellOjbect.Count);

        }
        else 
        { 
            //�ӵ�
            //go = PhotonNetwork.InstantiateRoomObject("shell", new Vector3(0, 0, 0), Quaternion.identity);
            //�̻�
            go = PhotonNetwork.Instantiate("Boom", new Vector3(0, 0, 0), Quaternion.identity);
            Debug.Log("��������������λ�" + go.name);
        }
        //���ڲ������ӵ�λ�û�Ҫ�ġ�
        return go;//û�еĻ���ֱ�Ӵ���һ���µ�Ԥ����
    }

   

}
