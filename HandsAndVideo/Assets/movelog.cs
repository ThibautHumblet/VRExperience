using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movelog : MonoBehaviour
{

    public string _log = "geef value pls";
    public GameObject manager;
    public string log
    {
        get { return _log; }
        set { _log = value; }
    }

    //public GameObject cameraopbject;
    public Cameras camerascript;


    int aantalKeer = 0;

    void Update()
    {
        // if (camerascript.timer > 3)
        // {
        if (transform.hasChanged)
        {
            if (aantalKeer >= 3)
            {
                Debug.Log(log);
                // pas aan aangeraakt van camera script
                camerascript.aangeraakt = log;

                if (camerascript.timerwandelen>=14 && camerascript.timerwandelenstart)
                    manager.SetActive(true);
                else
                    manager.SetActive(false);

                this.gameObject.SetActive(false);
            }
            transform.hasChanged = false;
            aantalKeer++;
        }
        //  }
    }

}
