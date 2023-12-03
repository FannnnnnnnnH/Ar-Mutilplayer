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
        //在创建房间时将RoomOptions.CleanupCacheOnLeave 设置为 false将会禁用（当客户端离开房间时，其余玩家将销毁离开玩家创建的游戏对象）。
        RoomOptions options = new RoomOptions { MaxPlayers = 4, CleanupCacheOnLeave = false };
        PhotonNetwork.JoinOrCreateRoom(roomNameInputField.text,options,default);
    }

    // 加入房间成功回调
    //主客户端加载场景即可。其他客户端进入房间后，会自动加载主客户端的场景
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        { PhotonNetwork.LoadLevel(1); }
    }
}
