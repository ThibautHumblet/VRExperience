using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandsEnabled : MonoBehaviour
{
    public GameObject HandModelPrefab;
    private GameObject spawnedHandModel;

    private Animator handsAnimator;

    private InputDevice inputDevice;
    public InputDeviceCharacteristics InputDeviceCharacteristics;

    // Start is called before the first frame update
    void Start()
    {
        InitializeControllers();

    }

    // Update is called once per frame
    void Update()
    {
        if (!inputDevice.isValid)
            InitializeControllers();
        else
        {
            spawnedHandModel.SetActive(true);
            UpdateAnimations();
        }

    }

    void InitializeControllers()
    {
        List<InputDevice> controllers = new List<InputDevice>();
        
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics, controllers);

        inputDevice = controllers[0];

        spawnedHandModel = Instantiate(HandModelPrefab, transform);
        handsAnimator = spawnedHandModel.GetComponent<Animator>();
    }

    void UpdateAnimations()
    {
        if (inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float valueTrigger))
        { 
            handsAnimator.SetFloat("Trigger", valueTrigger);
            
         }
        else
            handsAnimator.SetFloat("Trigger", 0);

        if (inputDevice.TryGetFeatureValue(CommonUsages.grip, out float valueGrip))
        {
            handsAnimator.SetFloat("Grip", valueGrip);
        }

        else
            handsAnimator.SetFloat("Grip", 0);
    }
}
