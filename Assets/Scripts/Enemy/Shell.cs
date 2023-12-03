using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
/// <summary>
/// https://blog.csdn.net/pengdongwei/article/details/50373041
/// </summary>
public class Shell : MonoBehaviour
{
    public GameObject target;
    public float speed = 10;
    private float distanceToTarget;
    private bool move = true;

    PhotonView view;
    //
    //private GameObject getBoom;

    private void Start()
    {
        target = GameObject.Find("Ziel");
        this.GetComponent<GameObject>();
        StartCoroutine(Shoot());
        //me = GameObject.Find("ArrayList");
        //GameObject.FindGameObjectWithTag("ArrayList");
    }

    IEnumerator Shoot()
    {

        while (move)
        {
            Vector3 targetPos = target.transform.position;
            //����Ŀ��  (Z�ᳯ��Ŀ��)
            this.transform.LookAt(targetPos);
            //���ݾ���˥�� �Ƕ�
            float angle = Mathf.Min(1, Vector3.Distance(this.transform.position, targetPos) / distanceToTarget) * 45;
            //��ת��Ӧ�ĽǶȣ����Բ�ֵһ���Ƕȣ�Ȼ��ÿ֡��X����ת��
            this.transform.rotation = this.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
            //��ǰ����Ŀ���
            float currentDist = Vector3.Distance(this.transform.position, target.transform.position);
            if (currentDist < 0.1f)
            {
                move = false;
            }
            //ƽ�� ������Z���ƶ���
            this.transform.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime*0.1f, currentDist));
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Move")//||other.tag=="MoveCube"|| other.tag == "Boden"
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                PhotonNetwork.Instantiate("Boom", transform.position, Quaternion.identity);
                Invoke("Test", 1);
                Debug.Log("Invoke(Test1)");
            }
            //getBoom = GetComponent<ObjectPool>().Get();
            //getBoom.SetActive(true);
            //GetComponent<ObjectPool>().Push(this.gameObject);
            
          
        }
    }

    void Test() 
    {
        GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);
        PhotonNetwork.Destroy(this.gameObject);
        /* if (view.IsMine)
         {
             //PhotonNetwork.Destroy(this.gameObject);
             PhotonNetwork.Destroy(view);
             Debug.Log("view");

         }*/
        //view.RPC("DestroySelf", RpcTarget.All);
        //PhotonNetwork.Destroy(this.gameObject);
        // GetComponent<ObjectPool>().Push(getBoom);
        //getBoom.SetActive(false);
    }

    /*[PunRPC]
     void DestroySelf()
    {
        Destroy(this.gameObject);
        Debug.Log("this.gameObject");
    }*/
    // Update is called once per frame
}
