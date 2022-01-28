using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementBORRAR : MonoBehaviour
{
    public Rigidbody rb;
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3f;


        rb.AddForce(new Vector3(x,0,0), ForceMode.Force);
    }
}
