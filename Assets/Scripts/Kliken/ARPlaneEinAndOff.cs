using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
/// <summary>
/// 控制AR地面识别功能的开关
/// 功能被挂在按钮上面
/// 生成AR锚点
/// 生成游戏场景，游戏场景放置
/// 开始AR游戏的功能
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class ARPlaneEinAndOff : MonoBehaviourPunCallbacks
{
    ARPlaneManager m_ARPlaneManger;

    [Header("AR地面打开和关闭")]
    public GameObject enableArButton;
    public GameObject disableArBUtton;

    public bool _isArEin = false;

    //游戏场景物体放置
    private GameObject GeneObject;
    private GameObject GeneObject_1;
    private GameObject GeneObject_2;
    private GameObject GeneObject_3;
    private GameObject GeneObject_4;
    private GameObject GeneObject_5;
    private GameObject GeneObject_6;
    private GameObject GeneObject_7;

    [Header("AR锚点")]
    private GameObject CursorObj;

    //判断锚点是否打开
    public bool useCursor = false;
    //是否已经生成锚点
    private bool isBonCursor = false;
    //是否点击开始游戏按钮
    private bool isBeginn = true;

    //开始游戏按钮
    [Header("锚点放置按钮")]
    public GameObject readyAnchorButton;
    [Header("AR开始游戏")]
    public GameObject gameBeginnButton;

    [Header("相机位置")]
    public Camera ArCamera;

    // Ar射线碰撞管理器
    ARRaycastManager arRaycast;

    // Ar射线碰到的物件和集合
    static private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    bool trackableRay=true;

    private void Awake()
    {
        arRaycast = GetComponent<ARRaycastManager>();
        m_ARPlaneManger = GetComponent<ARPlaneManager>();
        //最开始的AR地面检测是关着的
        m_ARPlaneManger.enabled = false;
        // m_ARClickandPutObject.enabled = true;
        trackableRay = false;
        SetAllPlanesActiveOrDeactive(false);
    }

    void Start()
    {
        //readyButton.SetActive(false);
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            enableArButton.SetActive(true);
            disableArBUtton.SetActive(false);
            readyAnchorButton.SetActive(false);
            gameBeginnButton.SetActive(false);

            // LoadArena();
        }

    }
    
    void Update()
    {
        //if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        if (useCursor && isBeginn)
        {
            CursorClickGene();
        }
        if (trackableRay) 
        { 
            gameBeginnButton.SetActive(true);
            trackableRay = false;
        }
    }
    /// <summary>
    /// 测试
    /// </summary>
    public void TestTest() 
    {
        Debug.Log("woshizhuren");
        m_ARPlaneManger.enabled = true;
        // m_ARClickandPutObject.enabled = true;

        SetAllPlanesActiveOrDeactive(true);
    }

    /// <summary>
    /// 关闭Ar地面识别
    /// </summary>
    public void DisableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManger.enabled = false;
        SetAllPlanesActiveOrDeactive(false);

        enableArButton.SetActive(true);
        disableArBUtton.SetActive(false);
        readyAnchorButton.SetActive(false);
        gameBeginnButton.SetActive(false);
        _isArEin = false;
        PhotonNetwork.Destroy(CursorObj);
        //searchForGameButton.SetActive(false);

        trackableRay = false;
    }
    /// <summary>
    /// 开启AR地面识别
    /// </summary>
    public void EnableARPlacementAndPlaneDetection()
    {
        //Debug.Log("woshizhuren");
        m_ARPlaneManger.enabled = true;
        // m_ARClickandPutObject.enabled = true;

        SetAllPlanesActiveOrDeactive(true);

        enableArButton.SetActive(false);
        disableArBUtton.SetActive(true);
        readyAnchorButton.SetActive(true);
        gameBeginnButton.SetActive(false);
        // searchForGameButton.SetActive(true);
        _isArEin = true;

    }

    private void SetAllPlanesActiveOrDeactive(bool value)
    {
        foreach (var plane in m_ARPlaneManger.trackables)
        {
            plane.gameObject.SetActive(value);

        }
    }

    /// <summary>
    /// 点击生成AR锚点
    /// </summary>
    public void Click()
    {
        readyAnchorButton.SetActive(false);
        useCursor = true;
        //GeneObject = true;
        // Debug.Log("sssss");
        CursorObj = PhotonNetwork.Instantiate("ww", new Vector3(0, 0, 0), Quaternion.identity);
        //Debug.Log("kkkkk");
        //gameBeginnButton.SetActive(true);//记得删除
        isBeginn = true;

   
    }
    /// <summary>
    /// 点击游戏场景生成
    /// </summary>
    public void GameBeginn()
    {
       // Debug.Log("HIHIIHIHIHIHI");
        GeneObject = PhotonNetwork.Instantiate("Cube", new Vector3(CursorObj.transform.position.x, CursorObj.transform.position.y - 1, CursorObj.transform.position.z), Quaternion.identity);
        GeneObject_1 = PhotonNetwork.Instantiate("010", new Vector3(CursorObj.transform.position.x + 2, CursorObj.transform.position.y + 2, CursorObj.transform.position.z), Quaternion.identity);
        GeneObject_2 = PhotonNetwork.Instantiate("011", new Vector3(CursorObj.transform.position.x - 2, CursorObj.transform.position.y + 1, CursorObj.transform.position.z), Quaternion.identity);
        GeneObject_3 = PhotonNetwork.Instantiate("013", new Vector3(CursorObj.transform.position.x + 1, CursorObj.transform.position.y + 3, CursorObj.transform.position.z), Quaternion.identity);
        GeneObject_4 = PhotonNetwork.Instantiate("014", new Vector3(CursorObj.transform.position.x + 1, CursorObj.transform.position.y + 2, CursorObj.transform.position.z+1), Quaternion.identity);
        GeneObject_5 = PhotonNetwork.Instantiate("015", new Vector3(CursorObj.transform.position.x + 2, CursorObj.transform.position.y + 2, CursorObj.transform.position.z+2), Quaternion.identity);
        GeneObject_6 = PhotonNetwork.Instantiate("016", new Vector3(CursorObj.transform.position.x + 1, CursorObj.transform.position.y + 2, CursorObj.transform.position.z-1), Quaternion.identity);
       // GeneObject_7 = PhotonNetwork.Instantiate("017", new Vector3(CursorObj.transform.position.x + 2, CursorObj.transform.position.y + 2, CursorObj.transform.position.z-1), Quaternion.identity);
        //GeneObject_5 = PhotonNetwork.Instantiate("GameObject", new Vector3(CursorObj.transform.position.x + 2, CursorObj.transform.position.y + 2, CursorObj.transform.position.z), Quaternion.identity);
        gameBeginnButton.SetActive(false);
        PhotonNetwork.Destroy(CursorObj);
        isBeginn = false;
        enableArButton.SetActive(false);
        disableArBUtton.SetActive(false);
        m_ARPlaneManger.enabled = false;
        SetAllPlanesActiveOrDeactive(false);
       
    }

    /// <summary>
    /// 锚点随着射线移动
    /// </summary>
    private void CursorClickGene()
    {

        Vector3 centerOFScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = ArCamera.ScreenPointToRay(centerOFScreen);
        ///
        // Ray ray = FirstPersonCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //arRaycast.Raycast(ray, hits, TrackableType.PlaneWithinPolygon);
        if (arRaycast.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
        {
            trackableRay = true;
            hits.Sort((x, y) => x.distance.CompareTo(y.distance));
            CursorObj.transform.position = hits[0].pose.position;
            CursorObj.transform.eulerAngles = new Vector3(90, 0, 0);
            Debug.Log("sssss");
        }

    }
   
}
