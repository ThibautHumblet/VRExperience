using UnityEngine;
using System.Collections;

[AddComponentMenu ("EffectKasper")]
public class DrunkScript : MonoBehaviour {

public Shader SCShader;
private float TimeX = 1.0f;
 
private Material SCMaterial;

public float Distortion = 3f;

public float Speed = 1f;

Material material
{
get
{
if(SCMaterial == null)
{
SCMaterial = new Material(SCShader);
SCMaterial.hideFlags = HideFlags.HideAndDontSave;	
}
return SCMaterial;
}
}

void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
{
if(SCShader != null)
{
TimeX+=Time.deltaTime;
if (TimeX>100)  TimeX=0;
material.SetFloat("_TimeX", TimeX);
material.SetFloat("_Speed", Speed);
material.SetFloat("_Distortion", Distortion);
material.SetVector("_ScreenResolution",new Vector4(sourceTexture.width,sourceTexture.height,0.0f,0.0f));
Graphics.Blit(sourceTexture, destTexture, material);
}
else
{
Graphics.Blit(sourceTexture, destTexture);	
}


}


void OnDisable ()
{
if(SCMaterial)
{
DestroyImmediate(SCMaterial);	
}

}


}