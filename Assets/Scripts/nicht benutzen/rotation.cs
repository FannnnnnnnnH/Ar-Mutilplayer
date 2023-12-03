using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }
}
