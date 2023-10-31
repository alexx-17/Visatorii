using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCave : MonoBehaviour
{
    float MoveSpeed;

    public Rigidbody2D rb;
    public Animator animator;
    public Transform cameraTransform;
    public Transform playerTransform;

    Vector2 Movement;
    Vector3 cameraOffset; //haha offset e artist bun haha

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 3f;
        Movement.y = 0;

        cameraOffset.x = 0;
        cameraOffset.y = 0;
        cameraOffset.z = -10;
    }

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Horizontal", Movement.x);
        animator.SetFloat("Speed", Movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * MoveSpeed * Time.fixedDeltaTime);
        cameraTransform.position = playerTransform.position + cameraOffset;
    }
}
