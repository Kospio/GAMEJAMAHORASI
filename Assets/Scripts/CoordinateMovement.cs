using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI; 

public class CoordinateMovement : MonoBehaviour
{
    [Space(10)]
    [Header("--MOVEMENT--")]
    [SerializeField] private float speed = 3f;
    private bool canMove = true; //Hace que acelere antes de colisionar
    private IEnumerator coroutineMovement;
    public int contadorMovimientos;
    public Text textoMovimientos; 

    [Space(10)]
    [Header("--SWIPE--")]
    [SerializeField] private float swipeDistance = 100f;
    [SerializeField] private float swipeTime = 0.8f;
    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;

    [Space(10)]
    [Header("--TP--")]
    private GameObject lastTP;

    [Space(10)]
    [Header("--GameLogic & Handler--")]
    public GameLogic gameLogic;
    public DimensionHandler dimensionHandler;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            fingerDown = Input.mousePosition;
            fingerDownTime = DateTime.Now;
        }
        if (Input.GetMouseButtonUp(0) && canMove)
        {
            fingerUp = Input.mousePosition;
            fingerUpTime = DateTime.Now;
            CheckSwipe();
        }
    }

    IEnumerator Movement(Vector3 rayDirection)
    {
        if (lastTP != null) StartCoroutine(reEnableTP());

        if (canMove)
        {
            canMove = false;

            RaycastHit Rhit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(rayDirection), out Rhit, 1000, dimensionHandler.currentDimension))
            {
                Debug.Log(Rhit.distance);
                if (Rhit.collider.tag == "Bloque") //Hit
                {

                    while (Vector3.Distance(transform.position, (Rhit.transform.position - rayDirection)) > 0)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, (Rhit.transform.position - rayDirection), speed * Time.deltaTime);

                        yield return null;
                    }
                }
            }

            if (Rhit.distance > 0.51)
            {
                dimensionHandler.ChangeDimension();
                contadorMovimientos++;
                textoMovimientos.text = contadorMovimientos.ToString();
            }

            canMove = true;
        }
    }

    public void Teleport(GameObject otherTP)
    {
        lastTP = otherTP;
        otherTP.GetComponent<Collider>().enabled = false;

        StopCoroutine(coroutineMovement);
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        dimensionHandler.ChangeDimension();
        transform.position = new Vector3(otherTP.transform.position.x, transform.position.y, otherTP.transform.position.z);

        canMove = true;
    }
    IEnumerator reEnableTP()
    {
        yield return new WaitForSeconds(2f);
        lastTP.GetComponent<BoxCollider>().enabled = true;
        lastTP = null;
    }

    public void CheckSwipe()
    {
        float duration = (float)fingerUpTime.Subtract(fingerDownTime).TotalSeconds;

        if (duration <= swipeTime)
        {
            float deltaX = fingerUp.x - fingerDown.x;
            float deltaY = fingerUp.y - fingerDown.y;

            if (Mathf.Abs(deltaX) > swipeDistance)
            {
                if (deltaX > 0) //derecha
                {
                    coroutineMovement = Movement(Vector3.right);
                    StartCoroutine(coroutineMovement);
                }
                else if (deltaX < 0) //izquierda
                {
                    coroutineMovement = Movement(Vector3.left);
                    StartCoroutine(coroutineMovement);
                }
            }

            if (Mathf.Abs(deltaY) > swipeDistance)
            {
                if (deltaY > 0) //arriba
                {
                    coroutineMovement = Movement(Vector3.forward);
                    StartCoroutine(coroutineMovement);
                }
                else if (deltaY < 0) //abajo
                {
                    coroutineMovement = Movement(-Vector3.forward);
                    StartCoroutine(coroutineMovement);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Amplificador"))
        {
            Debug.Log("Cojo ampli");
            gameLogic.GetAmplifier(); 
        }

        else if (other.CompareTag("Puerta"))
        {
            Debug.Log("Cojo puerta");
            gameLogic.Finish();
        }

        else if (other.CompareTag("DestapaPuerta"))
        {
            Debug.Log("destapo puerta");
            gameLogic.DoorUntapped(); 
        }

        else if (other.CompareTag("Tickets"))
        {
            Debug.Log("Cojo Ticket"); 
            gameLogic.GetTicket(); 
        }
    }
}
