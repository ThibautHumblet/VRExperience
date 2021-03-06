<h1 align="center">AP University College - VR Experience (group 6)</h1>

<p align="center">
  <img src="https://upload.wikimedia.org/wikipedia/commons/6/62/Logo_Artesis_Plantijn_Hogeschool_Antwerpen.jpg" width="300" alt="AP Logo"/>
  <br>
    <i>Welcome to the VR Experience repo of the students of AP University College for the course Immersive storytelling. We're the pupils from group six and our VR Experience will tackle the risks and effects of intoxication. </i>
</p>
<br>

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)  ![GitHub last commit](https://img.shields.io/github/last-commit/ThibautHumblet/VRExperience)  ![GitHub contributors](https://img.shields.io/github/contributors/ThibautHumblet/VRExperience)  ![GitHub repo size](https://img.shields.io/github/repo-size/ThibautHumblet/VRExperience)  ![GitHub](https://img.shields.io/github/license/ThibautHumblet/VRExperience)  ![GitHub Repo stars](https://img.shields.io/github/stars/ThibautHumblet/VRExperience?style=social)  ![GitHub watchers](https://img.shields.io/github/watchers/ThibautHumblet/VRExperience?style=social)

<hr>

- [Install](#install)
  - [1. Clone this GitHub Repo & open it](#1-clone-this-github-repo--open-it)
  - [2. Import the necessary videos](#2-import-the-necessary-videos)
  - [3. Enable Preview Packages](#3-enable-preview-packages)
  - [4. Install XR Interaction Toolkit](#4-install-xr-interaction-toolkit)
  - [5. Start developing](#5-start-developing)
- [Specific project-related documentation](#specific-project-related-documentation)
  - [Inverted sphere](#inverted-sphere)
  - [Hands and interaction](#hands-and-interaction)
  - [Sphere 'teleportation' on object interaction](#sphere-teleportation-on-object-interaction)
  - [Shaders](#shaders)
  - [Scene transition](#scene-transition)
- [Our team](#our-team)
- [Licence](#licence)

## Install
Because we're building a VR application in Unity, a couple of things need to be configured before we can start developing.

### 1. Clone this GitHub Repo & open it
Clone this repo by executing the following PowerShell command in your desired folder.
````
git clone https://github.com/ThibautHumblet/VRExperience.git
````
Afterwards open the folder in Unity.

**Important**
When opening the project in Unity, use version 2020.2.0f1!

### 2. Import the necessary videos
Download the necessary videos from our [Google Drive](https://drive.google.com/drive/folders/1AhZGsppMoFCiUZs7fdNkun8_yRyc4pmh?usp=sharing). When you've downloaded the videos, put them in the *Video* folder in the Unity assets. Afterwards, check if the right videos are added to the *Video Clip* property in the *Video Player* component from the *Inverted Sphere* object 

### 3. Enable Preview Packages
In your Unity project, enable preview packages in Edit > Project Settings > Package Manager > Advanced settings

### 4. Install XR Interaction Toolkit
Install the XR interaction toolkit in your package manager. This can be done in Window > Package Manager > XR Interaction Toolkit > Install. Be sure you have enabled all packages from the Unity Registry. 

### 5. Start developing
Now you are ready to develop your own Unity VR Experience! Try playing around with all the different features and XR Components. If you're stuck, or if you need some inspiration, you can always check out this repo or visit the following useful guides;
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

The initial code was designed to put it in a specific place in a specific folder. This is not very user friendly and is very keen to break if the developer is not careful. Our simple fix made the code more resilient.

### Hands and interaction
We want to let the user be immersed in our VR experience and let them use their hands. To achieve this, hands interaction was implemented in the application.
First, we downloaded the hands models from the [Oculus Developers](https://developer.oculus.com/downloads/package/oculus-hand-models/) page. After downloading them, we put the assets in our Unity project. 

Then, we added two empty objects in our XR Rigs Camera Offset. We renamed the objects both *LeftHand* and *RightHand* and gave them both an XR Controller and XR Direct interactor. Now we are ready to interact with objects, but we can't see our hands yet. We need to make some prefabs where we will store our hands. In the prefab, we put our *HandsEnabled* script and our models. When the code starts, we initialize our controllers. We also try to initialize them when the controllers are offline. This way, the user will be able to activate his controllers when the application is already running.
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

When we have our hands and our interactions, grabbing objects is extremely easy. We only need to set some components to our desired object and we are good to go. More precisely we need to add a *Collider*, to make the object collidable, a *Rigidbody*, for giving the object some physics, and a *XR Grab Interactable*, to let our XR toolkit handle the grabbing. After adding these components, we have our object interaction.

### Sphere 'teleportation' on object interaction
Because our project consists of multiple video clips, we need to find a way to change between them. It is not that easy to use the same inverted sphere and just switch the video. We found a workaround to just switch between different cameras that are inside the spheres. This will happen when we interact with a certain object.

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

First, we make an assign our variables. We make an array of all our camera's where we can switch through. We also keep track of our current camera, using the currentCameraIndex variable. We will use selectorArr to wait to play these videos, until we enter the designated sphere.

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

In our update script, we listen to an event. Depending on which object is touched, we will switch our camera. This will be checked using the static variable in *movelog*. The choice will decide which cameras will be turned on or off. If a drug is chosen, we will also apply our timer which will have an inpact on our shaders.

```cs
void Update()
{

    ...

    if (aangeraakt != "start" && abletochoosedrugs)
    {
        switch (aangeraakt)
        {
            case "Friendsenter":
                updateCamera(1);
                break;
            case "LSD":
                timerlsdstrt = true;
                updateCamera(10);
                break;
            case "ALCOHOL":
                timeralcoholstart = true;
                updateCamera(4);
                break;
            case "XTC":
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
}
```

After the user has chosen its drug, he will later have the ability to choose his mode of transport. If we would decide to add extra drugs, we can simply add them to our switch case.

### Shaders
The reason we have chosen for applying our shaders during the experience instead of using video editing beforehand, is very simple. We want to keep our scalability. Now we can use our shaders for every new video we add, instead of returning to our video editing software and manually adding an effect. This way, we will ensure that we can improve on this proof of concept once we film and add new videos.

Our shaders are code that run on the GPU instead of the CPU. They can change levels of colours in an image, produce special effects and can be used for video post-processing. They tell the computer how to draw something in a specific way. The languages we used to create these shaders are called Cg (C for Graphics) and HLSL (High-Level Shading Language).

At the start you want to give compilation directives which can be given through *#pragma statements*. There are many pragmas to use but in the Drunk and Colourshift shader we use the basics ones used in all shaders and target 3.0. The target allows certain instructions. In 3.0 we have the math instructions we need to distort the images with trigonometry.
```
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
```
After this we have the structures. Structures pass raw info about geometry being rendered to the vertex shader. Essentially objects in 3D are typically created using triangles. These triangles corners are what vertices are. You can manipulate these triangles to distort images or change the colours. For example, if you have 1 sprite of a character. You can use a shader to change the colours of this character and essentially have infinite different looking characters while only using 1 image.
For the drunk shader we will map the vertices in uv (coordinates) and create a wavy effect by using trigonometry. The time is to have the distorted effect happen over time instead of just having a still wavy image.
```cs
inline float2 getOffset(float time, float2 uv)
{ 
    float a = 1.0 + 0.5 * sin(time + uv.x * _Distortion);
    float b = 1.0 + 0.5 * cos(time + uv.y * _Distortion);
    return 0.02 * float2(a + sin(b), b + cos(a));
}
```
The "LSD" script: Colourshift. Only makes use of the colours of these vertices. It takes the colour red and blue of these triangles and shifts it to the side. This gives the objects on the screen a distorted view of seeing things double.

```cs
col.r = tex2D(_MainTex, p+offset.xy).r;
col.b = tex2D(_MainTex, p+offset.yx).b;
```

Since shaders are extremely difficult to work with, we've based ourselves on some pre-existing shaders from the internet. We used these as a guideline to quickly get the hang of it and to make our own shaders. Sources that were used to create and learn more about shaders as well as more extensive explanations are the following links:

- [Nvidia vertex shaders](https://www.nvidia.com/en-us/drivers/feature-vertexshader/#:~:text=What%20is%20a%20vertex%3F,on%20the%20objects'%20vertex%20data)
- [Unity's documentation about shaders ](https://wiki.unity3d.com/index.php/Shader_Code)
- [TeebarJunks ShaderGraphNode GitHub Readme](https://github.com/teebarjunk/Unity-ShaderGraphNodes)
- [GLSL Noise Algorithms](https://gist.github.com/patriciogonzalezvivo/670c22f3966e662d2f83)

We have learned a lot about shaders and how they can improve the immersive experience. Now we know how to quickly apply these to our videos and how to show these as a proof of concept. We are proud of the knowledge that we gained since this was a completely new subject.

### Scene transition
Because a hard cut between our videos would ruin the immersion, we decided to add a small fade between our camera transitions. This way, the user will not see himself teleport but gets the feeling the protagonist closes his eyes for a while. Once he opens them, he will find himself standing in a different scene. This is a great workaround and was done using some simple lines of code.

```cs
void Start()
{
    LoadingOverlay.ReverseNormals(this.gameObject);
    this.fading = false;
    this.fade_timer = 0;

    this.material = this.gameObject.GetComponent<Renderer>().material;
    this.from_color = this.material.color;
    this.to_color = this.material.color;
}
```

How we achieve this is by placing a cube over the camera which we will observe from the inside. Depending on the alpha value, we will have no or full transparency. At the start of the code, we will initialize our fade. We get our materials and our colors and reset the timer. We also make sure the fading effect is set to false so we won't start with a fading animation.

```cs
void Update()
{
    if(this.fading == false)
        return;

    this.fade_timer += Time.deltaTime;
    this.material.color = Color.Lerp(this.from_color, this.to_color, this.fade_timer);
    if(this.material.color == this.to_color){
        this.fading = false;
        this.fade_timer = 0;
        fadecomplete = true;
    }
}

public void FadeOut()
{
    this.from_color.a = this.in_alpha;
    this.to_color.a = this.out_alpha;
    if(this.to_color != this.material.color)
        this.fading = true;
}

public void FadeIn(){
    this.from_color.a = this.out_alpha;
    this.to_color.a = this.in_alpha;
    if(this.to_color != this.material.color)
        this.fading = true;
}
```

With the FadeIn() and FadeOut() methods we are able to adjust the alpha value of the color, which will increase of decrease the transparency of the objects. They will also set the fading Boolean to true or false.

```cs
public static void ReverseNormals(GameObject gameObject) 
{
    MeshFilter filter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
    if(filter != null)
    {
        Mesh mesh = filter.mesh;
        Vector3[] normals = mesh.normals;
        for(int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];

        mesh.normals = normals;

        for(int m = 0; m < mesh.subMeshCount; m++)
        {
            int[] triangles = mesh.GetTriangles(m);

            for(int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }

            mesh.SetTriangles(triangles, m);
        }
    }
}
```

Finally, we have our ReverseNormals() method. This will be used for rendering the inside of our cube instead of the outside. This is necessary because unity usually only renders the outside of objects. Because our camera is placed on the inside of the cube, we needed to use this as a workaround.

## Our team
Our code was written by these people. Due to the fact that we did not all have a pair of VR glasses; the development was not always that easy. But through good communication and teamwork, everyone did their fair share.

- [Thibaut Humblet](https://github.com/ThibautHumblet)
- [Stef Martens](https://github.com/stef2607)
- [Dries Bernaerts](https://github.com/DriesBe)
- [Kasper Ruys](https://github.com/KasperRuys)
- [Philip De Rudder](https://github.com/PhilipDeRudder)

We've learnt a lot during this project. Not only did we sharpen our Unity and C# skills, but we also got some new insight about how to work in a multidisciplinary team. We saw how to communicate with non-IT people and how to report back to them. 

Finally, we would like to thank our teacher, Mr. Tom Peeters, to share his insights and knowledge with us. With his support, we were able to make significant progress during the course of this project.

## Licence
This project is licensed under the terms of the **Apache** licence.
```md
   Copyright 2021 Thibaut Humblet, Stef Martens, Dries Bernaerts, Kasper Ruys, Philip De Rudder

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
```

>You can check the full licence [here](https://github.com/ThibautHumblet/VRExperience/blob/main/LICENSE).