using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VoteAgree : MonoBehaviour
{
    public GameObject testVoteText;

    PhotonView photonView;
    /// <summary>
    /// 点击这个按钮就同时按下按键
    /// </summary>
    public void VoteMapButton() 
    {
        photonView.RPC("VoteRPC", RpcTarget.All);
    }
    /// <summary>
    /// 使用RPC引用投票
    /// </summary>
    [PunRPC]
    public void VoteRPC()
    {
        testVoteText.SetActive(true);
       
    }
    private void Start()
    {
        testVoteText.SetActive(false);
        photonView = gameObject.GetComponent<PhotonView>();
    }
    // Start is called before the first frame update

}
