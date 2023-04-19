using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialTexture : MonoBehaviour {

    public Material[] MaterialToChange;
    public Texture[] textures;
    private int CurrentTexture = 0;
    // Use this for initialization
    void Start () {

  


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeShaderButtonClicked()
    {
       

        if (CurrentTexture < textures.Length-1)
            CurrentTexture++;
        else
            CurrentTexture = 0;
        for (int i = 0; i < MaterialToChange.Length; i++)
        {
            MaterialToChange[i].mainTexture = textures[CurrentTexture];
        }
    }
}
