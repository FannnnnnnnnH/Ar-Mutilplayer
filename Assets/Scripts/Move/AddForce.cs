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
        rd = this.GetComponent<Rigidbody>();//��������ֵ����
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");

        float h = Input.GetAxis("Horizontal");//�õ��������ҿ���
        //ForceMode.Impulse�������������ڸ����ϼ�һ��˲ʱ��������
        rd.AddForce(new Vector3(h, 0, v) * force, ForceMode.Impulse);//������ʩ����
        //Force�������ʩ��������������ζ�������ܵ�force����������ʱ��Ϊһ֡��ʱ�䣩����������������ͬ������ʩ����Խ�ص������ϲ����ļ��ٶ�ԽС��
    }
}
