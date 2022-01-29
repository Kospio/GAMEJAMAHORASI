using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoordinateMovement : MonoBehaviour
{
    public DimensionHandler dimensionHandler;

    [Header("--MOVEMENT--")]
    [SerializeField] private float speed = 3f;
    private bool canMove = true; //Hace que acelere antes de colisionar
    private IEnumerator coroutineMovement;

    [Header("--SWIPE--")]
    [SerializeField] private float swipeDistance = 50f;
    [SerializeField] private float swipeTime = 0.3f;
    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;

    [Header("--TP--")]
    private GameObject lastTP;

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

    public void Teleport(GameObject otherTP)
    {
        lastTP = otherTP;

        StopCoroutine(coroutineMovement);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        canMove = true;

        dimensionHandler.ChangeDimension();

        otherTP.GetComponent<Collider>().enabled = false;
        transform.position = new Vector3(otherTP.transform.position.x, transform.position.y, otherTP.transform.position.z);
    }
    IEnumerator reEnableTP()
    {
        yield return new WaitForSeconds(1f);
        lastTP.GetComponent<BoxCollider>().enabled = true;
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
}
