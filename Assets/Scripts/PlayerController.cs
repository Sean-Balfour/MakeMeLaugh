using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal, vertical;
    float movementSpeed = 2f;
    //float diagonal = 0.1f;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private float radius;
    [SerializeField] private float force;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
     
    }
    private void FixedUpdate()
    {
        Movement();
        interact();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce (new Vector3(horizontal, vertical, 0)*force * movementSpeed * Time.fixedDeltaTime);

       
    }
    void interact()
    {

        if (Input.GetKey("e"))
        {
            search();
        }
    }



    void search()
    {
      Collider2D[] interactableList = Physics2D.OverlapCircleAll(transform.position, radius, interactableLayerMask);
        Debug.Log(interactableList.Length);

    }




}