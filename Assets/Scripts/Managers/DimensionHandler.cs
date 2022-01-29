using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DimensionHandler : MonoBehaviour
{
    [SerializeField] internal LayerMask currentDimension;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;

    void Start()
    {
        currentDimension = LayerMask.GetMask("DIMENSION1");
    }

    public void ChangeDimension()
    {
        //if 1 => 2  //if 2 => 1
        if(currentDimension == LayerMask.GetMask("DIMENSION1"))
        {
            currentDimension = LayerMask.GetMask("DIMENSION2");
        }
        else if (currentDimension == LayerMask.GetMask("DIMENSION2"))
        {
            currentDimension = LayerMask.GetMask("DIMENSION1");
        }
        else
        {
            currentDimension = LayerMask.GetMask("DIMENSION1");
        }
            
        mainCamera.cullingMask = currentDimension;

        player.layer = (int)Mathf.Log(currentDimension.value, 2);
        Transform[] allPlayerChildren = player.GetComponentsInChildren<Transform>();
        foreach (Transform child in allPlayerChildren)
        {
            child.gameObject.layer = (int)Mathf.Log(currentDimension.value, 2);
        }
    }

    public void showOtherDimension()
    {
        //mainCamera.cullingMask = currentDimension;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
