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
    /// ����������ӷ���ʱ�Ļص��������Լ���
    ///OnPlayerEnteredRoom����Զ����Ҷ����Ǳ������
    ///</summary>
    public override void OnPlayerEnteredRoom(Player other)
    {
        allplayers = PhotonNetwork.PlayerList;
       // allplayers = PhotonNetwork.PlayerList;
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        print(other.NickName + " ���뷿��");
        if (PhotonNetwork.IsMasterClient)
         {
            Debug.Log("OnPlayerEnteredRoom IsMasterClient {0}"+ PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

             LoadArena();
         }
    }
    
        #region Private Methods
        void LoadArena()
    {
        //Photon��Ϊ���ͻ��ˡ������ͻ��ˡ�ֻ�����ͻ��˲ſɽ��м��ط��䡣������Ҽ������ͻ��˵ķ��伴�ɡ�
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
    /// OnLeftRoom�������ڱ������
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
    /// ����������������뿪����
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
