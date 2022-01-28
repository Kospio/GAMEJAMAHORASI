using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool canMove; //Hace que acelere antes de colisionar
    public bool canSwipe; //Evita que se pueda hacer un swipe una vez en movimiento

    public float waitTime; //Tiempo de control antes de que pueda volver a moverse tras colisionar

    public float speed; //Velocidad de aceleracion
    public float maxSpeed; //Velocidad máxima de velocidad

    public Rigidbody rb;
    public DimensionHandler dimensionHandler; 

    void Start()
    {
        dimensionHandler.GetComponent<DimensionHandler>(); 
        canMove = true;
        canSwipe = true; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && canSwipe)
        {
            StartCoroutine(CharacterMovementRight());
        }

        else if (Input.GetKeyDown(KeyCode.A) && canSwipe)
        {
            StartCoroutine(CharacterMovementLeft()); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bloque"))
        {
            dimensionHandler.ChangeDimension(); 
            StartCoroutine(ColliderStopMove());
        }
    }

    IEnumerator CharacterMovementLeft()
    {
        canSwipe = false; 

        while (canMove)
        {
            rb.AddForce(new Vector3(-speed, 0, 0), ForceMode.Force);

            if (rb.velocity.x <= -maxSpeed)
            {
                rb.velocity = new Vector3(-maxSpeed,0,0); 
            }

            Debug.Log(rb.velocity);

            yield return null;
        }
    }

    IEnumerator CharacterMovementRight()
    {
        canSwipe = false;

        while (canMove)
        {
            rb.AddForce(new Vector3(speed, 0, 0), ForceMode.Force);

            if (rb.velocity.x >= maxSpeed)
            {
                rb.velocity = new Vector3(maxSpeed, 0, 0);
            }

            Debug.Log(rb.velocity);

            yield return null;
        }
    }

    IEnumerator ColliderStopMove()
    {
        rb.velocity = Vector3.zero;
        canMove = false;
        yield return new WaitForSeconds(waitTime);
        canMove = true;
        canSwipe = true;
    }
}
