using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// ����Gamemanage����
/// ���ƻ�ľ�����ƶ��Ĵ���
/// </summary>
public class testMove : MonoBehaviourPun
{
	private Vector3 _vec3TargetScreenSpace;// Ŀ���������Ļ�ռ�����
	private Vector3 _vec3TargetWorldSpace;// Ŀ�����������ռ�����
	//private Transform _trans;// Ŀ������Ŀռ�任���
	private Vector3 _vec3MouseScreenSpace;// ������Ļ�ռ�����
	private Vector3 _vec3Offset;// ƫ��
	
	//���ƶ�������
	private GameObject Element;
	//������Ϊ��ľ��
	private LayerMask blockMask = 1<<6;
	//public GameObject[] players = new GameObject[5];

	//��ֹ��һ������Ĳ���CUBe������������
	public bool isLaydown = false;
	//�ж��Ƿ���갴��
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

			//������
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			//Debug.Log("Jaaa");
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 10000,blockMask))
			{
				//Debug.Log("++"+blockMask.value);
				//��Ҫ�ƶ���Tag��Move
				//if (hit.transform.tag == "Move")
				//{
					_generCur.GetComponent<MeshRenderer>().enabled = true;
					Element = hit.collider.gameObject;
					//if (Element.GetComponent<PhotonViphotonViewew>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
					if (Element.GetComponent<PhotonView>().Owner.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
					{
						Element.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
						//Debug.Log("��ID");
						//grabObject();
					}
					//��Gamewinnen����ʹ��
					isMouseDown = true;

					//�����һ��û�е������Element���ж�Ϊ��
					isLaydown = true;
				//Element.GetComponent<MeshRenderer>().material.color = Color.red;
				

				//Debug.Log("MauseDown");
					StartCoroutine(Translate());
			}


		}
		//����ſ������ô���лָ�ԭ��
		if (Input.GetMouseButtonUp(0)&&isLaydown)//Input.GetMouseButtonUp(0)&& isOk
		{
			//Element.GetComponent<MeshRenderer>().material.color = Color.white;
			Element.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			isMouseDown = false;
			isLaydown = false;
			Element = null;
			_generCur.GetComponent<MeshRenderer>().enabled = false;
			//ColliderBoxInArry();
			//����������
		}
	}

	/// <summary>
	/// ��װ��ΧΪ����С����ⷽ���Ƿ��ڼ�ⷶΧ�ڣ��ǵĻ�����������
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
	/// �ƶ���ľ����
	/// </summary>

	IEnumerator Translate()
	{
		// ��Ŀ�����������ռ�����ת�������������Ļ�ռ����� 
		_vec3TargetScreenSpace = Camera.main.WorldToScreenPoint(Element.transform.position);

		// �洢������Ļ�ռ����꣨Zֵʹ��Ŀ���������Ļ�ռ����꣩
		_vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);

		// ����Ŀ���������������������ռ��е�ƫ���� 
		_vec3Offset = Element.transform.position - Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace);
		

		// ���������� 
		while (Input.GetMouseButton(0))
		{
			Element.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Element.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			// �洢������Ļ�ռ����꣨Zֵʹ��Ŀ���������Ļ�ռ����꣩
			_vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);

			// ��������Ļ�ռ�����ת��������ռ����꣨Zֵʹ��Ŀ���������Ļ�ռ����꣩������ƫ�������Դ���ΪĿ�����������ռ�����
			_vec3TargetWorldSpace = Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace) + _vec3Offset;

			// ����Ŀ�����������ռ����� 
			Element.transform.position = _vec3TargetWorldSpace;

			// �ȴ��̶����� 
			yield return new WaitForFixedUpdate();
		}
	}


}
