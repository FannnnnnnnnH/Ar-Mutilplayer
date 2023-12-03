using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GPS : MonoBehaviour
{
    string GetGps = "";
    public Text ShowGPS;
    /// <summary>
    /// ��ʼ��һ��λ��
    /// </summary>
    void Start()
    {
        StartCoroutine(StartGPS());
        GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
        GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;
        ShowGPS.text = GetGps;
        Debug.Log(GetGps);
    }
    /// <summary>
    /// ˢ��λ����Ϣ����ť�ĵ���¼���
    /// </summary>
    public void updateGps()
    {
        StartCoroutine(StartGPS());
        GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
        GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;
        ShowGPS.text = GetGps;
        Debug.Log(GetGps);
    }
    /// <summary>
    /// ֹͣˢ��λ�ã���ʡ�ֻ�������
    /// </summary>
    void StopGPS()
    {
        Input.location.Stop();
    }

    IEnumerator StartGPS()
    {
        // Input.location ���ڷ����豸��λ�����ԣ��ֳ��豸��, ��̬��LocationServiceλ��  
        // LocationService.isEnabledByUser �û�������Ķ�λ�����Ƿ�����  
        if (!Input.location.isEnabledByUser)
        {
            GetGps = "isEnabledByUser value is:" + Input.location.isEnabledByUser.ToString() + " Please turn on the GPS";
            yield return false;
        }

        // LocationService.Start() ����λ�÷���ĸ���,���һ��λ������ᱻʹ��  
        Input.location.Start(10.0f, 10.0f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            // ��ͣЭͬ�����ִ��(1��)  
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            GetGps = "Init GPS service time out";
            yield return false;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GetGps = "Unable to determine device location";
            yield return false;
        }
        else
        {
            GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
            GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;
            yield return new WaitForSeconds(100);
        }
    }

}
