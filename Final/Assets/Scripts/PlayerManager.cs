using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject teleport;
    public GameObject blackScreen;      // For falling -> line 94

    private Vector3 respawn;            // For falling -> line 52
    private GameObject boat;
    private Rigidbody playerRB;

    public void setBoat(GameObject boat)
    {
        this.boat = boat;
    }

    public GameObject getBoat()
    {
        return this.boat;
    }
    
    public void setGravity(bool val)
    {
        playerRB.useGravity = val;
    }

    public void setTeleport(bool val)
    {
        teleport.SetActive(val);
    }

    public void setPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    // This is for Falling
    public void setRespawn(Vector3 position)
    {
        this.respawn = position;
    }

    public void setLaserPointer(bool val)
    {
        var holder = leftHand.GetComponent<SteamVR_LaserPointer>().holder;
        var pointer = leftHand.GetComponent<SteamVR_LaserPointer>().pointer;
        var holder2 = rightHand.GetComponent<SteamVR_LaserPointer>().holder;
        var pointer2 = rightHand.GetComponent<SteamVR_LaserPointer>().pointer;

        if (val)
        {
            holder.SetActive(true);
            pointer.SetActive(true);
            leftHand.GetComponent<SteamVR_LaserPointer>().enabled = true;

            holder2.SetActive(true);
            pointer2.SetActive(true);
            rightHand.GetComponent<SteamVR_LaserPointer>().enabled = true;
        }
        else
        {
            leftHand.GetComponent<SteamVR_LaserPointer>().enabled = false;
            holder.SetActive(false);
            pointer.SetActive(false);

            rightHand.GetComponent<SteamVR_LaserPointer>().enabled = false;
            holder2.SetActive(false);
            pointer2.SetActive(false);
        }
    }

    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        respawn = this.transform.position;
        setLaserPointer(false);
    }

    // For falling
    // I made a respawn point prefab for you to test the falling and respawn
    void Update()
    {

    }
}
