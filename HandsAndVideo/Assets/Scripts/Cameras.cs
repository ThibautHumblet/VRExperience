using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cameras : MonoBehaviour
{

    public Camera[] cameras;
    public GameObject[] cameraoffsets;
    //private LoadingOverlay[] overlays;

    public string aangeraakt = "start"; 
    bool abletochoosedrugs = true;
    bool ablatochoosetransport = false;
    bool ablekeuzelsd=false;
    bool timerstart = false;
    bool timeralcoholstart = false;
    bool timerautostart = false;
    bool timerlsdstrt = false;
    public bool timerwandelenstart = false;
    public bool abletorestart = false;

    //int huidigecamera = 0;


    float timer = 0;
    float timeralcohol = 0;
    float timerauto = 0;
    float timerlsd = 0;
    public float timerwandelen = 0;


    public GameObject optiesalcohol;
    public GameObject optieslsd;

    public VideoPlayer[] allevideos;

   public VideoPlayer autovideo;
   public VideoPlayer autovideolsd;

    public GameObject disabledopties;
    public GameObject enabledopties;

    public GameObject interactionmanager;

    public GameObject restarthospital;

    //public static Camera instance;


    // Use this for initialization
    void Start()
    {
        // Alle camera's uitzetten buiten de eerste
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
            // selectorArr[i].gameObject.VideoPlayer();

        }

        for (int i = 1; i < cameraoffsets.Length; i++)
        {
            cameraoffsets[i].gameObject.SetActive(false);
            // selectorArr[i].gameObject.VideoPlayer();

        }
       
        
        /*//alle overlays toewijzen
        for (int i=1; i<cameras.Length; i++)
        {
            overlays[i] = cameras[i].GetComponentInChildren<LoadingOverlay>();
        }

        //make sure all screens are black
        foreach (var overlay in overlays)
        {
            overlay.FadeIn();
        }

        overlays[0].FadeOut();

        */

        //dit heb ik toegevoegd om uiteindelijk ipv van hardcoded de arraygrootte mee te geven kan je dit via het empty object doen
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[0].GetComponent<Camera>().name + ", is now enabled");
        }
        //dit heb ik toegevoegd om uiteindelijk ipv van hardcoded de arraygrootte mee te geven kan je dit via het empty object doen
        if (cameraoffsets.Length > 0)
        {
            cameraoffsets[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameraoffsets[0].GetComponent<Camera>().name + ", is now enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerstart)
            timer += Time.deltaTime;

        if (timeralcoholstart)
            timeralcohol += Time.deltaTime;

        if (timerautostart)
            timerauto += Time.deltaTime;

        if (timerlsdstrt)
            timerlsd += Time.deltaTime;

        if (timerwandelenstart)
            timerwandelen += Time.deltaTime;

        if (timer > 15)
        {
            updateCamera(5);
            timerstart = false;
            timer = 0;
            timerwandelenstart = true;
        }
        if (timeralcohol > 15)
        {
            optiesalcohol.SetActive(true);
        }

        if (timerlsd > 15)
        {
            optieslsd.SetActive(true);
            timerlsd = 0;
            timerlsdstrt = false;
            ablekeuzelsd = true;
        }

        if (timerauto > 120)
        {
            
            updateCamera(9);
            timerauto = 0;
            timerautostart = false;
            restarthospital.SetActive(true);
        }

        if (timerwandelen > 15)
        {
            updateCamera(0);
            timerwandelen = 0;
            timerwandelenstart = false;
            enabledopties.SetActive(true);
            interactionmanager.SetActive(true);
        }

        if (aangeraakt != "start" && abletochoosedrugs)
        {
            //kijken naar de logwaarde (static var in movelog)
            // afhankelijk van de keuze zullen er bepaalde camera's worden aan of uitgezet worden
            switch (aangeraakt)
            {
                case "Friendsenter":
                    updateCamera(1);
                    break;
                case "LSD":
                    // 3 = bos lsd
                    timerlsdstrt = true;
                    updateCamera(10);
                    break;
                case "ALKEUL":
                    timeralcoholstart = true;
                    updateCamera(4);
                    break;
                case "XTC":
                    //5 = badtrip dinges, eerst feestje
                    timerstart = true;

                    updateCamera(8);
                    break;
                default:
                    updateCamera(0);
                    break;
            }
            disabledopties.SetActive(false);
            abletochoosedrugs = false;
            ablatochoosetransport = true;
        }

        //Debug.Log(aangeraakt);

        if (aangeraakt != "LSD" && aangeraakt != "ALKEUL" && aangeraakt != "BADTRIP" && ablatochoosetransport)
        {
            switch (aangeraakt)
            {
                case "auto":
                    updateCamera(6);
                    autovideolsd.Play();
                    timerautostart = true;
                    break;
                case "voet":
                    updateCamera(7);
                    timerwandelenstart = true;
                    break;
            }
            ablatochoosetransport = false;
            abletorestart = true;
        }

        if (aangeraakt != "LSD" && aangeraakt != "ALKEUL" && aangeraakt != "BADTRIP" && aangeraakt!="auto" && aangeraakt!="voet" && ablekeuzelsd)
        {
            switch (aangeraakt)
            {
                case "autolsd":
                    updateCamera(11);
                    autovideo.Play();
                    timerautostart = true;
                    break;
                case "boslsd":
                    updateCamera(3);
                    timerwandelenstart = true;
                    break;
            }
            ablekeuzelsd = false;
            abletorestart = true;
        }

        if (abletorestart && aangeraakt=="restart")
        {
            Restart();
            abletorestart = false;
        }


    }

    [System.Obsolete]
    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.LoadLevel(0);
    }
    // deze functie heeft als parameter de index van de camera waar we naartoe moeten gaan (aan en uit zetten dus)
    private void updateCamera(int cameranumb)
    {
        
        /*
        //fade from old camera
        overlays[huidigecamera].FadeIn();
        //check if fade is complete
        if (overlays[huidigecamera].fadecomplete==true) {

            //set complete to false
            overlays[huidigecamera].fadecomplete = false;
        */

            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(false);
            }
            cameras[cameranumb].gameObject.SetActive(true);
            
            Debug.Log("Camera with name: " + cameras[cameranumb].GetComponent<Camera>().name + ", is now enabled");


            for (int i = 0; i < cameraoffsets.Length; i++)
            {
                cameraoffsets[i].gameObject.SetActive(false);
            }
            cameraoffsets[cameranumb].gameObject.SetActive(true);
            
            //fade on new camera
           // overlays[cameranumb].FadeOut();
            
            
            allevideos[cameranumb].Play();
            //Debug.Log(allevideos[cameranumb]);
        
        
    }

}