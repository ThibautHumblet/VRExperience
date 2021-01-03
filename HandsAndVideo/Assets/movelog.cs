using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movelog : MonoBehaviour
{

    public string logWaarde="geef value eerst pls";
    int aantalKeer;

    void Update()
    {
        if (transform.hasChanged)
        {
            if (aantalKeer >= 1)
            {
                Debug.Log(logWaarde);
            }
            transform.hasChanged = false;
            aantalKeer++;
        }
    }
}
