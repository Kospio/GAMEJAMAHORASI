using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicTrigger : MonoBehaviour
{
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;

    public void ForceActive()
    {
        TriggerEnter.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger In");
            if (TriggerEnter != null)
            {
                TriggerEnter.Invoke();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Out");
        if (other.gameObject.CompareTag("Player"))
            if (TriggerExit != null)
                TriggerExit.Invoke();
    }
}
