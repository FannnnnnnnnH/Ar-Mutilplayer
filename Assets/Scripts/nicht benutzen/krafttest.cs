using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class krafttest : MonoBehaviour
{
    public int i;
    public int j;
    public GameObject obEin;
    public GameObject obZwei;
    public List<GameObject> boxInColliderList = new List<GameObject>();
    public GameObject uiMoveBy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            uiMoveBy.transform.DOShakePosition(1, new Vector3(0, i, 0));
            uiMoveBy.transform.DOShakeRotation(1, new Vector3(0, 0,j));
            // obEin.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
            // obZwei.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
            foreach (GameObject obj in boxInColliderList)
            {
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 3, ForceMode.Impulse);
            }
           // Debug.Log(obEin.name);
        }
    }
}
