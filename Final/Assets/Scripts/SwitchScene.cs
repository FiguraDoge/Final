using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SwitchScene : MonoBehaviour
{
    public GameObject[] destroyOnSceneSwitch;

    void endingScene()
    {
        foreach (GameObject o in destroyOnSceneSwitch)
        {
            Destroy(o, 1f);
        }
        SteamVR_LoadLevel.Begin("Sub");
    }

    void OnTriggerStay(Collider other)
    {
        // If player is inside the trigger area
        if (other.tag == "Player")
        {
            endingScene();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(this.transform.position, Vector3.one * 0.3f);
    }
}
