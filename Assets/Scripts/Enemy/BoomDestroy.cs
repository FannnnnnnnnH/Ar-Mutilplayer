using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BoomDestroy : MonoBehaviour
{
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GameObject>();
        Invoke("TEST", 2);
    }
    private void TEST() 
    {
        GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);
        PhotonNetwork.Destroy(this.gameObject); 
        

    }
  
}
