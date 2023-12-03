using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Playermanager : MonoBehaviour
{
    PhotonView PV;
    GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            controller = PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
