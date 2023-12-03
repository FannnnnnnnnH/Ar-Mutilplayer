using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

//����Eventtrigger����
public class TriggerButton : MonoBehaviour
{
    PhotonView photonView;
    
    //ͬʱ���µļ���
    public int klickCount = 0;
    //����ʱ��ʼ����
    private float timeLeft = 2f;

    //�ı���ʾ����ʱ
    public GameObject countDown;
    public Text countText;
    //��MasterclientDestory�ı���ʹ��
    public static bool klickCountTwoTime=false;

    private void Start()
    {

        photonView = PhotonView.Get(this);
        //birdObj = GameObject.FindGameObjectsWithTag("Bird");
        klickCountTwoTime = false;
        countDown.gameObject.SetActive(false);
        //countText.gameObject.SetActive(false); 
        
    }
    private void Update()
    {
        if (klickCount >=2) 
        {
            countDown.gameObject.SetActive(true);
           // countText.gameObject.SetActive(true);
            timeLeft -= Time.deltaTime;
            countText.text = timeLeft.ToString(format: "0");
            if (timeLeft <= 0)
            {
                klickCountTwoTime = true;
                //MasterclientDestory.isTankda = false;
                this.gameObject.SetActive(false);
                //print("������ʧ");
                timeLeft = 2f;
                klickCount = 0;
                countText.gameObject.SetActive(false);
            }
        }
        if (klickCount < 2) 
        { 
            timeLeft = 2f;
            countDown.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// ʹ��RPC����ͶƱ
    /// </summary>
    public void StopAttackDown()
    {
        
        photonView.RPC("Down", RpcTarget.All);
    }
    public void StopAttackUp()
    {
        photonView.RPC("Uppp", RpcTarget.All);
    }
    [PunRPC]
    private void Down()
    {
        klickCount++;
        if (klickCount >= 2)
        {
            //print("geschafft" + klickCount);

        }
        print("++" + klickCount);
        //do whatever has to happen here.
    }

    [PunRPC]
    private void Uppp()
    {
        klickCount--;
        if (klickCount < 0) { klickCount = 0; }
        print("--" + klickCount);
    }
 

}
