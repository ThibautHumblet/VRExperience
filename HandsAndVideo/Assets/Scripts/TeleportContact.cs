using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportContact : MonoBehaviour
{
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;


    public void Teleport()
    {
        this.gameObject.transform.position = new Vector3(x, y, z);
    }
}
