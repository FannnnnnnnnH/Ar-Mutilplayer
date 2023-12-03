using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Header("Login UI")]
    public GameObject CreatName;
    public InputField playerNameInputField;
    public GameObject uiLoginGameobject;
    
    [Header("Room UI")]
    public GameObject CreatRoom;
    public InputField roomNameInputField;
    public GameObject uiRoomGameobject;

   

    bool isConnecting;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //CreatName.SetActive(true);
    }
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        CreatName.SetActive(true);
    }
     public void PlayButton()
    {
       
        PhotonNetwork.NickName = playerNameInputField.text;
        if (playerNameInputField.text.Length < 1)
            return;
        CreatName.SetActive(false);
        CreatRoom.SetActive(true);
    }

    public void JoinOrCreateRoom()
    {
        if (roomNameInputField.text.Length < 1)
            return;
        CreatRoom.SetActive(false);
        //�ڴ�������ʱ��RoomOptions.CleanupCacheOnLeave ����Ϊ false������ã����ͻ����뿪����ʱ��������ҽ������뿪��Ҵ�������Ϸ���󣩡�
        RoomOptions options = new RoomOptions { MaxPlayers = 4, CleanupCacheOnLeave = false };
        PhotonNetwork.JoinOrCreateRoom(roomNameInputField.text,options,default);
    }

    // ���뷿��ɹ��ص�
    //���ͻ��˼��س������ɡ������ͻ��˽��뷿��󣬻��Զ��������ͻ��˵ĳ���
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        { PhotonNetwork.LoadLevel(1); }
    }
}
