using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CubeMoveTest : MonoBehaviourPun
{
	public GameObject cubePlatz;

	//被移动的物体
	private GameObject Element;
	//第六层为积木层
	private LayerMask blockMask = 1 << 6;
	//public GameObject[] players = new GameObject[5];

	//防止第一个点击的不是CUBe而产生的误判
	public bool isLaydown = false;
	//判断是否鼠标按下
	public bool isMouseDown = false;

	private GameObject _generCur;

	//private PhotonView pv;
	public Sub zuErstTest = Sub.erste;

	void Awake()
	{ //players[i].GetComponent<Collider>(); 
		_generCur = GameObject.FindWithTag("Anchor");
	}
	private void Start()
	{
		_generCur.GetComponent<MeshRenderer>();
		//pv=this.GetComponent<PhotonView>();
	}

	void Update()
	{
		//if (!pv.IsMine) { return; }

		if (Input.GetMouseButtonDown(0))
		{

			//发射线
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			//Debug.Log("Jaaa");
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 3, blockMask))
			{
				_generCur.GetComponent<MeshRenderer>().enabled = true;
				Element = hit.collider.gameObject;
				//if (Element.GetComponent<PhotonViphotonViewew>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
				//if (Element.GetComponent<PhotonView>().OwnershipTransfer == OwnershipOption.Fixed) { return; }
				if (!Element.GetComponent<PhotonView>().IsMine && Element.GetComponent<PhotonView>().OwnershipTransfer == OwnershipOption.Fixed) { return; }
				if (Element.GetComponent<PhotonView>().OwnershipTransfer == OwnershipOption.Takeover)//Element.GetComponent<PhotonView>().Owner.ActorNumber!= PhotonNetwork.LocalPlayer.ActorNumber
				{
					Element.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
					
				}
				isMouseDown = true;//避免第一次没有点击方块Element被判定为空
				isLaydown = true;
				StartCoroutine(Translate());
			}
		}
		//如果放开鼠标那么所有恢复原样
		if (Input.GetMouseButtonUp(0) && isLaydown)//Input.GetMouseButtonUp(0)&& isOk
		{
			//Element.GetComponent<MeshRenderer>().material.color = Color.white;
			Element.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			isMouseDown = false;
			isLaydown = false;
			Element = null;
			_generCur.GetComponent<MeshRenderer>().enabled = false;
			//ColliderBoxInArry();
			//当物体落下
		}
	}



	/// <summary>
	/// 移动积木代码
	/// </summary>

	IEnumerator Translate()
	{
		
		while (Input.GetMouseButton(0))
		{
			Element.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Element.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			// 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）
			
			Element.transform.position = cubePlatz.transform.position;

			// 等待固定更新 
			yield return new WaitForFixedUpdate();
		}
	}

}
