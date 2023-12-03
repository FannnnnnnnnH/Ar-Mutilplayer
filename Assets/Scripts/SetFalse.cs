using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Canvas->GewinnePanel
/// </summary>
public class SetFalse : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameManager;
    private GameWinnen _gewinn;
    
    void Start()
    {
       
        this.gameObject.SetActive(false);
        gameManager.GetComponent<CubeMoveTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameWinnen._isWinn) { gameManager.GetComponent<CubeMoveTest>().enabled = false; }
        //if (!GameWinnen._isWinn&&_einmal) { gameManager.GetComponent<testMove>().enabled = true;return; }
       
    }
}
