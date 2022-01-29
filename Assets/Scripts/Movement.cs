using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public DimensionHandler dimensionHandler;
    public float speed = 3f;

    private bool canMove = true;

    private void Start()
    {
        //StartCoroutine(MovementCoroutine());
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.D) && canMove) //Swipe right
        {
            StartCoroutine(MovementCoroutine(speed, 0, Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.A) && canMove) //Swipe left
        {
            StartCoroutine(MovementCoroutine(-speed, 0, Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.W) && canMove) //Swipe up
        {
            StartCoroutine(MovementCoroutine(0, speed, Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMove) //Swipe down
        {
            StartCoroutine(MovementCoroutine(0, -speed, -Vector3.forward));
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
        dimensionHandler.ChangeDimension();

        otherTP.GetComponent<Collider>().enabled = false;
        transform.position = new Vector3(otherTP.transform.position.x, transform.position.y, otherTP.transform.position.z);
    }

}
