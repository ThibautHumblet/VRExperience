using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandsEnabled : MonoBehaviour
{
    public GameObject HandModelPrefab;
    private GameObject spawnedHandModel;

    // Start is called before the first frame update
    void Start()
    {
        spawnedHandModel = Instantiate(HandModelPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        spawnedHandModel.SetActive(true);
    }
}
