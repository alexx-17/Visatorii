using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;
    bool reachedObjective;

    public Rigidbody2D rb;
    public Animator animator;
    public Transform iadesTransform;
    public Transform playerTransform;
    public Transform objectivePosition;
    public Collider2D objectiveCollider;
    public TextMeshPro text;
    public SpriteRenderer sageata;
    public Collider2D ExitCollider;

    Vector2 Movement;
    Vector3 iadesMovement;
    Vector2 iadesRotation;

    void Start()
    {
        MoveSpeed = 3f;
        reachedObjective = false;

        iadesMovement.x = 0.8f;
        iadesMovement.y = 0.8f;
        iadesMovement.z = 0f;
        //poti sa modifici astea depinde unde vrei sa fie iades ul fata de player

        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Movement.x = 0;
        Movement.y = 0;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Movement.x = Input.GetAxisRaw("Horizontal");
        }
        else if(Input.GetAxisRaw("Vertical") != 0)
        {
            Movement.y = Input.GetAxisRaw("Vertical");
        }

        // Debug.Log(Movement.x);
        // Debug.Log(Movement.y);
        // Debug.Log(iadesTransform.position);
        // Debug.Log(playerTransform.position);

        animator.SetFloat("Horizontal", Movement.x);
        animator.SetFloat("Vertical", Movement.y);
        animator.SetFloat("Speed", Movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * MoveSpeed * Time.fixedDeltaTime);

        if(reachedObjective == false)
        {
            iadesTransform.position = playerTransform.position + iadesMovement; 
            //asta misca iadesul relativ fata de player

            iadesRotation.x = objectivePosition.position.x - iadesTransform.position.x;
            iadesRotation.y = objectivePosition.position.y - iadesTransform.position.y;
            iadesTransform.up = iadesRotation;
            //asta roteste iadesul ca sa se uite la obiectiv

            //si cacatul asta de spaghetti code chiar merge :)
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if(collider == objectiveCollider)
        {
            reachedObjective = true;
            iadesTransform.position = objectivePosition.position;
            //nu se mai misca sau roteste iadesul odata ce a ajuns la obiectiv
            //pozitia lui e aceeasi ca a obiectivului

            text.text = "Ai nevoie de lopata!";
            sageata.enabled = true;
        }

        if(collider == ExitCollider)
        {
            //a iesit
        }
    }

}
