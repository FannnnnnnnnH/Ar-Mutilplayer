using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�ƶ��������꣬�ƶ���ʱ��
       // transform.DOMove(Vector3.one,1);

       
    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKey(KeyCode.Q)) { transform.DORotate(new Vector3(20,30,20), 2); }
        //if (Input.GetKey(KeyCode.E)) { transform.DOScale(Vector3.one * 0.5f, 2); };
        // DOBlendableMoveBy�����������ص�
        //������ͬʱִ��
        //transform.DOBlendableMoveBy(new Vector3(1, 1, 1), 1);
        //transform.DOBlendableMoveBy(new Vector3(-1, 0, 0), 1);
        //������ʵ��Ϊ��0,0,0������󶯻�ֹͣʱ��������ǣ�0,1,1��
        //������������transform.DOBlendableMoveBy(new Vector3(1, 1, 1), 1);
        //if (Input.GetKey(KeyCode.Q)) { transform.DOBlendableMoveBy(new Vector3(1, 1, 1), 2); }
        //if (Input.GetKey(KeyCode.E)) { transform.DOBlendableRotateBy(new Vector3(1, 1, 1), 2); };
        //������ʵ��Ϊ��1,1,1������󶯻�ֹͣʱ��������ǣ�2,2,2��
        //transform.DOBlendableRotateBy()//transform.DOBlendableScaleBy()//transform.DOBlendablePunchRotation()ͬ��

        //��һ������ punch����ʾ����ǿ��
        //�ڶ������� duration����ʾ��������ʱ��
        //���������� vibrato���𶯴���
        //���ĸ����� elascity: ���ֵ��0��1��
        // ��Ϊ0ʱ����������ʼ�㵽Ŀ���֮���˶�
        // ��Ϊ0ʱ������㸳��ֵ����һ����������Ϊ���˶����򷴷���ĵ㣬������������Ŀ���֮���˶�
        if (Input.GetKey(KeyCode.F)) { transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 2,10,0.1f); }
        //����ʱ�䣬������С���Ĵ�С���𶯴����������
        //����������ʱ�䣬�������𶯣�����ԣ�����
        //������ʵ�ʾ����𶯵ķ���,�����������ʩ�ӵ����Ĵ�С ʹ��Vector3����ѡ��ÿ������ͬ��ǿ��
        //�𶯣��𶯴���
        //����ԣ��ı��𶯷�������ֵ����С��0~180��
        //�����������˶�����Ƿ����ƶ��ص�ԭ��λ��
        //true�ͻ�ص���Ϸ�����ʼλ��
        if (Input.GetKey(KeyCode.G)) { transform.DOShakePosition(2, new Vector3(0.01f, 0, 0.01f), 10, 5); }
    }
}
