using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{
    public float ManualSpeed = 50f; //walk speed variable, set from Unity
    private float Speed; //just trust me, dont mess with this
    private bool idle;

    [SerializeField]
    public float jumpForce = 4000;
    public bool isGrounded = true;

    private bool pressedJump = false;
    private bool releasedJump = false;

    public float horizontalSpeed = 1000;

    [SerializeField]
    private float gravityScale = 10f;
    private bool startJumpTimer = false;
    [SerializeField]
    public float jumpTimerLength = 0.4f;
    private float jumpTimer;
    [SerializeField]
    public int jumpCount = 3;
    private int jumpCounter = 0;

    public Animator animator; //Animator instantiation
    public Rigidbody2D rb;

    public float grappleSideSpd = 1500;
    public float grapplePullSpd;

    // Update is called once per frame
    void Update()
    {
        // creating the idle boolean
        if (idle)
        {

            /* animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isCrouching", false);
            animator.SetBool("isIdle", true);
            */

            //If the character is idle, it shouldn't be moving.
            Speed = 0f;

        }

        if (GetComponent<HingeMovement>().isGrabbing == true)
        {
            // Debug.Log("isGrabbing");

            if (Input.GetKey(KeyCode.A))
            {
                //Debug.Log("A");
                rb.AddForce(new Vector3(-grappleSideSpd * Time.deltaTime, 0.0f, 0.0f));
            }

            if (Input.GetKey(KeyCode.D))
            {
                //Debug.Log("D");
                rb.AddForce(new Vector3(grappleSideSpd * Time.deltaTime, 0.0f, 0.0f));
            }

        }

        else
        {
            // Debug.Log("notGrabbing");

            // start jump timer
            if (startJumpTimer == true)
            {
                jumpTimer -= 2 * Time.deltaTime;
                //Debug.Log(jumpTimer);
                if (jumpTimer <= 0.0f)
                {

                    releasedJump = true;
                }
            }

            //jump key
            if (Input.GetKey(KeyCode.W))
            {
                /*
                animator.SetBool("isJumping", true);
                animator.SetBool("isIdle", false);
                */

                Speed = ManualSpeed;

            }


            // variable jump height using gravity change
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpCounter <= 0) // if emma is in the air and jumpCount = 0, endjump
                {
                    Debug.Log("not Grounded");
                    isGrounded = false;
                    EndJump();
                }

                else
                {
                    // Debug.Log("jump");
                    pressedJump = true;
                    jumpCounter -= 1; // subtract from jumpCounter
                    Debug.Log(jumpCounter);
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                releasedJump = true;
            }

            //move
            else if (Input.GetKey(KeyCode.A))
            {

                //animator.SetBool("isWalkingLeft", true);
                //animator.SetBool("isIdle", false);

                Speed = ManualSpeed;

                rb.AddForce(new Vector3(-horizontalSpeed * Time.deltaTime, 0.0f, 0.0f));

            }
            else if (Input.GetKey(KeyCode.D))
            {

                //animator.SetBool("isWalkingRight", true);
                //animator.SetBool("isIdle", false);

                Speed = ManualSpeed;

                rb.AddForce(new Vector3(horizontalSpeed * Time.deltaTime, 0.0f, 0.0f));

            }

            else if (Input.GetKey(KeyCode.S))
            {

                //animator.SetBool("isCrouching", true);
                //animator.SetBool("isIdle", false);

                Speed = ManualSpeed;

            }

            else
            {

                idle = true;
            }


            //Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            //transform.position += horizontal * Time.deltaTime * Speed; //move character horizontally

        }


        //animator.SetFloat("Horizontal", Input.GetAxis("Horizontal")); // horizontal animation
        //animator.SetFloat("Vertical", Input.GetAxis("Vertical")); // vertical animation


    } // end of Udpate()



    //check if grounded
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("grounded");
            isGrounded = true;
            jumpCounter = jumpCount; //reset jumpCounter to # of jumps
        }
    }

    //variable jump timer start and stop
    void StartJump()
    {
        if (isGrounded == true)
        {
            rb.gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);


            jumpTimer = jumpTimerLength;
            //Debug.Log("timer start");
            //Debug.Log(jumpTimer);
            startJumpTimer = true;
            pressedJump = false;

        }
    }

    void EndJump()
    {
        rb.gravityScale = gravityScale;
        releasedJump = false;

        startJumpTimer = false;
        //Debug.Log("timer end");

    }

    private void FixedUpdate()
    {
        if (pressedJump == true)
        {
            StartJump();
        }
        if (releasedJump == true)
        {
            EndJump();
        }
    }






}

