using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public DimensionHandler dimensionHandler;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float raycastDistance = 1.4f;

    private bool canMove = true;
    IEnumerator coroutineMovement;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.D) && canMove) //Swipe right
        {
            coroutineMovement = MovementCoroutine(speed, 0, Vector3.right);
            StartCoroutine(coroutineMovement);
        }
        else if (Input.GetKeyDown(KeyCode.A) && canMove) //Swipe left
        {
            coroutineMovement = MovementCoroutine(-speed, 0, Vector3.left);
            StartCoroutine(coroutineMovement);
        }
        else if (Input.GetKeyDown(KeyCode.W) && canMove) //Swipe up
        {
            coroutineMovement = MovementCoroutine(0, speed, Vector3.forward);
            StartCoroutine(coroutineMovement);
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMove) //Swipe down
        {
            coroutineMovement = MovementCoroutine(0, -speed, -Vector3.forward);
            StartCoroutine(coroutineMovement);
        }
    }


    IEnumerator MovementCoroutine(float xDistance, float zDistance, Vector3 rayDirection)
    {
        canMove = false;
        while (true)
        {
            //Raycast
            RaycastHit Rhit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(rayDirection), out Rhit, raycastDistance, dimensionHandler.currentDimension))
            {
                if (Rhit.collider.tag == "Bloque") //Hit
                {
                    Debug.Log("hit " + Rhit.transform.gameObject.name);

                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    dimensionHandler.ChangeDimension();
                    canMove = true;

                    break;
                }
            }

            GetComponent<Rigidbody>().velocity = new Vector3(xDistance, 0, zDistance);
            yield return null;
        }
    }

    public void Teleport(GameObject otherTP)
    {
        StopCoroutine(coroutineMovement);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        canMove = true;

        dimensionHandler.ChangeDimension();

        otherTP.GetComponent<Collider>().enabled = false;
        transform.position = new Vector3(otherTP.transform.position.x, transform.position.y, otherTP.transform.position.z);
    }

}
