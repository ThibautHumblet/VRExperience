# AP University College - VR Experience

## Install
Because we're building a VR application in Unity, a couple of things need to be configured before we can start developing.

### 1. Clone this Github Repo & open it
Clone this repo by executing the following powershell command in your desired folder.
````
git clone https://github.com/ThibautHumblet/VRExperience.git
````
Afterwards open the folder in Unity.

### 2. Import the necessary videos
Download the necessary videos from our [Google Drive](https://drive.google.com/drive/folders/1AhZGsppMoFCiUZs7fdNkun8_yRyc4pmh?usp=sharing). When you've downloaded the videos, put them in the *Video* folder in the Unity assets. Afterwards, check if the right videos are added to the *Video Clip* property in the *Video Player* component from the *Inverted Sphere* object 

### 3. Enable Preview Packages
In your Unity project, enable preview packages in Edit > Project Settings > Package Manager > Advanced settings

### 4. Install XR Interaction Toolkit
Install the XR interaction toolkit in your package manager. This can be done in Window > Package Manager > XR Interaction Toolkit > Install . Be sure you have enabled all packages from the Unity Registery. 

Now your ready to develop your own Unity VR Experience! Try playing around with all the different features and XR Components. If you're stuck, or if you need some inspiration, you can always check out this repo or visit the following useful guides;
- [VR With Andrew (Youtube)](https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ)
- [Getting started with VR in Unity](https://docs.unity3d.com/2019.3/Documentation/Manual/VROverview.html)
- [Valem (Youtube)](https://www.youtube.com/channel/UCPJlesN59MzHPPCp0Lg8sLw)
- [Oculus' first VR application guide](https://developer.oculus.com/documentation/unity/unity-tutorial/)

## Specific project-related documentation

### Inverted sphere
Normally, Unity reders its materials on the outside of an object. Because we want to make use of a 360° video, we need a sollution.

To render our video's, we will be making use of an inverted sphere. This is a fancy word to explain that we render our video on the inside of set sphere. We generate our sphere using an InvertedSphere.cs script. For the base of this scipt, we used mr. [Tom Peeters](https://github.com/tomptrs)' code. Because the code was not sufficient to our needs, we rewrote some pars of his code. 

First of all, we added a tag to check if the code was running in the Unity Editor or the built application. The inital code gave some nasty errors if we tried to build the application.
````cs
#if UNITY_EDITOR // <-- This simple line prevents the application from breaking if not in editor mode

public class InvertedSphere : EditorWindow 
{
  ....
}

#endif
````

The inital code was designed to put it in a specific place in a specific folder. This isn't very user friendly and is very keen to break if the developer is not careful. Our simple fix made the code more resilient.
