using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Vector2 rawInput;

    private Rigidbody2D myRigidbody2D;
    private BoxCollider2D myBoxCollider2D;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    
    void FixedUpdate()
    {
        FlipSprite();
        Run();
    }

    private void FlipSprite()
    {
        float temp = rawInput.x;
        if (temp > 0)
        {
            transform.localScale = new Vector3(rawInput.x, 1, 1);
        }
        else if(temp < 0)
        {
            transform.localScale = new Vector3(rawInput.x, 1, 1);
        }
        else
        {
            return;
        }
    }

    private void Run()
    {
        //Vector2 delta = rawInput * (moveSpeed * Time.deltaTime);
        myRigidbody2D.velocity = rawInput * moveSpeed;
        
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
}
