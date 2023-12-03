using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    private Rigidbody rd;
    public float force = 0.1F;
    // Start is called before the first frame update
    void Start()
    {
        rd = this.GetComponent<Rigidbody>();//给变量赋值变量
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");

        float h = Input.GetAxis("Horizontal");//得到键盘左右控制
        //ForceMode.Impulse利用其质量，在刚体上加一个瞬时力冲量。
        rd.AddForce(new Vector3(h, 0, v) * force, ForceMode.Impulse);//对物体施加力
        //Force：向刚体施加连续的力（意味着物体受到force参数的力，时间为一帧的时间），考虑其质量，即同样的力施加在越重的物体上产生的加速度越小。
    }
}
