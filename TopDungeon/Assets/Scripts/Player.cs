using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(BoxCollider2D))]
public class Player : Fighter
{
    
    private Vector2 rawInput;
    private RaycastHit2D hitX;
    private RaycastHit2D hitY;
    //private Vector2 playerShouldMove;

   
    private BoxCollider2D myBoxCollider2D;
    void Start()
    {
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rawInput.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            // might want to change the hard coded value 1f in this statement!!
            transform.localScale = new Vector2(Mathf.Sign(rawInput.x), 1f);
            
        }
    }

    private void Run()
    {
        hitX = Physics2D.BoxCast(transform.position,
            myBoxCollider2D.size,
            0,
            new Vector2(0, rawInput.y),
            Mathf.Abs(rawInput.y * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));
        if (hitX.collider == null)
        {
            transform.Translate(0, rawInput.y * Time.deltaTime, 0);
        }
        hitY = Physics2D.BoxCast(transform.position,
            myBoxCollider2D.size,
            0,
            new Vector2(rawInput.x, 0),
            Mathf.Abs(rawInput.x * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));
        if (hitY.collider == null)
        {
            transform.Translate(rawInput.x * Time.deltaTime, 0, 0);
        }
    }
    
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        //playerShouldMove = rawInput;
        //playerShouldMove.y = rawInput.y;
    }

    /*private void Run()
    {
        //vector2 in a vector3 method
        CollDetect();
        transform.Translate(playerShouldMove * Time.deltaTime);
        
    }

    private void CollDetect()
    {
        hitX = Physics2D.BoxCast(transform.position,
            myBoxCollider2D.size,
            0,
            new Vector2(0, rawInput.y),
            Mathf.Abs(rawInput.y * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));
        if (hitX.collider != null)
        {
            playerShouldMove.y = 0;
        }
        hitY = Physics2D.BoxCast(transform.position,
            myBoxCollider2D.size,
            0,
            new Vector2(rawInput.x, 0),
            Mathf.Abs(rawInput.x * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));
        if (hitY.collider != null)
        {
            playerShouldMove.x = 0;
        }
    }*/

    
}
