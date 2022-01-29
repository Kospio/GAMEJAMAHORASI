using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private bool canMove; //Hace que acelere antes de colisionar
    private bool canSwipe; //Evita que se pueda hacer un swipe una vez en movimiento
    private bool canChangeDimension = true;

    public float waitTime = 0.1f; //Tiempo de control antes de que pueda volver a moverse tras colisionar

    [Range(0,10)]public float speed = 2; //Velocidad de aceleracion
    public float maxSpeed = 10; //Velocidad máxima de velocidad

    [Space]

    public Rigidbody rb;
    public DimensionHandler dimensionHandler; 

    void Start()
    {
        canMove = true;
        canSwipe = true; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && canSwipe) //Swipe right
        {
            StartCoroutine(Movement(speed, 0, maxSpeed));
        }

        else if (Input.GetKeyDown(KeyCode.A) && canSwipe) //Swipe left
        {
            StartCoroutine(Movement(-speed, 0, -maxSpeed));
        }

        else if (Input.GetKeyDown(KeyCode.W) && canSwipe) //Swipe up
        {
            StartCoroutine(Movement(0, speed, -maxSpeed));
        }

        else if (Input.GetKeyDown(KeyCode.S) && canSwipe) //Swipe down
        {
            StartCoroutine(Movement(0, -speed, -maxSpeed));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bloque") && canChangeDimension)
        {
            canChangeDimension = false;
            dimensionHandler.ChangeDimension();
            StartCoroutine(StopMovement());
        }
    }

    IEnumerator Movement(float xSpeed, float zSpeed, float speedLimit)
    {
        canSwipe = false;

        while (canMove)
        {
            rb.AddForce(new Vector3(xSpeed, 0, zSpeed), ForceMode.Force);
            if (rb.velocity.x <= speedLimit)
            {
                rb.velocity = new Vector3(speedLimit, 0, 0);
            }
            Debug.Log(rb.velocity);
            yield return null;
        }

        canChangeDimension = true;
    }

    IEnumerator StopMovement()
    {
        rb.velocity = Vector3.zero;
        canMove = false;
        yield return new WaitForSeconds(waitTime);
        canMove = true;
        canSwipe = true;
    }

    public void Teleport(GameObject otherTP)
    {
        canSwipe = false;

        dimensionHandler.ChangeDimension();

        otherTP.GetComponent<Collider>().enabled = false;
        transform.position = new Vector3(otherTP.transform.position.x, transform.position.y, otherTP.transform.position.z);

        StartCoroutine(StopMovement());
    }
}
