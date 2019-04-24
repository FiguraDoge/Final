using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDisableController : MonoBehaviour
{
    private PlayerManager playerManager;
    private GameObject playerObject;

    public ClimbingTriggerNoGrav climbTrigger;
    public GameObject baseLevel;
    //public HumanController human;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
        playerObject = playerManager.gameObject;
    }
    
    void OnTriggerEnter(Collider other)
    {
        triggered(other);
    }

    void OnTriggerStay(Collider other)
    {
        triggered(other);
    }

    private void triggered(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.Equals(playerObject))
        {
            climbTrigger.endFall();
            //Debug.Log(gameObject.name);
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, baseLevel.transform.position.y, playerObject.transform.position.z);
        }
        /*
        if(human != null)
        {
            human.startFollowing = true;
        }
        */
    }
    /*
    void OnTriggerExit(Collider other)
    {
        if (human != null)
        {
            human.startFollowing = false;
        }
    }
    */
}
