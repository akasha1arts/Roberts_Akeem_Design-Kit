using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    [Header("Player Movement Stats")]
    [Tooltip("Use the following to adjust player movement speed")]  
    public float speed = 1f;
    [Tooltip("Use the following to adjust player jump height")]
    [SerializeField] float jump = 1f;
    float moveVelocity;

    //Grounded Vars
    bool grounded = true;

    [Header("Player Animation")]
    [Tooltip("Add Player game object here")]
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                GetComponent<Rigidbody>().velocity = new Vector2(GetComponent<Rigidbody>().velocity.x, jump);
                anim.SetBool("IsJumping", true);
            }
            else
            {
                anim.SetBool("IsJumping", false);
            }
        }

        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVelocity = -speed;
            anim.SetBool("IsRunningBack", true);

        }
        else
        {
            anim.SetBool("IsRunningBack", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVelocity = speed;
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }


        GetComponent<Rigidbody>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody>().velocity.y);

    }
    //Check if Grounded
    void OnTriggerEnter()
    {
        grounded = true;
    }
    void OnTriggerExit()
    {
        grounded = false;
    }
}
