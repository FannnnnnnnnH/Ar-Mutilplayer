using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;
//using DG.Tweening;

/// <summary>
/// https://blog.csdn.net/chy_xfn/article/details/40031697?ops_request_misc=%257B%2522request%255Fid%2522%253A%2522167062885116800186544198%2522%252C%2522scm%2522%253A%252220140713.130102334..%2522%257D&request_id=167062885116800186544198&biz_id=0&utm_medium=distribute.pc_search_result.none-task-blog-2~all~sobaiduend~default-1-40031697-null-null.142^v68^wechat_v2,201^v4^add_ask,213^v2^t3_esquery_v2&utm_term=unity3d%E6%95%8C%E4%BA%BA%E5%90%91%E7%8E%A9%E5%AE%B6%E9%9D%A0%E8%BF%91&spm=1018.2226.3001.4187
/// </summary>
public class BirdsAttribute : MonoBehaviour
{
    // Start is called before the first frame update
    private float distanceToMe;           //智能体到目标的距离
    private GameObject zielPo;                //目标角色
   // private float isSeekDistance = 3.0f;  //可靠近范围
   // private int state;                     //智能体状态
    private float timeLeft = 5f;

    private bool testMove=true;
    private bool testMove1 = true;

    public GameObject cylinderBird;
    public GameObject spotLicht;
    PhotonView view;


    void Start()
    {
        testMove=true;
        testMove1 = true;
        cylinderBird.SetActive(false);
        spotLicht.SetActive(false);
        // me = GameObject.FindGameObjectWithTag("Respawn");
        zielPo = GameObject.Find("Ziel");
        view = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        zielPo = GameObject.Find("Ziel");
        distanceToMe = Vector3.Distance(zielPo.transform.position, this.transform.position);
        timeLeft -= Time.deltaTime;
        if (timeLeft > 0 && testMove) 
        {
            //transform.up
            this.transform.RotateAround(zielPo.transform.position, Vector3.up, 45 * Time.deltaTime);//new vector me.transform.position
        }
        else if(timeLeft <= 0 && testMove)
        {
            // this.transform.LookAt(me.transform.position);               //该方法使智能体总是面对目标
            //transform.Rotate(new Vector3(0, 0.1f, 0));

          Tween t=  transform.DOMove(new Vector3(zielPo.transform.position.x,this.transform.position.y, zielPo.transform.position.z),1);
            t.OnComplete(() => { transform.DOBlendableMoveBy(new Vector3(0, -3.5f, 0), 2); }
                );
            testMove = false;
        }

        if (distanceToMe <= 1 && testMove1)
        {
            testMove1 = false;
            transform.DOShakePosition(2, new Vector3(0.03f, 0, 0.03f), 5, 2);
            Debug.Log("DOShakePosition");
            cylinderBird.SetActive(true);
            spotLicht.SetActive(true);
            //Debug.Log("distanceTo3"+ distanceToMe);

        }
    }


   public void TEST()
    {
        PhotonView view = GetComponent<PhotonView>();
        if (view != null && view.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        
       // this.gameObject.SetActive(false);
    }
   
}
