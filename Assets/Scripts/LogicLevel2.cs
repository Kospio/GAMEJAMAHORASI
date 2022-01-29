using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicLevel2 : MonoBehaviour
{
    public GameObject checkpoint3;

    public GameObject objetoQueGira1;
    public GameObject objetoQueGira2;

    private int checkpoint2 = 0;
    public void CheckCheckpoint2()
    {
        checkpoint2++;
        if (checkpoint2 == 2)
        {
            checkpoint3.SetActive(true);
        }
    }

    public void CosasQueGiran()
    {

    }

    public void Checkpoint3Catched()
    {

    }
}
