using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonAndBack : MonoBehaviour {


    // Toggle between Diffuse and Transparent/Diffuse shaders
    // when space key is pressed

    Shader shader1;
    Shader shader2;
    Renderer[] rend;
    bool ToonActive = false;
    Color MatColor;

    // Use this for initialization
    void Start ()
    {

        rend = GetComponentsInChildren<Renderer>();
        shader1 = Shader.Find("Standard");
        shader2 = Shader.Find("Flat");

    }
	
	// Update is called once per frame
	void Update ()
    {
        //checking if ToonActivationNeeded
        if(ChangeShader.ToonTime && !ToonActive)
        {
            ChangeTime();
            ToonActive = true;
        }
        else if (!ChangeShader.ToonTime && ToonActive)
        {
            ChangeTime();
            ToonActive = false;
        }

    }
    public void ChangeTime()
    {
        foreach (Renderer Check in rend)
        {



            
            if (Check.material.shader == shader1)
            {
                MatColor = Check.material.color;
                Check.material.shader = shader2;
                Check.material.SetColor("_ShadowColor", MatColor);
            }
            else
            {
                Check.material.shader = shader1;
            }
        }
    }
}
