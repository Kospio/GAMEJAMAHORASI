using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CoordinateMovement : MonoBehaviour
{
    [Space(10)]
    [Header("--MOVEMENT--")]
    public Text MovementsText;
    [SerializeField] private int movements;
    [SerializeField] private int maxMovements = 20;
    [SerializeField] public float speed = 3f;

    private bool canMove = true; //Hace que acelere antes de colisionar
    private IEnumerator coroutineMovement;

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
    public LogicLevel1 gameLogic;
    public DimensionHandler dimensionHandler;

    [Space(10)]
    [Header("--Fox--")]
    public GameObject fox;
    public SoundController soundController;

    private void Start()
    {
        if (GameManager.Instance.currentDifficulty == GameManager.DifficultLevely.Insane)
        {
            MovementsText.text = "Movements: " + movements.ToString() + " / " + maxMovements;
        }
        else
        {
            MovementsText.text = "Movements: " + movements.ToString();
        }
    }
    void Update()
    {
        if (GameManager.Instance.currentDifficulty == GameManager.DifficultLevely.Insane && movements >= maxMovements)
        {
            Die();
            canMove = false;
            Debug.Log("YOU DIED");
        }

        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                fingerDown = Input.mousePosition;
                fingerDownTime = DateTime.Now;
            }
            if (Input.GetMouseButtonUp(0))
            {
                fingerUp = Input.mousePosition;
                fingerUpTime = DateTime.Now;
                CheckSwipe();
            }
        }
    }

    IEnumerator Movement(Vector3 rayDirection)
    {

        if (canMove)
        {
            canMove = false;

            if (lastTP != null) StartCoroutine(reEnableTP());

            RaycastHit Rhit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(rayDirection), out Rhit, 1000, dimensionHandler.currentDimension))
            {
                //Debug.Log(Rhit.distance);
                if (Rhit.collider.tag == "Bloque") //Hit
                {
                    if (Vector3.Distance(transform.position, (Rhit.transform.position - rayDirection)) > 0)
                    {
                        fox.gameObject.GetComponent<Animator>().SetTrigger("Walk");
                    }

                    while (Vector3.Distance(transform.position, (Rhit.transform.position - rayDirection)) > 0)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, (Rhit.transform.position - rayDirection), speed * Time.deltaTime);

                        yield return null;
                    }
                }
            }

            if (Rhit.distance > 0.51)
            {
                //Dimensions
                dimensionHandler.ChangeDimension();

                soundController.CambioDimension();

                fox.gameObject.GetComponent<Animator>().SetTrigger("Idle");

                //Movements
                movements++;
                if (GameManager.Instance.currentDifficulty == GameManager.DifficultLevely.Insane)
                {
                    MovementsText.text = "Movements: " + movements.ToString() + " / " + maxMovements;
                }
                else
                {
                    MovementsText.text = "Movements: " + movements.ToString();
                }
            }

            canMove = true;
        }
    }

    public void Teleport(GameObject otherTP)
    {
        movements++;

        soundController.Teleport();

        lastTP = otherTP;
        otherTP.GetComponent<Collider>().enabled = false;

        StopCoroutine(coroutineMovement);
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        dimensionHandler.ChangeDimension();
        transform.position = new Vector3(otherTP.transform.position.x, transform.position.y, otherTP.transform.position.z);
        fox.gameObject.GetComponent<Animator>().SetTrigger("Idle");

        canMove = true;
    }
    IEnumerator reEnableTP()
    {
        if (lastTP != null)
        {
            yield return new WaitForSeconds(2f);
            lastTP.GetComponent<BoxCollider>().enabled = true;
            lastTP = null;
        }
    }

    public void CheckSwipe()
    {
        float duration = (float)fingerUpTime.Subtract(fingerDownTime).TotalSeconds;

        if (duration <= swipeTime)
        {
            float deltaX = fingerUp.x - fingerDown.x;
            float deltaY = fingerUp.y - fingerDown.y;


            if (deltaX > 0 && deltaX > deltaY && Mathf.Abs(deltaX) > swipeDistance) //derecha
            {
                coroutineMovement = Movement(Vector3.right);
                StartCoroutine(coroutineMovement);

                Vector3 Rot = fox.transform.eulerAngles;
                Rot.y = 90;
                fox.transform.eulerAngles = Rot;
            }
            else if (deltaX < 0 && deltaX < deltaY && Mathf.Abs(deltaX) > swipeDistance) //izquierda
            {
                coroutineMovement = Movement(Vector3.left);
                StartCoroutine(coroutineMovement);

                Vector3 Rot = fox.transform.eulerAngles;
                Rot.y = -90;
                fox.transform.eulerAngles = Rot;
            }

            else if (deltaY > 0 && deltaX < deltaY && Mathf.Abs(deltaY) > swipeDistance) //arriba
            {
                coroutineMovement = Movement(Vector3.forward);
                StartCoroutine(coroutineMovement);

                Vector3 Rot = fox.transform.eulerAngles;
                Rot.y = 0;
                fox.transform.eulerAngles = Rot;
            }

            else if (deltaY < 0 && deltaY < deltaX && Mathf.Abs(deltaY) > swipeDistance) //abajo
            {
                coroutineMovement = Movement(-Vector3.forward);
                StartCoroutine(coroutineMovement);

                Vector3 Rot = fox.transform.eulerAngles;
                Rot.y = -180;
                fox.transform.eulerAngles = Rot;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Amplificador"))
        {
            soundController.Agarrar();

            Debug.Log("Cojo ampli");
            gameLogic.GetAmplifier();
        }

        else if (other.CompareTag("Puerta"))
        {
            soundController.Victoria();

            Debug.Log("Cojo puerta");
            gameLogic.Finish();
            speed = 0;
            fox.gameObject.GetComponent<Animator>().SetTrigger("Idle");

        }

        else if (other.CompareTag("DestapaPuerta"))
        {
            soundController.Agarrar();

            Debug.Log("destapo puerta");
            gameLogic.DoorUntapped();
        }

        else if (other.CompareTag("Tickets"))
        {
            soundController.Agarrar();

            Debug.Log("Cojo Ticket");
            gameLogic.GetTicket();
        }
    }

    public void Die()
    {
        speed = 0;
        Debug.Log("DIEEEEEEEEEEEE");
        fox.gameObject.GetComponent<Animator>().SetTrigger("Die");
    }
}
