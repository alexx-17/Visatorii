using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementCave : MonoBehaviour
{
    float MoveSpeed;
    bool running;

    public Rigidbody2D rb;
    public Animator animator;
    public Transform cameraTransform;
    public Transform playerTransform;
    public Collider2D scheletCollider;
    public TextMeshPro text;

    Vector2 Movement;
    Vector3 cameraOffset; //haha offset e artist bun haha

    // Start is called before the first frame update
    void Start()
    {
        running = false;

        MoveSpeed = 3f;
        Movement.y = 0;

        cameraOffset.x = 0;
        cameraOffset.y = 2.5f;
        cameraOffset.z = -10;

        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (running == false)
            Movement.x = Input.GetAxisRaw("Vertical");
        

        animator.SetFloat("Horizontal", Movement.x);
        animator.SetFloat("Speed", Movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * MoveSpeed * Time.fixedDeltaTime);
        if (running == true)
            Movement.x = 0;
        cameraTransform.position = playerTransform.position + cameraOffset;
        
        //te rog frumos sa nu comentezi de acest spaghetti code
        if(cameraTransform.position.x < 0)
        {
            cameraOffset.y += playerTransform.position.y;
            cameraTransform.position = cameraOffset;
            cameraOffset.y = 2.5f;
        }
        if(cameraTransform.position.x > 7.8)
        {
            cameraOffset.x = 7.8f;
            cameraOffset.y += playerTransform.position.y;
            cameraTransform.position = cameraOffset;
            cameraOffset.x = 0;
            cameraOffset.y = 2.5f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider == scheletCollider && running == false)
        {
            //fade out, fade in

            running = true;

            cameraOffset.x = 12.5f;
            cameraOffset.y = playerTransform.position.y;
            cameraOffset.z = 0;
            playerTransform.position = cameraOffset;
            cameraOffset.x = 7.8f;
            cameraOffset.y = 2.5f + playerTransform.position.y;
            cameraOffset.z = -10;
            cameraTransform.position = cameraOffset;
            cameraOffset.x = 0;
            cameraOffset.y = 2.5f;

            text.text = "Apasa click pe caracter pentru a alerga!";
        }
    }

    private void OnMouseDown()
    {
        if(running == true)
        {
            Movement.x = -5;
        }
    }
}
