using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 Movement;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

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

        Debug.Log(Movement.x);
        Debug.Log(Movement.y);

        animator.SetFloat("Horizontal", Movement.x);
        animator.SetFloat("Vertical", Movement.y);
        animator.SetFloat("Speed", Movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * MoveSpeed * Time.fixedDeltaTime);
    }
}
