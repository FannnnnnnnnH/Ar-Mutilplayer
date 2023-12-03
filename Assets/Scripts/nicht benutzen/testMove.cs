using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// 挂在Gamemanage上面
/// 控制积木方块移动的代码
/// </summary>
public class testMove : MonoBehaviourPun
{
	private Vector3 _vec3TargetScreenSpace;// 目标物体的屏幕空间坐标
	private Vector3 _vec3TargetWorldSpace;// 目标物体的世界空间坐标
	//private Transform _trans;// 目标物体的空间变换组件
	private Vector3 _vec3MouseScreenSpace;// 鼠标的屏幕空间坐标
	private Vector3 _vec3Offset;// 偏移
	
	//被移动的物体
	private GameObject Element;
	//第六层为积木层
	private LayerMask blockMask = 1<<6;
	//public GameObject[] players = new GameObject[5];

	//防止第一个点击的不是CUBe而产生的误判
	public bool isLaydown = false;
	//判断是否鼠标按下
	public bool isMouseDown=false;

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
			if (Physics.Raycast(ray, out hit, 10000,blockMask))
			{
				//Debug.Log("++"+blockMask.value);
				//我要移动的Tag是Move
				//if (hit.transform.tag == "Move")
				//{
					_generCur.GetComponent<MeshRenderer>().enabled = true;
					Element = hit.collider.gameObject;
					//if (Element.GetComponent<PhotonViphotonViewew>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
					if (Element.GetComponent<PhotonView>().Owner.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
					{
						Element.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
						//Debug.Log("改ID");
						//grabObject();
					}
					//给Gamewinnen代码使用
					isMouseDown = true;

					//避免第一次没有点击方块Element被判定为空
					isLaydown = true;
				//Element.GetComponent<MeshRenderer>().material.color = Color.red;
				

				//Debug.Log("MauseDown");
					StartCoroutine(Translate());
			}


		}
		//如果放开鼠标那么所有恢复原样
		if (Input.GetMouseButtonUp(0)&&isLaydown)//Input.GetMouseButtonUp(0)&& isOk
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
	/// 盒装范围为检测大小，检测方块是否在检测范围内，是的话加入数组中
	/// </summary>
	public void ColliderBoxInArry() 
	{
		Collider[] collider = Physics.OverlapBox(Vector3.zero, Vector3.one);
		for(int i = 0; i < collider.Length; ++i)
		{
			Debug.Log(collider[i].gameObject.name);
		}
	}

	/// <summary>
	/// 移动积木代码
	/// </summary>

	IEnumerator Translate()
	{
		// 把目标物体的世界空间坐标转换到它自身的屏幕空间坐标 
		_vec3TargetScreenSpace = Camera.main.WorldToScreenPoint(Element.transform.position);

		// 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）
		_vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);

		// 计算目标物体与鼠标物体在世界空间中的偏移量 
		_vec3Offset = Element.transform.position - Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace);
		

		// 鼠标左键按下 
		while (Input.GetMouseButton(0))
		{
			Element.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Element.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			// 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）
			_vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);

			// 把鼠标的屏幕空间坐标转换到世界空间坐标（Z值使用目标物体的屏幕空间坐标），加上偏移量，以此作为目标物体的世界空间坐标
			_vec3TargetWorldSpace = Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace) + _vec3Offset;

			// 更新目标物体的世界空间坐标 
			Element.transform.position = _vec3TargetWorldSpace;

			// 等待固定更新 
			yield return new WaitForFixedUpdate();
		}
	}


}
