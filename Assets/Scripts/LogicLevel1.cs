using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class LogicLevel1 : MonoBehaviour
{
    public DimensionHandler dimensionHandler;

    [Header("Amplifier")]
    public bool gotAmplifier;
    public GameObject amplifier;

    [Space(10)]
    [Header("Tickets")]
    private bool gotTickets;
    public GameObject tickets;

    [Space(10)]
    [Header("DootUntap")]
    public GameObject doorUntapp;

    [Space(10)]
    [Header("Puerta")]
    public GameObject door;
    private bool finish;

    [Space(10)]
    [Header("UI")]
    public RawImage silueta; 

    void Start()
    {
        dimensionHandler = dimensionHandler.GetComponent<DimensionHandler>();

        tickets.SetActive(false);
        amplifier.SetActive(false);
        door.SetActive(false);
    }

    public void DoorUntapped()
    {
        amplifier.SetActive(true);
        tickets.SetActive(true);
        doorUntapp.SetActive(false);
    }

    public void GetAmplifier()
    {
        gotAmplifier = true;
        amplifier.SetActive(false);

        if (gotTickets == true)
        {
            door.SetActive(true);
        }
    }

    public void GetTicket()
    {
        gotTickets = true;
        tickets.SetActive(false);

        if (gotAmplifier == true)
        {
            door.SetActive(true);
        }
    }

    public void Finish()
    {
        StartCoroutine(ChangeScene());
        Debug.Log("Final primer nivel");
    }

    public IEnumerator ChangeScene()
    {
        silueta.gameObject.GetComponent<Animator>().SetTrigger("NextScene"); 
        yield return new WaitForSeconds(1.7f);
        SceneManager.LoadScene("Level2");
    }

}
