using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movelog: MonoBehaviour
{

    //public static string logWaarde="geef value eerst pls";

    public string _log = "geef value pls";
    public string log
    {
        get { return _log; }
        set { _log = value; }
    }

    //public GameObject cameraopbject;
    public Cameras camerascript;


    int aantalKeer;

    /*private void Start()
    {
        camerascript= cameraopbject.GetComponent<Cameras>();
    }*/

    void Update()
    { 
        if (transform.hasChanged)
        {
            if (aantalKeer >= 1)
            {
                // pas aan aangeraakt van camera script
                camerascript.aangeraakt = log;
            }
            transform.hasChanged = false;
            aantalKeer++;
        }
    }
}
