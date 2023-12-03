using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    GameObject playerOb;
    List<GameObject> mplayers = new List<GameObject>();
    Player[] allplayers;
    // Start is called before the first frame update

  
    /// <summary>
    /// 其他玩家连接房间时的回调（不是自己）
    ///OnPlayerEnteredRoom用于远程玩家而不是本地玩家
    ///</summary>
    public override void OnPlayerEnteredRoom(Player other)
    {
        allplayers = PhotonNetwork.PlayerList;
       // allplayers = PhotonNetwork.PlayerList;
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        print(other.NickName + " 加入房间");
        if (PhotonNetwork.IsMasterClient)
         {
            Debug.Log("OnPlayerEnteredRoom IsMasterClient {0}"+ PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

             LoadArena();
         }
    }
    
        #region Private Methods
        void LoadArena()
    {
        //Photon分为主客户端、其他客户端。只有主客户端才可进行加载房间。其他玩家加入主客户端的房间即可。
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.Log("PhotonNetwork : Loading Level : {0}"+ PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion

    void Start()
    {
       
        this.GetComponent<CubeMoveTest>().enabled = true;
        allplayers = PhotonNetwork.PlayerList;
        playerOb = PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        playerOb.GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    #region Photon Callbacks
    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// OnLeftRoom是做用于本地玩家
    /// </summary>
    public override void OnLeftRoom()
    {
        
        //base.OnLeftRoom();
  
        
        PhotonNetwork.LoadLevel(0);
    }
    #endregion


    #region Public Methods
    public void LeaveRoom()
    {
        Destroy(playerOb);
        PhotonNetwork.LeaveRoom();
    }
    #endregion
    /// <summary>
    /// 是做用于其他玩家离开房间
    /// </summary>
    /// <param name="other"></param>
    public override void OnPlayerLeftRoom(Player other)
    {
        if (PhotonNetwork.PlayerList != null)
        {
            foreach (var playersName in PhotonNetwork.PlayerList)
            {
                //tell use each player who is in the room

                if (PhotonNetwork.IsMasterClient)
                {
                    foreach (var p in allplayers)
                    {
                        if (p != PhotonNetwork.LocalPlayer)
                        {
                            PhotonNetwork.CurrentRoom.SetMasterClient(p);
                            print(allplayers);
                            break;
                        }
                    }
                    break;
                }

            }
        }
    }
     
    
}
