using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks  //行为Pun回调
{

    [Header("Login UI")]
    public InputField playerNameInputField;
    public GameObject uiLoginGameobject;

    [Header("Lobby UI")]
    public GameObject uiLobbyGameobject;
    public GameObject ui3DGameobject;

    [Header("Connection Status UI")]
    public GameObject uiConnectionStatusGameobject;
    public Text connectStatusText;
    public bool showConnectionStatus = false;

    /* //用于大厅操作，例如连接到服务器和加载播放器选项
       //For lobby operations, such as connecting to the server and loading player options
    */
    #region unity Methods
    // Start is called before the first frame update
    void Start()
    {
        uiLobbyGameobject.SetActive(false);
        ui3DGameobject.SetActive(false);
        uiConnectionStatusGameobject.SetActive(false);
        uiLoginGameobject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (showConnectionStatus) { connectStatusText.text = "Connection Status" + PhotonNetwork.NetworkClientState; }
       
    }
    #endregion

    #region UI Callback Methods 回调方法OnEnterGameButtonCliked
    public void OnEnterGameButtonCliked()
    {
        string playerName = playerNameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            showConnectionStatus = true;

            uiLobbyGameobject.SetActive(false);
            ui3DGameobject.SetActive(false);
            uiConnectionStatusGameobject.SetActive(true);
            uiLoginGameobject.SetActive(false);

            if (!PhotonNetwork.IsConnected)
            {
                //用户名
                PhotonNetwork.LocalPlayer.NickName = playerName;
               //这里链接Photon网络
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        else
        {
            Debug.Log("Player name is invalid or empty");
        }
    }

    public void OnQuickMatchButtonClicked()
    {

    }

    #endregion

    #region Photon Callback Method 编写Photon回调方法调用Photon，点击Enter Game Button时候调用

    public override void OnConnected()//
    {
        Debug.Log(" We connected to Internet");
    }

    public override void OnConnectedToMaster()//用户调用这个时候已经成功连接服务器
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName +"is conneted to Photon Server");
        uiLobbyGameobject.SetActive(true);
        ui3DGameobject.SetActive(true);
        uiConnectionStatusGameobject.SetActive(false);
        uiLoginGameobject.SetActive(false);
    }

    #endregion
}
