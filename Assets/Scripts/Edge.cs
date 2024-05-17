using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Edge : MonoBehaviour
{
    /*public Material hatredLikeMaterial;
    [ColorUsage(true, true)]
    public Color bloodColor = Color.red;
    [Range(0, 1)]
    public float saturationAmount = 0.5f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (hatredLikeMaterial != null)
        {
            hatredLikeMaterial.SetColor("_BloodColor", bloodColor);
            hatredLikeMaterial.SetFloat("_SaturationAmount", saturationAmount);
            Graphics.Blit(source, destination, hatredLikeMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }*/

    public Material hatredLikeMaterial;
    //[ColorUsage(true, true)]
    [Range(0, 1)]
    public float bloodColor = 0;
    
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (hatredLikeMaterial != null)
        {
            hatredLikeMaterial.SetFloat("_RedThreshold", bloodColor);
            Graphics.Blit(source, destination, hatredLikeMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

}
