using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 虽然说叫Cubemove
/// 但是功能是被移动的Cube向下发射射线生成一个锚点可以确认CUbe的xy轴
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

