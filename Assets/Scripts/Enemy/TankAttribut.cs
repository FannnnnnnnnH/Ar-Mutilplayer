using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankAttribut : MonoBehaviour
{
    private float distanceToMe;           //智能体到目标的距离
    private GameObject zielPo;                //目标角色
   // private float isSeekDistance = 3.0f;  //可靠近范围
   // private int state;                     //智能体状态

    public GameObject firePoint;
    PhotonView view;
    //发射音效
    public AudioClip shotAudio;
    bool isTrue=true;

    // Start is called before the first frame update
    void Start()
    {
        view = PhotonView.Get(this);
        zielPo = GameObject.Find("Ziel");
        firePoint.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToMe = Vector3.Distance(zielPo.transform.position, this.transform.position);
       
       
         if (distanceToMe > 2f)
         {
            this.transform.LookAt(new Vector3(zielPo.transform.position.x, this.transform.position.y, zielPo.transform.position.z));               //该方法使智能体总是面对目标
            this.transform.Translate(Vector3.forward * 0.05f);
                //Debug.Log("DOMove");
         }
         if (distanceToMe <= 2f && isTrue)
        { 
             
                
            isTrue = false;
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
           {
                //Invoke("ShellPerSekunde", 1);
                // PhotonNetwork.Instantiate("shell", firePoint.transform.position, Quaternion.identity);
               Invoke("ShellPerSekunde", 1); 
            }
            
         }
    }

    public void ShellPerSekunde() 
    {
        for (int i = 0; i <= 4; i++)
            PhotonNetwork.Instantiate("shell", firePoint.transform.position, Quaternion.identity);
    }



}
