using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Edge : MonoBehaviour
{

    public Material hatredLikeMaterial;
    //[ColorUsage(true, true)]
    [Range(0, 1)]
    public float bloodColor = 0;
    [Range(0, 1)]
    public float darken = 1;
    
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (hatredLikeMaterial != null)
        {
            hatredLikeMaterial.SetFloat("_RedThreshold", bloodColor);
            hatredLikeMaterial.SetFloat("_EdgeDarkenAmount", darken);
            Graphics.Blit(source, destination, hatredLikeMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

}
