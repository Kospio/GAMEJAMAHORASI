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
        mainCamera.cullingMask = currentDimension;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ChangeDimension()
    {
        //if 1 => 2  //if 2 => 1
        currentDimension = (currentDimension == LayerMask.GetMask("DIMENSION1")) ? currentDimension = LayerMask.GetMask("DIMENSION2") : currentDimension = LayerMask.GetMask("DIMENSION1");

        //camera
        mainCamera.cullingMask = currentDimension;

        //player
        player.layer = (int)Mathf.Log(currentDimension.value, 2);
        Transform[] allPlayerChildren = player.GetComponentsInChildren<Transform>();
        foreach (Transform child in allPlayerChildren)
        {
            child.gameObject.layer = (int)Mathf.Log(currentDimension.value, 2);
        }
    }

    public void ShowOtherDimension()
    {
        mainCamera.cullingMask = (mainCamera.cullingMask == LayerMask.GetMask("DIMENSION1")) ? mainCamera.cullingMask = LayerMask.GetMask("DIMENSION2") : mainCamera.cullingMask = LayerMask.GetMask("DIMENSION1");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
