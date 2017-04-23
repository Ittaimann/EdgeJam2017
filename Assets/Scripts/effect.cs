using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class effect : MonoBehaviour {

    public Material material;
    // Use this for initialization

 

    // Update is called once per frame
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
       
        Graphics.Blit(source, destination,material);
        return;
        

       
    }
}
