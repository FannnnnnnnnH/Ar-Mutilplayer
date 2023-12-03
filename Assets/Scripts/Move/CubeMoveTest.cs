using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CubeMoveTest : MonoBehaviourPun
{
	public GameObject cubePlatz;

	//���ƶ�������
	private GameObject Element;
	//������Ϊ��ľ��
	private LayerMask blockMask = 1 << 6;
	//public GameObject[] players = new GameObject[5];

	//��ֹ��һ������Ĳ���CUBe������������
	public bool isLaydown = false;
	//�ж��Ƿ���갴��
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

			//������
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
				isMouseDown = true;//�����һ��û�е������Element���ж�Ϊ��
				isLaydown = true;
				StartCoroutine(Translate());
			}
		}
		//����ſ������ô���лָ�ԭ��
		if (Input.GetMouseButtonUp(0) && isLaydown)//Input.GetMouseButtonUp(0)&& isOk
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
	/// �ƶ���ľ����
	/// </summary>

	IEnumerator Translate()
	{
		
		while (Input.GetMouseButton(0))
		{
			Element.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Element.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			// �洢������Ļ�ռ����꣨Zֵʹ��Ŀ���������Ļ�ռ����꣩
			
			Element.transform.position = cubePlatz.transform.position;

			// �ȴ��̶����� 
			yield return new WaitForFixedUpdate();
		}
	}

}
