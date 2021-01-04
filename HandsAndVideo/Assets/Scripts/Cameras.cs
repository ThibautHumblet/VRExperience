using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class Cameras : MonoBehaviour
{

    public Camera[] cameras;
    public GameObject[] anderedinges;
    //private int currentCameraIndex;
    //public GameObject[] selectorArr = new GameObject[3];
    //private movelog movelog;

    public string aangeraakt="yeet";
    bool abletochoosedrugs = true;
    bool ablatochoosetransport = false;
    bool timerstart = false;

   public float timer = 0;

    //public static Camera instance;


    // Use this for initialization
    void Start()
    {
        

        //movelog = gameObject.AddComponent<movelog>();


        //currentCameraIndex = 0;


        // Alle camera's uitzetten buiten de eerste
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
            // selectorArr[i].gameObject.VideoPlayer();

        }

        for (int i = 1; i < anderedinges.Length; i++)
        {
            anderedinges[i].gameObject.SetActive(false);
            // selectorArr[i].gameObject.VideoPlayer();

        }

        //dit heb ik toegevoegd om uiteindelijk ipv van hardcoded de arraygrootte mee te geven kan je dit via het empty object doen
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[0].GetComponent<Camera>().name + ", is now enabled");
        }
        //dit heb ik toegevoegd om uiteindelijk ipv van hardcoded de arraygrootte mee te geven kan je dit via het empty object doen
        if (anderedinges.Length > 0)
        {
            anderedinges[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + anderedinges[0].GetComponent<Camera>().name + ", is now enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerstart)
        {
            timer += Time.deltaTime;
        }

        if (timer > 15)
        {
            updateCamera(5);
            timerstart = false;
            timer = 0;
        }

        if (aangeraakt != "yeet" && abletochoosedrugs)
        {
            //kijken naar de logwaarde (static var in movelog)
            // afhankelijk van de keuze zullen er bepaalde camera's worden aan of uitgezet worden
            switch (aangeraakt)
            {
                case "Friendsenter":
                    updateCamera(1);
                    break;
                case "LSD":
                    updateCamera(3);
                    break;
                case "ALKEUL":
                    updateCamera(4);
                    break;
                case "BADTRIP":
                    //5 = badtrip dinges, eerst feestje
                    timerstart = true;
                    updateCamera(8);
                    break;
                default:
                    updateCamera(0);
                    break;
            }
            abletochoosedrugs = false;
            ablatochoosetransport = true;
        }

        //Debug.Log(aangeraakt);

        if (aangeraakt!= "LSD" && aangeraakt!= "ALKEUL" && aangeraakt!= "BADTRIP" && ablatochoosetransport)
        {
            switch (aangeraakt)
            {
                case "auto":
                    updateCamera(6);
                    break;
                case "voet":
                    updateCamera(7);
                    break;
            }
            
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


        for (int i = 0; i < anderedinges.Length; i++)
        {
            anderedinges[i].gameObject.SetActive(false);
        }
        anderedinges[cameranumb].gameObject.SetActive(true);
    }
  
}