using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ȼ˵��Cubemove
/// ���ǹ����Ǳ��ƶ���Cube���·�����������һ��ê�����ȷ��CUbe��xy��
/// </summary>
public class AnchorMove : MonoBehaviour
{
    public GameObject _generCur;

    private void Awake()
    {
        _generCur = GameObject.FindWithTag("Anchor");
    }
   
    IEnumerator OnMouseDown()
    {
            while (Input.GetMouseButton(0))
            {
                //Debug.Log("Jaaa");
                RaycastHit hit;

                if (Physics.Raycast(transform.position, Vector3.down, out hit))
                {
                   // _generCur.SetActive(true);
                    _generCur.transform.position = hit.point;
                    //Debug.Log(_generCur.transform.position);
                }
                yield return null;
            }
    }

    }

