using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// 实现了在 Photon 网络上生成、销毁游戏对象以及修改游戏对象的所有权
/// Implementierung der Erzeugung und Zerstoerung von Spielobjekten und AEnderung des Eigentums an Spielobjekten im Photon-Netzwerk
/// 挂在player上面
/// </summary>
public class MasterclientDestory : MonoBehaviour
{
    //场景中所有的敌人
    GameObject[] birdObj;
    //自身的ID
    PhotonView photonView;
    private int zahl;
    public GameObject arrayListPos;
    private GameObject emeyObject;
    int[] count = new int[] { -5, -3, 3, 5, -4, 4};
    private string[] cubeNameInResources = new string[9] { "001", "002", "003", "004", "005", "006", "007", "008", "009" };

    //如果生成敌人就将数据传到ISButtonInScence中去
    //然后就可以打开警告红光
    public static bool isTrigger = false;
    //TriggerButton中使用
    public static bool isTankda = false;

    private void Start()
    {
        isTrigger = false;
        isTankda = false;
        photonView = PhotonView.Get(this);
       
        //birdObj = GameObject.FindGameObjectsWithTag("Bird");

    }

    //检查当前玩家是否为 MasterClient，如果是则监听 TriggerButton 按钮的点击事件和 BoxColliderTest 的敌人生成事件。
    private void Update()
    {
        if ( PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (TriggerButton.klickCountTwoTime && isTankda) { EmeyDestory(); }
            if (BoxColliderTest.isEmeyGenerate && !isTankda) { EmeyGeneration(); }
           
        }
        if (photonView.IsMine) { if (BoxColliderTest.isTriggeExit) CubeGene(); }
        
    }

    //通过 GameObject.FindGameObjectsWithTag() 方法查找所有标签为 "Bird" 的游戏对象，并逐一销毁这些对象。
    private void EmeyDestory() 
    {
        birdObj = GameObject.FindGameObjectsWithTag("Enemy");
        
        for (int i = 0; i < birdObj.Length; i++)
        {
            BoxColliderTest.isEmeyGenerate = false;
            TriggerButton.klickCountTwoTime = false;
            birdObj[i].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);
            PhotonNetwork.Destroy(birdObj[i]);
            isTankda = false;


        }

    }

    private Vector3 InCircle()
    {
        arrayListPos = GameObject.Find("Ziel");
        Vector3 p;
        int i = 0;
        do
        {
            i = Random.Range(-4, 4);
        } while (i == 0);

        int j = 0;
        do
        {
            j = Random.Range(-4, 4);
        } while (j == 0);
        p = new Vector3(arrayListPos.transform.position.x+i, arrayListPos.transform.position.y, arrayListPos.transform.position.z+j);
        Debug.Log("ceshiceshi"+p);
        return p;
    }

    private Vector3 InCircleUFO()
    {
        arrayListPos = GameObject.Find("Ziel");
        Vector3 p;
        int i = Random.Range(0, 5);
        int x = Random.Range(0, 5);
        p = new Vector3(arrayListPos.transform.position.x + count[i], arrayListPos.transform.position.y+4, arrayListPos.transform.position.z + count[x]);
        Debug.Log("ceshiceshi" + p);
        return p;
    }
    //随机生成的位置上生成敌人，并将其所有权传递给 MasterClient
    //Erzeugen Feinde an zufällig generierten Orten und Besitz an den MasterClient
    private void EmeyGeneration() 
    {
        zahl = Random.Range(0, 100);

        //isTankda = true;
        /* if (zahl <= 10 && isTankda)
         {
             Debug.Log("20 BirdE");
             emeyObject = PhotonNetwork.Instantiate("Tank 1", InCircle(), Quaternion.identity);
             //如果生成敌人就将数据传到ISButtonInScence中去
             isTrigger = true;
             isTankda = false;
             //birdGener = PunSmartPoolBridge.
         }
         else if (isTankda) */
        //{
        BoxColliderTest.isEmeyGenerate = false;
        if (zahl<10)
        {
            Debug.Log("50 Tank");
            emeyObject = PhotonNetwork.Instantiate("Tank 1", InCircle(), Quaternion.identity);
            isTrigger = true;
            isTankda = true;
        }
        else if (zahl>90) 
        {
            Debug.Log("30 UFO");
            emeyObject = PhotonNetwork.Instantiate("UFO", InCircleUFO(), Quaternion.identity); 
            isTrigger = true; 
            isTankda = true;
        }
           

        }

    /// <summary>
    /// 随机生成方块
    /// </summary>
    private void CubeGene()
    {
        BoxColliderTest.isTriggeExit = false;
        int zahl = Random.Range(0, 100);
        int cubeID = Random.Range(0, 8);
        if (zahl <= 70)
        {
            PhotonNetwork.Instantiate(cubeNameInResources[cubeID], InCircle(), Quaternion.identity);
        }
        // birdGener = false;
        BoxColliderTest.isTriggeExit = false;
    }

}
