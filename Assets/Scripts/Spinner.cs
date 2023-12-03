using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public bool doSpin = false;
    public float spinSpeed = 3600;

    public GameObject playerGraphics;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (doSpin)
        {
            playerGraphics.transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
        }
    }
}
