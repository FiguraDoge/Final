using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

/* 
 * This dock_water is for player to port the boat
 * For each water there should be one gameobject boat
 * If boat == null, then there is available port for boat
 * If boat != null, then there is no available port
 * When player pull the trigger inside the dock, dock should send player to the lnad, and send the boat to the port
 * unless there is no available port
 * Also, each dock_water has a corresponding dock_ground.
*/

public class Dock_Water : MonoBehaviour
{
    public SteamVR_Input_Sources leftHand;
    public SteamVR_Input_Sources rightHand;
    public SteamVR_Action_Boolean grabAction;


    public GameObject dock_ground;      // The coressponding dock_ground
    public GameObject portText;         // The attached text UI
    public Transform landPosition;      // This is going to be where player land
    public Transform dockPosition;      // This is going to be position after player park the boat

    private Text text;                  // The text in portText
    private Dock_Ground dockGround;     // The dock script of coressponding dock ground
    private PlayerManager playerManager;


    private bool CheckTrigger()
    {
        return grabAction.GetState(leftHand) || grabAction.GetState(rightHand);
    }

    void Start()
    {
        text = portText.GetComponent<Text>();
        playerManager = PlayerManager.instance;
        dockGround = dock_ground.GetComponent<Dock_Ground>();
    }

    // Inside the trigger area
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dockGround.getBoat() == null)
            {
                text.text = "Press the trigger to port the boat";
                
                if (CheckTrigger())
                {
                    // Process Player  
                    GameObject player = other.gameObject;
                    player.transform.position = landPosition.position;      // Move player to the ground 
                    playerManager.setGravity(true);                         // Set gravity to true since player is on ground now
                    playerManager.setTeleport(true);                        // Enable teleporting again
                    playerManager.setLaserPointer(false);

                    // Process Boat
                    GameObject boat = playerManager.getBoat();
                    playerManager.setBoat(null);                            // Tell player that it is no longer on boat
                    dockGround.setBoat(boat);                               // dock_ground now has available boat 
                      
                    boat.transform.position = dockPosition.position;        // Move boat to the port
                    boat.GetComponent<Row>().setPlayerOnBoard(false);       // Tell boat that player is no longer on the boat
                    boat.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    boat.transform.rotation = dockPosition.rotation;
                }
            }
            else
            {
                text.text = "There is no available port in this dock";
            }
        }
    }

    // If player is outside the trigger area 
    void OnTriggerExit()
    {
        text.text = "";
    }
}
