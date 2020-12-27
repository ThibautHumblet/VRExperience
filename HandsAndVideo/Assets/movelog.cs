using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movelog : MonoBehaviour
{
    void Update()
    {
        if (transform.hasChanged)
        {
            Debug.Log("The transform has changed!");
            transform.hasChanged = false;
        }
    }
}
