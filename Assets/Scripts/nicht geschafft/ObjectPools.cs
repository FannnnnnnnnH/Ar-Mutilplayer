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
    //����������0��ʱ��
    public GameObject InstantiateObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        PhotonView view;

        if (availableObjects.Count > 0)
        {
            // ȡ�����б��е�һ����Ϸ����
            obj = availableObjects[0];
            // ɾ���б�ĵ�һ����Ϸ����(��Ϊ���ó�ȥ��)
            availableObjects.RemoveAt(0);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            view = obj.GetComponent<PhotonView>();
        }
        // ���û����Ϸ����
        else
        {
            // ��ôʵ��������һ����Ϸ����
            obj = PhotonNetwork.Instantiate(prefab.name, position, rotation);
            view = obj.GetComponent<PhotonView>();
            // �ŵ��������
            instantiatedObjects.Add(obj, view);
        }
        // ���伤��
        obj.SetActive(true);
        // ���ؽ��
        return obj;
    }
    // �Ŷ���
    public void ReturnToPool(GameObject obj)
    {
        // ��������
        obj.SetActive(false);
        // ����Ϸ������ӵ��б���
        availableObjects.Add(obj);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B)) 
        {InstantiateObject(transform.position, transform.rotation); Debug.Log("33"); }
        if (Input.GetKey(KeyCode.Q)) { ReturnToPool(gameObject); Debug.Log("44"); }
    }

}
