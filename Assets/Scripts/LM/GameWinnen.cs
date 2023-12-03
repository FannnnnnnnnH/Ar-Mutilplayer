using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


/// <summary>
/// ��Ϸ��ʤ���ж�
/// ��GameWinn����
/// </summary>
public class GameWinnen : MonoBehaviour
{
    PhotonView photonView;
    // Start is called before the first frame update
    //����ʱ��ʼ����
    public float timeLeft=5f;
    //�ı���ʾ����ʱ
    private GameObject countText;

    //�Ƿ��ȡʤ��
    public static bool _isWinn;

    public bool _MousSituation;
    //�Ƿ�����5������
    private bool _isFunfSekunden;
    //�ҵ����
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

    // ���������໥�Ӵ�Ħ��ʱ �᲻ͣ�ĵ��øú���
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
                //������ڼ�����Ϸʤ��
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

        //print(this.name + "һֱ�ں�" + collision.gameObject.name + "�Ӵ�");
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
