using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public GameObject body;
    public Rigidbody rb;

    public float movementSpeed = 3;
    private float jumpForce = 200f;
    public Transform cameraTransform;

    private Transform playerTransform;
    public Camera camera;
    public bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
        rb.freezeRotation = true;
        playerTransform = GetComponent<Transform>();
        print("Hello form player");
        
        
    }
    private void OnCollisionEnter(Collision other) {
        if (other.collider.tag.Equals("Ground"))
        {
            print("Grounded");
            isGrounded = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        print(mousePos);
        playerTransform.LookAt(mousePos,Vector3.up);
        // make camera follow player
        cameraTransform.position = playerTransform.position + new Vector3(0,0.5f,0);
        //cameraTransform.rotation = playerTransform.rotation;
        // input form keyboard
        if(Input.GetKey(KeyCode.A))
        {
            print("Left");
            rb.velocity = (movementSpeed * Vector3.left) + new Vector3(0,rb.velocity.y,0);
        }
        if(Input.GetKey(KeyCode.S))
        {
            print("Down");
            rb.velocity = (movementSpeed * Vector3.back) + new Vector3(0,rb.velocity.y,0);
        }
        if(Input.GetKey(KeyCode.D))
        {
            print("Right");
            rb.velocity = (movementSpeed * Vector3.right) + new Vector3(0,rb.velocity.y,0);
        }
        if(Input.GetKey(KeyCode.W))
        {
            print("Up");
            rb.velocity = (movementSpeed * Vector3.forward) + new Vector3(0,rb.velocity.y,0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // jump
            isGrounded = false;
            rb.AddForce(jumpForce*Vector3.up);
        }
    }

}
