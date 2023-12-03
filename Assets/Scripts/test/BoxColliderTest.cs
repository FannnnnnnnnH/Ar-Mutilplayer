using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// 碰撞体检测积木是否在平台上，有的话就加入list
/// 控制场景中的突发事件
/// 1.平台积木的缩放，抖动，变形
/// 2.外部突发事件，生成敌人，生成地震
/// </summary>
public class BoxColliderTest : MonoBehaviour
{
   
    //随机数
    private int zahl;
    //进入碰撞检测器的积木的列表
    public List<GameObject> boxInColliderList = new List<GameObject>();

    GameObject[] CubeInSecneArray;

    int arrayListZahl;
    //生成的敌人
    GameObject birdObject;
    //敌人生成的位置
    //private Vector3 enemyTransf;
    //在masterclientDestory中使用
    public static bool isEmeyGenerate=false;
    public static bool isTriggeExit = false;

    bool einMal = true;

    //找到面板
   // GameObject canvasMenu;
    //找到面板下面的按钮
   // GameObject stopButton;
   
    PhotonView photonView;

    private string[] cubeNameInResources = new string[9] { "001", "002", "003", "004", "005", "006", "007", "008", "009" };

    int[] count = new int[] { -5, -3, 3, 5, -4, 4 };

    private int cubeID;

    //GameObject buttonTrigger;
    
    //圆心
    public GameObject circleCenter;

    Vector3 pos2;
    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);
        //canvasMenu = GameObject.Find("Canvas");
       // stopButton = canvasMenu.transform.Find("EventTrigge").gameObject;
        boxInColliderList.Clear();
        //Debug.Log("刚开始数目是"+boxInColliderList.Count);
        // enemyTransfCount = 1;
        isEmeyGenerate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { einMal = true;Debug.Log("einMal"+ einMal); }
       // if (MasterclientDestory.isTrigger) { stopButton.SetActive(true); MasterclientDestory.isTrigger = false; }
    }

    /// <summary>
    /// 如果积木进入碰撞体，那么把物体的transform加入list.
    /// 同时随机产生敌人，随机经行物体的大小变化
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Move") 
        { 
            boxInColliderList.Add(other.gameObject);
            //Debug.Log("现有" + boxInColliderList.Count); 
              
        }
       if (boxInColliderList.Count > 3 && !isEmeyGenerate && other.tag == "Move")
        {
            isEmeyGenerate = true;
            
        }

        if (other.tag == "CylinderBird") { Invoke("ArrayListDiepiert", 1); }

        if (other.tag=="shell") {Invoke("ArrayListAddForce", 1); }

        zahl = Random.Range(0, 100);
        //随机改变物体大小
        if (boxInColliderList.Count >= 3&& einMal)//抖动
        {
            arrayListZahl = Random.Range(0, boxInColliderList.Count);
    
            if (zahl <= 10) 
            { 
               Debug.Log("20"); 
                boxInColliderList[arrayListZahl].transform.DOBlendableMoveBy(new Vector3(0.1f, 0.1f, 0.1f), 1); 
            }
            else if (zahl <= 20 && zahl < 80) 
            { 
                Debug.Log("50"); 
                //boxInColliderList[arrayListZahl].transform.DOScale(Vector3.one * 5, 2); 
            }
            else if (zahl >= 90 && zahl <= 100) 
            { Debug.Log("70"); 
                //boxInColliderList[arrayListZahl].transform.DOBlendableMoveBy(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
            } //arrayListObject.GetComponent<GameObject>().transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 2, 10, 0.1f); }
            einMal = false;
        }
       
        


    }

    /// <summary>
    /// 先是检查list里面的物体，然后随机杀掉一个。将杀掉的移除列表
    /// </summary>
    public void ArrayListDiepiert() 
    {
        if (boxInColliderList.Count == 0) return;
        arrayListZahl = Random.Range(0, boxInColliderList.Count);
        
        boxInColliderList[arrayListZahl].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);

        PhotonNetwork.Destroy(boxInColliderList[arrayListZahl].gameObject);
        boxInColliderList.Remove(boxInColliderList[arrayListZahl]);
        
        //后面继续写
        //boxInColliderList.Remove(boxInColliderList[arrayListZahl]);
        Debug.Log("UFO，UFO coming");
    }

    //子弹爆炸用这里，子弹弹飞积木，积木飞起来就修改tag，防止再次产生敌人
    //2秒之后把tag修改回来
    public void ArrayListAddForce() 
    {
        if (boxInColliderList.Count == 0) return;
        arrayListZahl = Random.Range(0, boxInColliderList.Count);
        boxInColliderList[arrayListZahl].tag = "MoveCube";
        Debug.Log("第一个"+boxInColliderList[arrayListZahl].tag);
        boxInColliderList[arrayListZahl].GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
        Debug.Log("子弹来啦，shell coming");
        // StartCoroutine(TagReset(boxInColliderList[arrayListZahl]));
        // 创建一个 Tweener 对象， 另 number的值在 5 秒内变化到 100  
        //Tween t = DOTween.To(() => number, x => number = x, 100, 5);

       float timer = 0;
        Tween t = DOTween.To(() => timer, x => timer = x, 1, 2)
                      .OnStepComplete(() =>
                      {
                          print("use");
                          boxInColliderList[arrayListZahl].tag = "Move";
                          Debug.Log("2." + boxInColliderList[arrayListZahl].tag);
                      })
                      .SetLoops(0);
        Debug.Log("3." + boxInColliderList[arrayListZahl].tag);
    }


    //https://blog.csdn.net/qq617119142/article/details/43606043
    /// <summary>
    /// 再写一个出生地
    /// </summary>
    /// <returns></returns>
    private Vector3 InCircle()
    {
        // Vector2 p = Random.insideUnitCircle * 1;
        // Vector2 pos = p.normalized * (2 + p.magnitude);
        // Vector3 pos2 = new Vector3(pos.x, 0, pos.y);
        // return pos2;
        //int i = Random.Range(0, 5);

        //int x = Random.Range(0, 5);

        Vector3 p;
        int i = 0;
        do
        {
            i = Random.Range(-4, 4);
        } while (i == 0);
        int j = 0;
        do
        {
            j = Random.Range(-4, 4);
        } while (j == 0);
        p = new Vector3(this.transform.position.x+i, this.transform.position.y+3, this.transform.position.z+j);
       
        Debug.Log("p的大小"+p);
        return p;
    }
    /// <summary>
    /// 如果积木掉出碰撞体，那么把物体的transform移出list.
    /// 增加随机积木出现
    /// 后面修改，不要生成游戏物体，通过Setactive来做；
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Move")
        {
            boxInColliderList.Remove(other.gameObject);
            
            CubeInSecneArray = GameObject.FindGameObjectsWithTag("Move");
            if (CubeInSecneArray.Length<=35 ) {
                isTriggeExit = true;
            }
           
        }
       
        
        
       
    }


 

}
