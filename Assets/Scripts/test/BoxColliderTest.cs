using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// ��ײ�����ľ�Ƿ���ƽ̨�ϣ��еĻ��ͼ���list
/// ���Ƴ����е�ͻ���¼�
/// 1.ƽ̨��ľ�����ţ�����������
/// 2.�ⲿͻ���¼������ɵ��ˣ����ɵ���
/// </summary>
public class BoxColliderTest : MonoBehaviour
{
   
    //�����
    private int zahl;
    //������ײ������Ļ�ľ���б�
    public List<GameObject> boxInColliderList = new List<GameObject>();

    GameObject[] CubeInSecneArray;

    int arrayListZahl;
    //���ɵĵ���
    GameObject birdObject;
    //�������ɵ�λ��
    //private Vector3 enemyTransf;
    //��masterclientDestory��ʹ��
    public static bool isEmeyGenerate=false;
    public static bool isTriggeExit = false;

    bool einMal = true;

    //�ҵ����
   // GameObject canvasMenu;
    //�ҵ��������İ�ť
   // GameObject stopButton;
   
    PhotonView photonView;

    private string[] cubeNameInResources = new string[9] { "001", "002", "003", "004", "005", "006", "007", "008", "009" };

    int[] count = new int[] { -5, -3, 3, 5, -4, 4 };

    private int cubeID;

    //GameObject buttonTrigger;
    
    //Բ��
    public GameObject circleCenter;

    Vector3 pos2;
    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);
        //canvasMenu = GameObject.Find("Canvas");
       // stopButton = canvasMenu.transform.Find("EventTrigge").gameObject;
        boxInColliderList.Clear();
        //Debug.Log("�տ�ʼ��Ŀ��"+boxInColliderList.Count);
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
    /// �����ľ������ײ�壬��ô�������transform����list.
    /// ͬʱ����������ˣ������������Ĵ�С�仯
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Move") 
        { 
            boxInColliderList.Add(other.gameObject);
            //Debug.Log("����" + boxInColliderList.Count); 
              
        }
       if (boxInColliderList.Count > 3 && !isEmeyGenerate && other.tag == "Move")
        {
            isEmeyGenerate = true;
            
        }

        if (other.tag == "CylinderBird") { Invoke("ArrayListDiepiert", 1); }

        if (other.tag=="shell") {Invoke("ArrayListAddForce", 1); }

        zahl = Random.Range(0, 100);
        //����ı������С
        if (boxInColliderList.Count >= 3&& einMal)//����
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
    /// ���Ǽ��list��������壬Ȼ�����ɱ��һ������ɱ�����Ƴ��б�
    /// </summary>
    public void ArrayListDiepiert() 
    {
        if (boxInColliderList.Count == 0) return;
        arrayListZahl = Random.Range(0, boxInColliderList.Count);
        
        boxInColliderList[arrayListZahl].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);

        PhotonNetwork.Destroy(boxInColliderList[arrayListZahl].gameObject);
        boxInColliderList.Remove(boxInColliderList[arrayListZahl]);
        
        //�������д
        //boxInColliderList.Remove(boxInColliderList[arrayListZahl]);
        Debug.Log("UFO��UFO coming");
    }

    //�ӵ���ը������ӵ����ɻ�ľ����ľ���������޸�tag����ֹ�ٴβ�������
    //2��֮���tag�޸Ļ���
    public void ArrayListAddForce() 
    {
        if (boxInColliderList.Count == 0) return;
        arrayListZahl = Random.Range(0, boxInColliderList.Count);
        boxInColliderList[arrayListZahl].tag = "MoveCube";
        Debug.Log("��һ��"+boxInColliderList[arrayListZahl].tag);
        boxInColliderList[arrayListZahl].GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
        Debug.Log("�ӵ�������shell coming");
        // StartCoroutine(TagReset(boxInColliderList[arrayListZahl]));
        // ����һ�� Tweener ���� �� number��ֵ�� 5 ���ڱ仯�� 100  
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
    /// ��дһ��������
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
       
        Debug.Log("p�Ĵ�С"+p);
        return p;
    }
    /// <summary>
    /// �����ľ������ײ�壬��ô�������transform�Ƴ�list.
    /// ���������ľ����
    /// �����޸ģ���Ҫ������Ϸ���壬ͨ��Setactive������
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
