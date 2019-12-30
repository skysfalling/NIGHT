using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    private int selectedNum = 0;

    public GameObject emma;
    public GameObject ball;
    public GameObject MainCamera;

    public Rigidbody2D ballrb;
    public Rigidbody2D emmarb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedNum++;

            if(selectedNum >= 2)
            {
                selectedNum = 0;
            }

            Debug.Log("Selected Character: " + selectedNum);
        }

        if (selectedNum == 0) //activate Emma
        {
            emmarb.constraints = RigidbodyConstraints2D.None; //unfreeze all constraints
            emmarb.freezeRotation = true; //re-freeze rotation;
            emmarb.GetComponent<BasicMovement>().enabled = true;


            ballrb.constraints = RigidbodyConstraints2D.FreezePositionX; //freeze x so that when collider is deactivated, the character doesn't move
            ball.GetComponent<ballMovement>().enabled = false;
            ball.GetComponent<HingeMovement>().enabled = false;

            MainCamera.GetComponentInChildren<followPlayer>().target = emma.transform;

        }

        if (selectedNum == 1) //activate ball
        {
            emmarb.constraints = RigidbodyConstraints2D.FreezePositionX; //freeze x so that when collider is deactivated, the character doesn't move
            emma.GetComponent<BasicMovement>().enabled = false;


            ballrb.constraints = RigidbodyConstraints2D.None;
            ball.GetComponent<ballMovement>().enabled = true;
            ball.GetComponent<HingeMovement>().enabled = true;

            MainCamera.GetComponentInChildren<followPlayer>().target = ball.transform;


        }
    }
}
