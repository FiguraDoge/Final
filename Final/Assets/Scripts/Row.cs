using Ditzelgames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/*
 * Row is attacked to boat, and row only works when player is on boards 
 * Row requires waterfloat script, since the boat need to be on the water
 * waterfloat requires rigidbody to make boat float on the water
 */

[RequireComponent(typeof(WaterFloat))]
public class Row : MonoBehaviour
{
 
    public Transform seat;                  // The seat for player
    public Transform Motor;                 // The power source, where we apply force to
    public float MaxSpeed;                  // THe max speed that boat can reach
    public float rotateSpeed;               // The speed that boat rotates
    public float accelerateFactor;          // The acceleration
    public float decelerateFactor;          // The deceleration of boat
    public float rowSensitivity;            // The sensitivity of the player rowing action

    private Rigidbody boatRB;               // The rigidbody of boat
    private bool playerOnBoard;             // 1: player is on boat; 0: not on boat 
    private PlayerManager playerManager;    // A static script attached to player, used to track player

    private SteamVR_Behaviour_Pose trackedObjectLeft;       // left hand
    private SteamVR_Behaviour_Pose trackedObjectRight;      // right hand

    public void setPlayerOnBoard(bool val)
    {
        this.playerOnBoard = val;
    }

    public bool getPlayerOnBoard()
    {
        return this.playerOnBoard;
    }

    // Only return true when hand is pull to the oppisite direciton of boat forawrd 
    private bool CheckRow(SteamVR_Behaviour_Pose hand)
    {
        Vector3 handDir = hand.GetVelocity();                                  // Hand movement direction
        Vector3 boatDir = this.gameObject.transform.forward;                   // Boat forward

        // Using dot product to find whether angle between hand and boat is greater than 90 degrees
        // BE CAREFUL !!! THE COMPARISION OPERATOR DEPENDS ON THE BEGINING ROTATION OF THE BOAT !!!!
        // WHICH MEANS PLAYER FORWARD COULD BE THE OPPISITE OF BOAT FORWARD !!!
        // Here the boat is actually rotated 180 at the first place, so I use ">" here. 
        if (Vector3.Dot(handDir, boatDir) < 0 && handDir.magnitude > rowSensitivity)            
            return true;
  
        return false;
    }

    // Debug check
    private void debugPrint()
    {
        Debug.Log("left hand velocity = " + trackedObjectLeft.GetVelocity());
        Debug.Log("right hand velocity = " + trackedObjectRight.GetVelocity());
    }

    void Start()
    {
        playerOnBoard = false;
        playerManager = PlayerManager.instance;
        boatRB = this.gameObject.GetComponent<Rigidbody>();

        trackedObjectLeft = playerManager.leftHand.GetComponent<SteamVR_Behaviour_Pose>();
        trackedObjectRight = playerManager.rightHand.GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        // Player stay on the boat
        if (playerOnBoard)
            playerManager.gameObject.transform.position = seat.position;
    }

    void FixedUpdate()
    {
        // If the player is not on the board then return
        if (!playerOnBoard)
            return;

        Vector3 forceDirection = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        int rotateDirection = 0;

        // If player pull the left controller
        // The boat should go right and forward
        if (CheckRow(trackedObjectLeft))
        {
            rotateDirection += 1;
            PhysicsHelper.ApplyForceToReachVelocity(boatRB, forceDirection * MaxSpeed, accelerateFactor);
        }


        if (CheckRow(trackedObjectRight))
        {
            rotateDirection -= 1;
            PhysicsHelper.ApplyForceToReachVelocity(boatRB, forceDirection * MaxSpeed, accelerateFactor);
        }

        // Rotate the boat
        boatRB.AddForceAtPosition(rotateDirection * transform.right * rotateSpeed, Motor.position);

        // Slow down the speed
        boatRB.AddForceAtPosition(-1 * decelerateFactor * boatRB.velocity, Motor.position);
    }

}
