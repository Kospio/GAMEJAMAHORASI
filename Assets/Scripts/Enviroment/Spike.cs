using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public enum Direction { Side, UpDown}
    public Direction currentDirection;

    public float speed = 1f;

    [Header("SIDE MOVEMENT")]
    public float XrightLimit = 13f;
    public float XleftLimit = 11f;

    [Header("UPDOWN MOVEMENT")]
    public float ZrightLimit = 13f;
    public float ZleftLimit = 11f;

    void Update()
    {
        if(currentDirection== Direction.Side)
        {
            //basic movemenT
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;

            //changing direction
            if (pos.x < XrightLimit)
            {
                speed = Mathf.Abs(speed); //move right
            }
            else if (pos.x > XleftLimit)
            {
                speed = -Mathf.Abs(speed); //move left
            }
        }
        else if (currentDirection == Direction.UpDown)
        {
            //basic movemenT
            Vector3 pos = transform.position;
            pos.z += speed * Time.deltaTime;
            transform.position = pos;

            //changing direction
            if (pos.z < ZrightLimit)
            {
                speed = Mathf.Abs(speed); //move right
            }
            else if (pos.z > ZleftLimit)
            {
                speed = -Mathf.Abs(speed); //move left
            }
        }
    }
}
