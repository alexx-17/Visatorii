using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovementStartScreen : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 spiderMovement;
    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        spiderMovement.x = 0;
        spiderMovement.y = -1;
    }

    IEnumerator stopSpider()
    {
        spiderMovement.y = 0;

        yield return new WaitForSeconds(5);

        spiderMovement.y = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.position.y <= 3.50f)
        {
            StartCoroutine(stopSpider());
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + spiderMovement * moveSpeed * Time.fixedDeltaTime);
    }
}
