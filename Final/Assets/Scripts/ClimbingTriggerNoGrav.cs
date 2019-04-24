using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClimbingTriggerNoGrav : MonoBehaviour
{
    // Input source
    public SteamVR_Input_Sources leftHandSource;
    public SteamVR_Input_Sources rightHandSource;
    public SteamVR_Action_Boolean climbAction;

    // Moving direciton on the wall
    public bool xMove;                  
    public bool yMove;
    public bool zMove;

    public GameObject fallingTo;
    private bool fall;                       

    private bool leftHandTagged;            // Left hand attached 
    private bool rightHandTagged;           // Right hand attached

    private Vector3 prevLeftPos;            // Previous left hand position
    private Vector3 prevRightPos;           // Previous right hand position
    private Vector3 prevPlayerPos;          // Without this, player will slowly fall when both hands are tagged ????

    // Player stats
    private GameObject playerObject;
    private GameObject leftHandObject;
    private GameObject rightHandObject;
    private PlayerManager playerManager;

    private bool checkTrigger(SteamVR_Input_Sources hand)
    {
        return climbAction.GetState(hand);
    }

    public void endFall()
    {
        leftHandTagged = false;
        rightHandTagged = false;
        fall = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        leftHandTagged = false;
        rightHandTagged = false;
        fall = false;

        playerManager = PlayerManager.instance;
        playerObject = playerManager.gameObject;
        leftHandObject = playerManager.leftHand;
        rightHandObject = playerManager.rightHand;
    }

    // Update is called once per frame
    void Update()
    {
        if (fall)
        {
            playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, fallingTo.transform.position, 0.6f * Time.deltaTime);
            if(playerObject.transform.position.y - fallingTo.transform.position.y < 0.1f)
            {
                fall = false;
            }
        }

        /* Determine whether the trigger should be deactivated */
        
        if(leftHandTagged || rightHandTagged)
        {
            fall = false;
            //Debug.Log("tagging");
            // Deactivate tag when the player release the trigger
            if (!checkTrigger(leftHandSource))
                leftHandTagged = false;

            if (!checkTrigger(rightHandSource))
                rightHandTagged = false;

            // Enable gravity so player can fall down
            if (!rightHandTagged && !leftHandTagged)
                fall = true;
            // Depending on the current hands status, move the player
            // 1. Both hands are tagged
            if (leftHandTagged && rightHandTagged)
            {
                // To Do
                // When both hands are triggered
                // Do nothing for right now
                playerObject.transform.position = prevPlayerPos;    // otherwise, player will fall ????
            }
            // 2. Left hand is tagged
            else if (leftHandTagged)
            {
                // When only left hand is attached, move the player according to the y value change in left hand's position
                playerObject.transform.position -= new Vector3(xMove ? leftHandObject.transform.position.x - prevLeftPos.x : 0, yMove ? leftHandObject.transform.position.y - prevLeftPos.y : 0, zMove ? leftHandObject.transform.position.z - prevLeftPos.z : 0);
            }
            // 3. Right hand is tagged
            else if (rightHandTagged)
            {
                // When only right hand is attached, move the player according to the y value change in right hand's position
                playerObject.transform.position -= new Vector3(xMove ? rightHandObject.transform.position.x - prevRightPos.x : 0, yMove ? rightHandObject.transform.position.y - prevRightPos.y : 0, zMove ? rightHandObject.transform.position.z - prevRightPos.z : 0);
            }

        }
        prevPlayerPos = playerObject.transform.position;
        prevLeftPos = leftHandObject.transform.position;
        prevRightPos = rightHandObject.transform.position;

    }

    /* climbing action input is only activated when the hand is in the trigger, and will be deactivated anytime when the player release the trigger */
    void OnTriggerStay(Collider other)
    {
        /* ASSUMPTIONS: 
         * 1 - Both hands have a child with collider in a layer that can interact with the layer the trigger is in
         * 2 - the child component on left hand is tagged "LeftHand", and the child component on the right hand is tagged "Right Hand"
         */

        if (other.gameObject.tag.Equals("LeftHand") && checkTrigger(leftHandSource))
        {
            // Debug.Log("Triggered By LeftHand");
            leftHandTagged = true;
        }

        if (other.gameObject.tag.Equals("RightHand") && checkTrigger(rightHandSource))
        {
            // Debug.Log("Triggered By RightHand");
            rightHandTagged = true;
        }
    }


}
