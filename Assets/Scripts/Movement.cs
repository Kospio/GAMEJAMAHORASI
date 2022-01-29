using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    public DimensionHandler dimensionHandler;

    //EL PLAYER TIENE ESCALA 0.5

    [Header("--MOVEMENT--")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float raycastDistance = .5f;

    private bool canMove = true;
    IEnumerator coroutineMovement;

    [Header("--SWIPE--")]
    [SerializeField] private float swipeDistance = 50f;
    [SerializeField] private float swipeTime= 0.3f;
    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;

    [Header("--TP--")]
    private GameObject lastTP;

    private void Update()
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


    IEnumerator MovementCoroutine(float xDistance, float zDistance, Vector3 rayDirection)
    {
        if(lastTP != null) StartCoroutine(reEnableTP());


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

        if(duration <= swipeTime)
        {
            float deltaX = fingerUp.x - fingerDown.x;
            float deltaY = fingerUp.y - fingerDown.y;

            if (Mathf.Abs(deltaX) > swipeDistance)
            {
                if(deltaX > 0) //derecha
                {
                    coroutineMovement = MovementCoroutine(speed, 0, Vector3.right);
                    StartCoroutine(coroutineMovement);
                }
                else if(deltaX < 0) //izquierda
                {
                    coroutineMovement = MovementCoroutine(-speed, 0, Vector3.left);
                    StartCoroutine(coroutineMovement);
                }
            }

            if (Mathf.Abs(deltaY) > swipeDistance)
            {
                if (deltaY > 0) //arriba
                {
                    coroutineMovement = MovementCoroutine(0, speed, Vector3.forward);
                    StartCoroutine(coroutineMovement);
                }
                else if (deltaY < 0) //abajo
                {
                    coroutineMovement = MovementCoroutine(0, -speed, -Vector3.forward);
                    StartCoroutine(coroutineMovement);
                }
            }
        }
    }

}
