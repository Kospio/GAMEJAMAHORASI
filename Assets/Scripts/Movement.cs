using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public DimensionHandler dimensionHandler;
    public float stepDistance = 1f;

    private bool canMove = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && canMove) //Swipe right
        {
            StartCoroutine(MovementCoroutine(1,0, Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.A) && canMove) //Swipe left
        {
            StartCoroutine(MovementCoroutine(-1,0, Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.W) && canMove) //Swipe up
        {
            StartCoroutine(MovementCoroutine(0,1, Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMove) //Swipe down
        {
            StartCoroutine(MovementCoroutine(0,-1, -Vector3.forward));
        }
    }

    IEnumerator MovementCoroutine(float xDistance, float zDistance, Vector3 rayDirection)
    {
        canMove = false;
        while (true)
        {
            //Raycast
            RaycastHit Rhit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(rayDirection), out Rhit, 1.4f, dimensionHandler.currentDimension)) // 2 METROS
            {
                if (Rhit.collider.tag == "Bloque") //Hit
                {
                    Debug.DrawRay(transform.position, rayDirection * 1.4f, Color.red);
                    Debug.Log("hit");

                    dimensionHandler.ChangeDimension();
                    canMove = true;

                    break;
                }
            }
            else //Not Hit
            {
                Debug.DrawRay(transform.position, rayDirection * 1.4f, Color.white);
            }

            transform.Translate(xDistance, 0, zDistance);
            yield return new WaitForSeconds(.2f);
        }
    }


    public void Teleport(GameObject otherTP)
    {
        dimensionHandler.ChangeDimension();

        otherTP.GetComponent<Collider>().enabled = false;
        transform.position = new Vector3(otherTP.transform.position.x, transform.position.y, otherTP.transform.position.z);
    }

}
