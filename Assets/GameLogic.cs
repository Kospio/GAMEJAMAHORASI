using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Amplifier")]
    public bool gotAmplifier;
    public GameObject amplifier;

    [Space(10)]
    [Header("Amplifier")]
    public bool gotTickets;
    public GameObject tickets;

    [Space(10)]
    [Header("Amplifier")]
    public GameObject doorUntapp;

    [Space(10)]
    [Header("Amplifier")]
    public GameObject door;
    public bool finish;

    public DimensionHandler dimensionHandler;
    // Start is called before the first frame update
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
        Debug.Log("Final primer nivel");
    }

}
