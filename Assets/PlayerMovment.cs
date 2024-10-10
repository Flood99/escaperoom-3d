using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    public bool hasKey = false;
    bool grounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(hInput,0,vInput)* speed *Time.deltaTime);
        if(Input.GetButtonDown("Jump")&& grounded)
        {
            grounded = false;
            rb.velocity = new Vector3(rb.velocity.x,jumpForce,rb.velocity.z);
            
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        //if touching key destroy it and set it as collected 
        if(collision.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(collision.gameObject);
        }

        //if touching door after collecting key teleport player
        if(collision.gameObject.CompareTag("Door")&& hasKey)
        {
           transform.position = new Vector3(1000,1000,1000);
        }
        //reset jump when touching ground
        if(collision.gameObject.CompareTag("Ground"))
        {
           grounded = true;
        }
    }
}
