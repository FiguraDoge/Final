using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

/* 
 * This dock_ground is for player to enter the boat
 * For each dock_ground there should be one gameobject boat
 * If boat == null, then there is no available boat in the port
 * If boat != null, then there is one boat available
 * When player pull the trigger inside the dock, dock should send player to the boat in dock
 * unless there is no available boat
 * Also, each dock_ground has a corresponding dock_water.
*/

public class Dock_Ground : MonoBehaviour
{
    public SteamVR_Input_Sources leftHand;
    public SteamVR_Input_Sources rightHand;
    public SteamVR_Action_Boolean grabAction;

    public GameObject boat;             // The boat in port 
    public GameObject dock_water;       // The corresponding dock_water
    public GameObject boardText;        // The attached text UI 
    public GameObject buttonObj;        // The button object

    private Text text;                  // The text in boardText
    private PlayerManager playerManager;

    private bool CheckTrigger()
    {
        return grabAction.GetState(leftHand) || grabAction.GetState(rightHand);
    }

    public void setBoat(GameObject boat)
    {
        this.boat = boat;
    }

    public GameObject getBoat()
    {
        return this.boat;
    }

    public void movePlayer()
    {              
        playerManager.setBoat(boat);                                        // Set player boat 
        playerManager.setGravity(false);                                    // Prevent boat from flipping 
        playerManager.setTeleport(false);                                   // Disable teleporting while on boat
        playerManager.setPosition(boat.GetComponent<Row>().seat.position);  // Move player to the seat
        playerManager.setLaserPointer(false);                               // Turn off the layser pointer

        boat.GetComponent<Row>().setPlayerOnBoard(true);                    // Tell boat that player is on board
        this.boat = null;                                                   // Boat leaves this dock
    }

    void Start()
    {
        buttonObj.SetActive(false);
        text = boardText.GetComponent<Text>();
        playerManager = PlayerManager.instance;
    }

    void OnTriggerStay(Collider other)
    {
        // If player is inside the trigger area
        if (other.tag == "Player")
        {
            buttonObj.SetActive(true);
            playerManager.setLaserPointer(true);
            if (boat != null)
            {
                text.text = "Press Trigger to enter the boat";
                buttonObj.GetComponent<Button>().interactable = true;
            }
            else
            {
                text.text = "There is no available boat in this dock";
                buttonObj.GetComponent<Button>().interactable = false;
            }
        }
    }

    // If player is outside the trigger area 
    void OnTriggerExit()
    {
        text.text = "";
        buttonObj.SetActive(false);
    }
}
