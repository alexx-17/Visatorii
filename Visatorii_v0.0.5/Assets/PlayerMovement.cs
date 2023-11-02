using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{

    float MoveSpeed;
    bool reachedObjective;  //le-am mutat in Start()
    bool reachedExitPoint;
    bool digging;
    int cnt;

    public Rigidbody2D rb;
    public Animator animator;
    public Transform iadesTransform;
    public Transform playerTransform;
    public Transform objectivePosition;
    public Collider2D objectiveCollider;
    public TextMeshPro text;
    public SpriteRenderer sageata;
    public Collider2D ExitCollider;
    public Transition transitionManager; // asta e celalalt script
    public Camera mainCamera;
    public Transform lopataTransform;
    public SpriteRenderer lopataRender;
    public SpriteRenderer iadesRender;
    
    public SpriteRenderer dirtPileRender;
    public Sprite[] dirtSprites;

    //toate astea sunt pt bara
    public SpriteRenderer barRender;
    public Transform barTransform;
    public SpriteRenderer blockRender;
    public Transform blockTransform;
    public SpriteRenderer cursorRender;
    public Transform cursorTransform;

    Vector2 Movement;
    Vector3 iadesMovement;
    Vector3 lopataMovement;
    Vector2 iadesRotation;
    Vector3 cursorMovement;
    Vector3 blockPos;

    void Start()
    {
        MoveSpeed = 3f;
        reachedObjective = false;
        reachedExitPoint = false;
        digging = false;

        iadesMovement.x = 0.6f;
        iadesMovement.y = 0.6f;
        iadesMovement.z = 0f;
        //poti sa modifici astea depinde unde vrei sa fie iades ul fata de player

        lopataMovement.x = 0.6f;
        lopataMovement.y = 0f;
        lopataMovement.z = 0f;

        text.text = "";

    }

    IEnumerator makeTransition1()
    {
        //Debug.Log("a iesit");
        transitionManager.startTransition = true;
        //fade out

        yield return new WaitForSeconds(1);

        mainCamera.backgroundColor = new Color(0f, 0.45f, 0f, 1f);
        //nu ma intreba cum functioneaza culorile in unity
        sageata.enabled = false;
        text.text = "";

        yield return new WaitForSeconds(1);

        transitionManager.startTransition = true;

        lopataRender.enabled = true;
        //fade in
        //Debug.Log("a intrat");
    }

    IEnumerator makeTransition2()
    {
        yield return new WaitForSeconds(1);

        transitionManager.startTransition = true;

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Scene2");

        //daca te intrebi unde e fade in ul
        //e fct din inspector ca asa a fost cel mai usor
        //daca te uiti pe camera pe scena 2 o sa vezi ca sunt bifate si casuta cu startFadeOut si cu startTransition
        //mi era lene sa fac un script nou si d aia (sper ca nu te supi)
    }

    // Update is called once per frame
    void Update()
    {
        Movement.x = 0;
        Movement.y = 0;

        if (Input.GetAxisRaw("Horizontal") != 0 && digging == false)
        {
            Movement.x = Input.GetAxisRaw("Horizontal");
        }
        else if(Input.GetAxisRaw("Vertical") != 0 && digging == false)
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

        if (digging == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (cursorTransform.position.x >= blockTransform.position.x - 0.225 && cursorTransform.position.x <= blockTransform.position.x + 0.225)
                {
                    cnt++;
                    dirtPileRender.enabled = true;
                    dirtPileRender.sprite = dirtSprites[cnt];
                    if (cursorMovement.x > 0)
                    {
                        cursorMovement.x += 0.3f;
                    }
                    else
                    {
                        cursorMovement.x -= 0.3f;
                        Debug.Log(cursorMovement.x);
                    }

                    if (cnt == 5)
                    {
                        digging = false;

                        StartCoroutine(makeTransition2());
                    }
                }
                else
                {
                    cnt = 0;
                    if (cursorMovement.x > 0)
                        cursorMovement.x = 3f;
                    else
                        cursorMovement.x = -3f;
                    dirtPileRender.sprite = dirtSprites[cnt];
                }

                blockPos.x = Random.Range(3375, 5225);
                blockPos.x /= 1000;
                blockTransform.position = blockPos;
            }
        }
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

        if(reachedExitPoint == true)
        {
            lopataTransform.position = playerTransform.position + lopataMovement;
        }

        if(digging == true)
        {
            Debug.Log(cursorMovement.x);

            cursorTransform.position += cursorMovement * Time.fixedDeltaTime;
            if (cursorTransform.position.x >= 5.4542 || cursorTransform.position.x <= 3.1542)
                cursorMovement.x *= -1;

        }
        
    }



    private void OnTriggerEnter2D(Collider2D collider)
    {

        if(collider == objectiveCollider && reachedObjective == false)
        {
            reachedObjective = true;
            iadesTransform.position = objectivePosition.position;
            //nu se mai misca sau roteste iadesul odata ce a ajuns la obiectiv
            //pozitia lui e aceeasi ca a obiectivului

            text.text = "Ai nevoie de lopata!";
            sageata.enabled = true;
        }

        if(collider == ExitCollider && reachedObjective == true && reachedExitPoint == false)
        {
            reachedExitPoint = true;
            StartCoroutine(makeTransition1());
        }

        if(collider == objectiveCollider && reachedObjective == true && reachedExitPoint == true)
        {
            iadesRender.enabled = false;

            //aici ar trebui sa fie o animatie nebuna de sapat pe care nu pot sa o fac ca n-am grafica pt player


            barRender.enabled = true;
            cursorRender.enabled = true;
            blockRender.enabled = true;

            digging = true;
            cursorMovement.x = 3f;
            blockPos = blockTransform.position;
            cnt = 0;
        }
    }

}
