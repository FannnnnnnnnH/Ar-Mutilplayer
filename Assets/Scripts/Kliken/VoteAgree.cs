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
    /// ��������ť��ͬʱ���°���
    /// </summary>
    public void VoteMapButton() 
    {
        photonView.RPC("VoteRPC", RpcTarget.All);
    }
    /// <summary>
    /// ʹ��RPC����ͶƱ
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
