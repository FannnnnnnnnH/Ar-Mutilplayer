using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //移动到的坐标，移动的时长
       // transform.DOMove(Vector3.one,1);

       
    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKey(KeyCode.Q)) { transform.DORotate(new Vector3(20,30,20), 2); }
        //if (Input.GetKey(KeyCode.E)) { transform.DOScale(Vector3.one * 0.5f, 2); };
        // DOBlendableMoveBy方法有两个特点
        //允许多个同时执行
        //transform.DOBlendableMoveBy(new Vector3(1, 1, 1), 1);
        //transform.DOBlendableMoveBy(new Vector3(-1, 0, 0), 1);
        //假设其实点为（0,0,0），最后动画停止时的坐标就是（0,1,1）
        //它是增量动画transform.DOBlendableMoveBy(new Vector3(1, 1, 1), 1);
        //if (Input.GetKey(KeyCode.Q)) { transform.DOBlendableMoveBy(new Vector3(1, 1, 1), 2); }
        //if (Input.GetKey(KeyCode.E)) { transform.DOBlendableRotateBy(new Vector3(1, 1, 1), 2); };
        //假设其实点为（1,1,1），最后动画停止时的坐标就是（2,2,2）
        //transform.DOBlendableRotateBy()//transform.DOBlendableScaleBy()//transform.DOBlendablePunchRotation()同理

        //第一个参数 punch：表示方向及强度
        //第二个参数 duration：表示动画持续时间
        //第三个参数 vibrato：震动次数
        //第四个参数 elascity: 这个值是0到1的
        // 当为0时，就是在起始点到目标点之间运动
        // 不为0时，会把你赋的值乘上一个参数，作为你运动方向反方向的点，物体在这个点和目标点之间运动
        if (Input.GetKey(KeyCode.F)) { transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 2,10,0.1f); }
        //持续时间，整幅大小力的大小，震动次数，随机性
        //参数：持续时间，力量，震动，随机性，淡出
        //力量：实际就是震动的幅度,可以理解成相机施加的力的大小 使用Vector3可以选择每个轴向不同的强度
        //震动：震动次数
        //随机性：改变震动方向的随机值（大小：0~180）
        //淡出：就是运动最后是否缓慢移动回到原本位置
        //true就会回到游戏最初开始位置
        if (Input.GetKey(KeyCode.G)) { transform.DOShakePosition(2, new Vector3(0.01f, 0, 0.01f), 10, 5); }
    }
}
