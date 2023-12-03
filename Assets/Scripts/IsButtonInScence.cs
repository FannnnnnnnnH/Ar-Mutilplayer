using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// in Arraylist
/// ��trigger��ť
/// �򿪾���ƹ�spotlicht
/// </summary>
public class IsButtonInScence : MonoBehaviour
{

    //�ҵ����
    GameObject canvasMenu;
    //�ҵ��������İ�ť
    GameObject stopButton;
    //�����Ǿ�����
    public GameObject rotLicht;

    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        canvasMenu = GameObject.Find("Canvas");
        stopButton = canvasMenu.transform.Find("EventTrigge").gameObject;
        photonView = PhotonView.Get(this);
        rotLicht.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�����Ƿ����
        if (MasterclientDestory.isTrigger) 
        {
            StartCoroutine(LichtAnderung());
           
           // Debug.Log(this.name);
           // Debug.Log("Bitte");
        }
        //�����Ƿ񱻷���
        if (EnemyDetection.wirdEmeyGefunden)
        {
            if (!stopButton.activeInHierarchy) 
            {
               
                Debug.Log("stopButton.activeInHierarchy");
                photonView.RPC("Test", RpcTarget.All); 
            }
           
        }
    }

    IEnumerator LichtAnderung() 
    {
        
        yield return new WaitForSeconds(0);
        rotLicht.SetActive(true);
        yield return new WaitForSeconds(1);
        rotLicht.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        rotLicht.SetActive(true);
        yield return new WaitForSeconds(1);
        rotLicht.SetActive(false);
    }

    [PunRPC]
    public void Test()
    {
        stopButton.SetActive(true);
        EnemyDetection.wirdEmeyGefunden = false;
        Debug.Log("Endlich");
    }
}
