using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class EnemyDetection : MonoBehaviour
{
    //public GameObject enemy;
    //public Button enemyButton;
    //用在IsButtoninscence中
    public static bool wirdEmeyGefunden = false;
    public static bool rayActiv = true;
    private LayerMask blockMask = 1 << 7;
    /*
    public Camera ArCamera;
    // Ar射线碰撞管理器
    ARRaycastManager arRaycast;

    // Ar射线碰到的物件和集合
    static private List<ARRaycastHit> hits = new List<ARRaycastHit>();*/
    private void Start()
    {
       // arRaycast = GetComponent<ARRaycastManager>();
        wirdEmeyGefunden = false;
        rayActiv = true;
        //enemyButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (MasterclientDestory.isTrigger) 
        {
            Vector3 centerOFScreen = new Vector3(Screen.width / 2, Screen.height / 2);
            // Ray ray = ArCamera.ScreenPointToRay(centerOFScreen);
            Ray ray = Camera.main.ScreenPointToRay(centerOFScreen);
            RaycastHit hit;
           

            if (Physics.Raycast(ray, out hit, 500, blockMask))//Camera.main.ScreenPointToRay(Input.mousePosition);//Physics.Raycast(ray, out hit, 500, blockMask)
            {

                wirdEmeyGefunden = true;
                Debug.Log(blockMask);
                MasterclientDestory.isTrigger = false;
                MasterclientDestory.isTankda = true;
            }
        }
        

    }
}
