using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


/// <summary>
/// 游戏获胜的判定
/// 挂GameWinn上面
/// </summary>
public class GameWinnen : MonoBehaviour
{
    PhotonView photonView;
    // Start is called before the first frame update
    //倒计时开始数字
    public float timeLeft=5f;
    //文本显示倒计时
    private GameObject countText;

    //是否获取胜利
    public static bool _isWinn;

    public bool _MousSituation;
    //是否满足5秒条件
    private bool _isFunfSekunden;
    //找到面板
    GameObject canvasMenu;
    //
    GameObject winPanel;
    private GameObject gameManager;

    void Start()
    {
        _isWinn = false;
       
        winPanel = canvasMenu.transform.Find("GewinnePanel").gameObject;
        gameManager = GameObject.Find("GameManager");
       // gameManager.GetComponent<testMove>().enabled = tr;
        photonView = PhotonView.Get(this);
        //winPanel.SetActive(false);
       // _isCounting = true;
        _isFunfSekunden = false;
        countText.gameObject.SetActive(false);
    }
    private void Awake()
    {
        canvasMenu = GameObject.Find("Canvas");
        _MousSituation = GameObject.Find("GameManager").GetComponent<CubeMoveTest>().isMouseDown;
        countText = canvasMenu.transform.Find("Noten").gameObject;//GameObject.FindWithTag("Noten").GetComponent<GameObject>();
    }

    // 两个物体相互接触摩擦时 会不停的调用该函数
    private void OnTriggerStay(Collider collision)
    {
        _MousSituation = GameObject.Find("GameManager").GetComponent<CubeMoveTest>().isMouseDown;
        //  Debug.Log("hiiiiiiiiiiiiii");
        if (collision.tag == "Move" || collision.tag == "MoveCube")
        {
            if (timeLeft > 0 && !_MousSituation)
            {
                timeLeft -= Time.deltaTime;
                countText.gameObject.SetActive(true);
                countText.GetComponent<Text>().text = timeLeft.ToString(format: "0");
                // Debug.Log("hiiiiiiiiiiiiii");
            }
            if (_MousSituation)
            {
                timeLeft = 5;
                countText.gameObject.SetActive(false);
            }

            if (timeLeft <= 0 && !_MousSituation)
            {
                _isFunfSekunden = true;
                //这里后期加上游戏胜利
                Debug.Log("gewonnen!!!");
                _isWinn = true;
                //Winne();
                photonView.RPC("Winne", RpcTarget.All);
                //photonView.RPC("Uppp", RpcTarget.All);
            }
        }
        else 
        { 
            timeLeft = 5;
            countText.GetComponent<Text>().text = timeLeft.ToString(format: "0"); }

        //print(this.name + "一直在和" + collision.gameObject.name + "接触");
    }
    [PunRPC]
    public void Winne() 
    {
        winPanel.SetActive(true);
        gameManager.GetComponent<CubeMoveTest>().enabled=false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Move" || other.tag == "MoveCube")
        {
            timeLeft = 5;
        }
    }
}
