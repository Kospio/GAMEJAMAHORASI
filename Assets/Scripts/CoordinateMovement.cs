using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateMovement : MonoBehaviour
{
    public bool canMove = true; //Hace que acelere antes de colisionar

    public float waitTime = 0.1f; //Tiempo de control antes de que pueda volver a moverse tras colisionar

    [SerializeField] private float speed = 3f;

    public DimensionHandler dimensionHandler;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) //Swipe right
        {
            StartCoroutine(Movement(Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.A)) //Swipe left
        {
            StartCoroutine(Movement(Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.W)) //Swipe up
        {
            StartCoroutine(Movement(Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.S)) //Swipe down
        {
            StartCoroutine(Movement(-Vector3.forward));
        }
    }

    IEnumerator Movement(Vector3 rayDirection)
    {
        if (canMove)
        {
            canMove = false;

            RaycastHit Rhit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(rayDirection), out Rhit, 1000, dimensionHandler.currentDimension))
            {
                if (Rhit.collider.tag == "Bloque") //Hit
                {
                    while (Vector3.Distance(transform.position, (Rhit.transform.position - rayDirection)) > 0)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, (Rhit.transform.position - rayDirection), speed * Time.deltaTime);

                        yield return null;
                    }
                }
            }

            dimensionHandler.ChangeDimension();
            canMove = true;

        }
    }
}
