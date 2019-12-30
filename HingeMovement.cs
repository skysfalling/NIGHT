using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeMovement : MonoBehaviour
{

    public bool isGrabbing;

    // Update is called once per frame
    void Update()
    {

        LineRenderer lr = gameObject.GetComponentInChildren<LineRenderer>(); //get line renderer from player
        //lr.startColor = Color.black;
        //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startWidth = 0.8f;
        lr.endWidth = 0.8f;


        if (Input.GetMouseButtonDown(0)) // called the 1st frame that the mouse is pressed
        {
            isGrabbing = true;
            //Debug.Log("isGrabbing = " + isGrabbing);
        }

        if (Input.GetMouseButton(0)) //all frames that the mouse is pressed
        {
            lr.positionCount = 2; // two positions (player & hinge object)
            GameObject closest = FindNearest();

            if (isGrabbing)
            {
                lr.SetPosition(1, closest.transform.position); //set one point of line to position of hinge

                //create a hinge joint to the nearest object
                closest.GetComponentInChildren<HingeJoint2D>().connectedBody =
                    gameObject.GetComponentInChildren <Rigidbody2D>();

                
            }

            lr.SetPosition(0, transform.position); //set one point of line to position of player
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            GameObject[] hinges;
            hinges = GameObject.FindGameObjectsWithTag("hinge");
            lr.positionCount = 0;


            foreach(GameObject go in hinges)
            {
                go.GetComponentInChildren<HingeJoint2D>().connectedBody = null; // detach anyy hinge joint
            }

            isGrabbing = false;
            //Debug.Log("is Grabbing = " + isGrabbing);
        }
    }

    GameObject FindNearest() // find nearest hinge object
    {
        GameObject[] hinges;
        hinges = GameObject.FindGameObjectsWithTag("hinge"); // hinges array
        GameObject closest = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach(GameObject go in hinges) // for each game object in hinges array
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if(curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
