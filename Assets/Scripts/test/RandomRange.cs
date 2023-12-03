using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRange : MonoBehaviour
{
    private GameObject arrayList;
    Transform _transform;
    static Vector3 _pos;
    static int[] intXY = new int[8] {9,4,6,7,-5,-8,-4,-7 };
    
   
    private void Start()
    {
       
    }

    public static Vector3 GetRandomVector3()
    {
        _pos =GameObject.Find("ArrayList").transform.position;
        int xyID = Random.Range(0,7);
        Vector3 _v3;
        _v3 = new Vector3(_pos.x+intXY[xyID], _pos.y - 0.2f, _pos.z + intXY[xyID]);

        return _v3;
        //return v3.normalized;
    }
    public static Vector3 GetRandomVector3Rotation()
    {
        Vector3 vRotation = new Vector3(Random.Range(-90.0f, 90.0f),0, Random.Range(-90.0f, 90.0f));
        return vRotation;
    }

    public static Vector3 GetRandomVector3RotationTest()
    {
        Vector3 vRotation = new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));
        return vRotation;
    }

}
