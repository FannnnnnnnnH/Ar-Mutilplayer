using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
/// <summary>
/// 
/// </summary>
public enum Sub
{
    erste,
    zweite,
    dritte,
    vierte
}
public class CubeSub : MonoBehaviour
{

    public bool chosenRace = false; 
    Hashtable playerCustomSettings = new Hashtable(); 
    public int playersReady = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }
   

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
