using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask currentDimension;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;

    void Start()
    {
        currentDimension = LayerMask.GetMask("DIMENSION1");
    }

    public void ChangeDimension()
    {
        //if 1 => 2
        //if(currentDimension = ? LayerMask.GetMask("DIMENSION2") : LayerMask.GetMask("DIMENSION2");

        mainCamera.cullingMask = currentDimension;
        player.layer = currentDimension;
    }
}
