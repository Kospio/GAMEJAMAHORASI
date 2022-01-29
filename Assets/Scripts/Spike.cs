using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float speed = 1f;
    public float XrightLimit = 13f;
    public float XleftLimit = 11f;


    void Update()
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
}
