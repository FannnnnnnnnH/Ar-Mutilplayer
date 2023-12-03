using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PositionUpdate : MonoBehaviour
{
    public GameObject arrayListPos;
    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);
       
        photonView.RPC("PoUpdate", RpcTarget.All);

    }

    [PunRPC]
    public void PoUpdate() 
    {
         arrayListPos = GameObject.Find("Ziel");
        this.transform.position = arrayListPos.transform.position;//TransformPoint(arrayListPos.transform.localPosition);
        Debug.Log("局部坐标:"+arrayListPos.transform.position+"  世界坐标:"+this.transform.position);
    }
        // Update is called once per frame
     void Update()
    {
        
    }
}
