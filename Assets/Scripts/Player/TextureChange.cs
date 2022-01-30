using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChange : MonoBehaviour
{
    private DimensionHandler dimensionHandler;

    [Header("--MATERIALS--")]
    public GameObject eyebrowsMat;
    public GameObject dressMat;

    [Header("--TEXTURES--")]
    public Texture eyebrow_DIM1_Text; //CUTE
    public Texture eyebrow_DIM2_Text; //HEAVY
    [Space]
    public Texture dress_DIM1_Text;
    public Texture dress_DIM2_Text;


    void Start()
    {
        dimensionHandler = FindObjectOfType<DimensionHandler>();
        dimensionHandler.OnDimensionChanged += ChangeTextures;
    }

    void ChangeTextures()
    {
        if(dimensionHandler.currentDimension == LayerMask.GetMask("DIMENSION1"))
        {
            Debug.Log("DIMENSION CUTE");
            eyebrowsMat.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eyebrow_DIM1_Text;
            dressMat.GetComponent<SkinnedMeshRenderer>().material.mainTexture = dress_DIM1_Text;
        }
        else
        {
            Debug.Log("DIMENSION HEAVY");
            eyebrowsMat.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eyebrow_DIM2_Text;
            dressMat.GetComponent<SkinnedMeshRenderer>().material.mainTexture = dress_DIM2_Text;
        }
    }
}
