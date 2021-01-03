using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class Cameras : MonoBehaviour
{

    public Camera[] cameras;
    private int currentCameraIndex;
    public GameObject[] selectorArr = new GameObject[3];
    movelog movelog = new movelog();


    // Use this for initialization
    void Start()
    {

        currentCameraIndex = 0;


        // Alle camera's uitzetten buiten de eerste
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
            // selectorArr[i].gameObject.VideoPlayer();

        }

        //dit heb ik toegevoegd om uiteindelijk ipv van hardcoded de arraygrootte mee te geven kan je dit via het empty object doen
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[0].GetComponent<Camera>().name + ", is now enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
       //kijken naar de logwaarde (static var in movelog)
       // afhankelijk van de keuze zullen er bepaalde camera's worden aan of uitgezet worden
        switch (movelog.logWaarde)
        {
            case "Friendsenter":
                updateCamera(1);
                break;
            case "LSD":
                updateCamera(2);
                break;
            case "ALKEUL":
                updateCamera(3);
                break;
            case "BADTRIP":
                updateCamera(4);
                break;
            default:
                updateCamera(0);
                break;
        }

        //hieronder gewoon een oud script waar voor van camera te wisselen
        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCameraIndex++;
            Debug.Log("C button has been pressed. Switching to the next camera");
            if (currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
            else
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                currentCameraIndex = 0;
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
        }
        */
    }

    // deze functie heeft als parameter de index van de camera waar we naartoe moeten gaan (aan en uit zetten dus)
    private void updateCamera(int cameranumb)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[cameranumb].gameObject.SetActive(true);
        Debug.Log("Camera with name: " + cameras[cameranumb].GetComponent<Camera>().name + ", is now enabled");

    }
}