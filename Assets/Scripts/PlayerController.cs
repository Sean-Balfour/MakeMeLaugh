using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal, vertical;
    float movementSpeed = 2f;
    //float diagonal = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
     
    }
    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //if (horizontal != 0 && vertical != 0)
        //{
        //    movementSpeed *= diagonal;
        //}

        transform.Translate(new Vector3(horizontal, vertical, 0) * movementSpeed * Time.deltaTime);
    }
    //void interact()
    //{
    //    Input.GetKey("e");
    //}
}