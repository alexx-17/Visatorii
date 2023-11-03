using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementCave : MonoBehaviour
{
    float MoveSpeed, paianjenSpeed;
    bool running;

    public Rigidbody2D rb;
    public Animator animator;
    public Transform cameraTransform;
    public Transform playerTransform;
    public Collider2D scheletCollider;
    public TextMeshPro text;
    public Transform paianjenTransform;
    public Collider2D paianjenCollider;

    Vector2 Movement;
    Vector3 cameraOffset; //haha offset e artist bun haha
    Vector2 paianjenMovement;

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

        paianjenSpeed = 0.005f;
        paianjenMovement.x = 75;
        paianjenMovement.y = 0;
        paianjenTransform.position = paianjenMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (running == false)
        {
            Movement.x = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", Movement.x);
            animator.SetFloat("Speed", Movement.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * MoveSpeed * Time.fixedDeltaTime);
        if (running == true)
        {
            Movement.x = 0;

            paianjenSpeed += 0.08f * Time.fixedDeltaTime;
            paianjenMovement.x -= paianjenSpeed * Time.fixedDeltaTime;
            paianjenTransform.position = paianjenMovement;
        }
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

        if(playerTransform.position.x <= -7.7 && running == true)
        {
            //tranzitie catre carte

        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider == scheletCollider && running == false)
        {
            //fade out, fade in

            running = true;

            cameraOffset.x = 12f;
            cameraOffset.y = playerTransform.position.y;
            cameraOffset.z = 0;
            playerTransform.position = cameraOffset;
            cameraOffset.x = 7.8f;
            cameraOffset.y = 2.5f + playerTransform.position.y;
            cameraOffset.z = -10;
            cameraTransform.position = cameraOffset;
            cameraOffset.x = 0;
            cameraOffset.y = 2.5f;

            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Speed", 0);

            paianjenMovement.x = 23f;
            paianjenTransform.position = paianjenMovement;

            text.text = "Apasa click pe caracter pentru a alerga!";
        }

        if(collider == paianjenCollider)
        {
            //lose screen

            Debug.Log("MORT, AI MURIT!");
        }
    }

    private void OnMouseDown()
    {
        if(running == true)
        {
            Movement.x = -5;
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Speed", 1);
        }
    }
}
