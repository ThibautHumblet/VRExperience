<h1 align="center">AP University College - VR Experience (group 6)</h1>

<p align="center">
  <img src="https://upload.wikimedia.org/wikipedia/commons/6/62/Logo_Artesis_Plantijn_Hogeschool_Antwerpen.jpg" width="300" alt="AP Logo"/>
  <br>
    <i>Welcome to the VR Experience repo of the students of AP University College for the course Immersive storytelling. We're the pupils from group six and our VR Experience will tackle the risks and effects of intoxication. </i>
</p>
<br>

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)  ![GitHub last commit](https://img.shields.io/github/last-commit/ThibautHumblet/VRExperience)  ![GitHub contributors](https://img.shields.io/github/contributors/ThibautHumblet/VRExperience)  ![GitHub repo size](https://img.shields.io/github/repo-size/ThibautHumblet/VRExperience)  ![GitHub](https://img.shields.io/github/license/ThibautHumblet/VRExperience)  ![GitHub Repo stars](https://img.shields.io/github/stars/ThibautHumblet/VRExperience?style=social)  ![GitHub watchers](https://img.shields.io/github/watchers/ThibautHumblet/VRExperience?style=social)

<hr>

## Table of contents
- Install
- Specific project-related documentation
  - Inverted sphere
  - Hands and interaction

## Install
Because we're building a VR application in Unity, a couple of things need to be configured before we can start developing.

### 1. Clone this GitHub Repo & open it
Clone this repo by executing the following PowerShell command in your desired folder.
````
git clone https://github.com/ThibautHumblet/VRExperience.git
````
Afterwards open the folder in Unity.

### 2. Import the necessary videos
Download the necessary videos from our [Google Drive](https://drive.google.com/drive/folders/1AhZGsppMoFCiUZs7fdNkun8_yRyc4pmh?usp=sharing). When you've downloaded the videos, put them in the *Video* folder in the Unity assets. Afterwards, check if the right videos are added to the *Video Clip* property in the *Video Player* component from the *Inverted Sphere* object 

### 3. Enable Preview Packages
In your Unity project, enable preview packages in Edit > Project Settings > Package Manager > Advanced settings

### 4. Install XR Interaction Toolkit
Install the XR interaction toolkit in your package manager. This can be done in Window > Package Manager > XR Interaction Toolkit > Install. Be sure you have enabled all packages from the Unity Registry. 

### 5. Start developing
Now you're ready to develop your own Unity VR Experience! Try playing around with all the different features and XR Components. If you're stuck, or if you need some inspiration, you can always check out this repo or visit the following useful guides;
- [VR With Andrew (Youtube)](https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ)
- [Getting started with VR in Unity](https://docs.unity3d.com/2019.3/Documentation/Manual/VROverview.html)
- [Valem (Youtube)](https://www.youtube.com/channel/UCPJlesN59MzHPPCp0Lg8sLw)
- [Oculus' first VR application guide](https://developer.oculus.com/documentation/unity/unity-tutorial/)

## Specific project-related documentation

### Inverted sphere
Normally, Unity renders its materials on the outside of an object. Because we want to make use of a 360° video, we need a solution.

To render our video's, we will be making use of an inverted sphere. This is a fancy word to explain that we render our video on the inside of set sphere. We generate our sphere using an InvertedSphere.cs script. For the base of this script, we used Mr. [Tom Peeters](https://github.com/tomptrs)' code. Because the code was not sufficient to our needs, we rewrote some parts of his code. 

We added a tag to check if the code was running in the Unity Editor or the built application. The initial code gave some nasty errors if we tried to build the application.
````cs
#if UNITY_EDITOR // <-- This simple line prevents the application from breaking if not in editor mode

public class InvertedSphere : EditorWindow 
{
  ....
}

#endif
````

The inital code was designed to put it in a specific place in a specific folder. This isn't very user friendly and is very keen to break if the developer is not careful. Our simple fix made the code more resilient.

### Hands and interaction
We want to let the user be immersed in our VR experience and let them use their hands. To achieve this, hands interaction was implemented in the application.
First, we downloaded the hands models from the [Oculus Developers](https://developer.oculus.com/downloads/package/oculus-hand-models/) page. After downloading them, we put the assets in our Unity project. 

Then, we added two empty objects in our XR Rigs Camera Offset. We renamed the objects both *LeftHand* and *RightHand* and gave them both an XR Controller and XR Direct interactor. Now we're ready to interact with objects, but we can't see our hands yet. We need to make some prefabs where we will store our hands. In the prefab, we put our *HandsEnabled* script and our models. When the code starts, we initialize our controllers. We also try to initialize them when the controllers are offline. This way, the user will be able to activate his controllers when the application is already running.
````cs
void Start()
{
    InitializeControllers();
}
    
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
````

The InitializeControllers() method will assign an input device and put a hand model on it. It will also assign an animator to set model. This way, we can let the hands perform various gestures, depending on which buttons are pressed. This will improve the immersion.
````cs
void InitializeControllers()
{
    List<InputDevice> controllers = new List<InputDevice>();
        
    InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics, controllers);

    inputDevice = controllers[0];

    spawnedHandModel = Instantiate(HandModelPrefab, transform);
    handsAnimator = spawnedHandModel.GetComponent<Animator>();
}
````

To update our animations, we have a UpdateAnimations() script. We use this script to listen to the different trigger and button values. Depending on the value, we parse this to our animator. In this animator, we added a blend tree to make a transition between our models.

When we increase our *grip* or our *trigger* values, this will have an impact on our animations. For example, if the trigger value is set to one, we want to have a pinching animation. 

<img src="/img/BlendTree.gif" alt="Animated blend tree example"/>

As you can see; once a value increases or decreases, the required animation state will change. With our UpdateAnimations() script, getting this values is very easy.

````cs
void UpdateAnimations()
{
    if (inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float valueTrigger))
        handsAnimator.SetFloat("Trigger", valueTrigger);
    else
        handsAnimator.SetFloat("Trigger", 0);

    if (inputDevice.TryGetFeatureValue(CommonUsages.grip, out float valueGrip))
        handsAnimator.SetFloat("Grip", valueGrip);
    else
        handsAnimator.SetFloat("Grip", 0);
}
````

When we have our hands and our interactions, grabbing objects is extremely easy. We only need to set some components to our desired object and we're good to go. More precisely we need to add a *Collider*, to make the object collidable, a *Rigidbody*, for giving the object some physics, and a *XR Grab Interactable*, to let our XR toolkit handle the grabbing. After adding these components, we have our object interaction.

### Sphere 'teleportation'
Because our project consists of multiple video clips, we need to find a way to change between them. It's not that easy to use the same inverted sphere and just switch the video. We found a workaround to just switch between different cameras that are inside the spheres. This will happen when we interact with a certain object.

```cs
public class Cameras : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex;
    public GameObject[] selectorArr = new GameObject[3];

    void start()
    {
        ...
    }

    void Update()
    {
        ...
    }
}
```

First, we make an assign our variables. We make an array of all our camera's where we can switch through. We also keep track of our current camera, using the currentCameraIndex variable. We wil use selectorArr to wait playing these videos, until we enter the designated sphere.

```cs
void Start()
{
    currentCameraIndex = 0;

    for (int i = 1; i < cameras.Length; i++)
    {
        cameras[i].gameObject.SetActive(false);
    }

    if (cameras.Length > 0)
    {
        cameras[0].gameObject.SetActive(true);
        Debug.Log("Camera with name: " + cameras[0].GetComponent<Camera>().name + ", is now enabled"); // this is for debugging purposes
    }
}
```
In our initialization script, we check how many cameras there are. Then we will turn all these camera's off, except the first one. Then we check if there are multiple cameras added to the controller, we will just enable the first one.

In our update script, we listen to an event. When this happens, we will change our currentCameraIndex variable. Then we just simply disable the current camera and enable the next camera. The user will have the immersion that he's now in a different scene. When we reach the end of the camera array, move back to the beginning or the array.
```cs
void Update()
{
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
}
```