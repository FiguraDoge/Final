using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private PlayerManager playerManager;

    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    void OnTriggerStay(Collider other)
    {
        // If player is inside the trigger area
        if (other.tag == "Player")
        {
            playerManager.setRespawn(this.transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(this.transform.position, Vector3.one * 0.3f);
    }

}
