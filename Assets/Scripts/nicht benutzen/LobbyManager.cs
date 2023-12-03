using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks  //��ΪPun�ص�
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

    /* //���ڴ����������������ӵ��������ͼ��ز�����ѡ��
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

    #region UI Callback Methods �ص�����OnEnterGameButtonCliked
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
                //�û���
                PhotonNetwork.LocalPlayer.NickName = playerName;
               //��������Photon����
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

    #region Photon Callback Method ��дPhoton�ص���������Photon�����Enter Game Buttonʱ�����

    public override void OnConnected()//
    {
        Debug.Log(" We connected to Internet");
    }

    public override void OnConnectedToMaster()//�û��������ʱ���Ѿ��ɹ����ӷ�����
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName +"is conneted to Photon Server");
        uiLobbyGameobject.SetActive(true);
        ui3DGameobject.SetActive(true);
        uiConnectionStatusGameobject.SetActive(false);
        uiLoginGameobject.SetActive(false);
    }

    #endregion
}
