using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

/// <summary>
/// 这里是生成AR锚点
/// 生成游戏场景，游戏场景放置
/// 开始AR游戏的功能
/// 已经放在ARPlaneEinAndOff里面了
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]

public class ClickandPutObject : MonoBehaviourPunCallbacks
{
    [Header("游戏场景放置")]
    private GameObject GeneObject;
    private GameObject GeneObject_1;
    private GameObject GeneObject_2;
    private GameObject GeneObject_3;
    // private GameObject[] _geneObject = new GameObject[4];

    [Header("AR锚点")]
    public GameObject CursorObj;
    //判断锚点是否打开
    public bool useCursor = false;
    //是否点击开始游戏按钮
    private bool isBeginn = true;
    //锚点按钮
    private bool isArein;
    public GameObject readyButton;

    [Header("相机位置")]
    public Camera ArCamera;
    //private Vector2 screenPosition;


    [Header("AR开始游戏")]
    public GameObject gameBeginnButton;



    /// <summary>
    /// Ar射线碰撞管理器
    /// </summary>
    ARRaycastManager arRaycast;
    /// <summary>
    /// Ar射线碰到的物件和集合
    /// </summary>
    static private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    /// <summary>
    /// 点击坐标
    /// </summary>
   
    // Start is called before the first frame update
    private void Start()
    {
        
        arRaycast = GetComponent<ARRaycastManager>();
        //CursorObj = PhotonNetwork.Instantiate("Cube", new Vector3(0, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        //管理应用的生命周期

        /* Touch touch;
         if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
         {
             return;
         }*/
        if (useCursor&& isBeginn)
        {
            CursorClickGene();
        }
    }

    // Update is called once per frame
    /// <summary>
    /// 点击生成AR锚点
    /// </summary>
    public void Click()
    {
        readyButton.SetActive(false);     
        useCursor = true;
        //GeneObject = true;
       // Debug.Log("sssss");
        CursorObj = PhotonNetwork.Instantiate("ww", new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("kkkkk");
        gameBeginnButton.SetActive(true);
        isBeginn = true;
        isArein= this.gameObject.GetComponent<ARPlaneEinAndOff>()._isArEin;
    }
    /// <summary>
    /// 点击游戏场景生成
    /// </summary>
    public void GameBeginn()
    {
        GeneObject = PhotonNetwork.Instantiate("Cube", new Vector3(CursorObj.transform.position.x, CursorObj.transform.position.y-1, CursorObj.transform.position.z), Quaternion.identity);
        GeneObject_1 = PhotonNetwork.Instantiate("t", new Vector3(CursorObj.transform.position.x+2, CursorObj.transform.position.y+2, CursorObj.transform.position.z), Quaternion.identity);
        GeneObject_2 = PhotonNetwork.Instantiate("t1", new Vector3(CursorObj.transform.position.x - 2, CursorObj.transform.position.y + 1, CursorObj.transform.position.z), Quaternion.identity);
        GeneObject_3 = PhotonNetwork.Instantiate("t2", new Vector3(CursorObj.transform.position.x + 1, CursorObj.transform.position.y + 3, CursorObj.transform.position.z), Quaternion.identity);
        gameBeginnButton.SetActive(false);
        PhotonNetwork.Destroy(CursorObj);
        isBeginn = false;
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
           
            hits.Sort((x, y) => x.distance.CompareTo(y.distance));
             CursorObj.transform.position = hits[0].pose.position;
             CursorObj.transform.rotation = hits[0].pose.rotation;
            Debug.Log("sssss");
        }

    }

   /* private void TapOject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //储存点击坐标
            mausePos = Input.mousePosition;

            if (arRaycast.Raycast(mausePos, hits, TrackableType.PlaneWithinPolygon)) 
            {
                Pose pose = hits[0].pose;
                GameObject temp= Instantiate(GeneObject, pose.position, pose.rotation);

                Vector3 look = transform.position;
                look.y = temp.transform.position.y;
                temp.transform.LookAt(look);

            }
        }
    }*/
}
