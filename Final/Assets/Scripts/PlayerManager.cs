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

    private bool fadingBlack;
    private bool fadingIn;

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
            if (holder != null) holder.SetActive(true);
            if (pointer != null) pointer.SetActive(true);
            leftHand.GetComponent<SteamVR_LaserPointer>().enabled = true;

            if (holder2 != null) holder2.SetActive(true);
            if (pointer2 != null) pointer2.SetActive(true);
            rightHand.GetComponent<SteamVR_LaserPointer>().enabled = true;
        }
        else
        {
            leftHand.GetComponent<SteamVR_LaserPointer>().enabled = false;
            if (holder != null) holder.SetActive(false);
            if (pointer != null) pointer.SetActive(false);

            rightHand.GetComponent<SteamVR_LaserPointer>().enabled = false;
            if (holder2 != null)  holder2.SetActive(false);
            if (pointer2 != null) pointer2.SetActive(false);
        }
    }

    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        respawn = this.transform.position;
        setLaserPointer(false);

        fadingBlack = false;
        fadingIn = false;
    }

    // For falling
    // I made a respawn point prefab for you to test the falling and respawn
    void Update()
    {
        if (playerRB.velocity.y < -4) //check for if player is falling
        {
            fadingBlack = true; //set to true to call the fading object
            playerRB.velocity = new Vector3(0, 0, 0); //set the velocity to 0 so player stops falling
            setGravity(false); //turn gravity off so player stops falling
        }
        if (fadingBlack) //call fade() if necessary
        {
            fade();
        }
        if (fadingIn) //call fadingIn() if necessary
        {
            fadeIn();
        }
    }

    public void fade() //Fades the black screen in
    {
        if (blackScreen.transform.localScale.x < .5 || blackScreen.transform.localScale.z < .5)
        {
            blackScreen.transform.localScale += new Vector3(.005f, 0, .005f);
            if (blackScreen.transform.localScale.x > .5 || blackScreen.transform.localScale.z > .5)
            {
                fadingBlack = false;
                fadingIn = true;
                setGravity(true);
                transform.position = respawn;
            }
        }
        else
        {
            fadingBlack = false;
            fadingIn = true;
            setGravity(true);
            transform.position = respawn;
        }
    }

    public void fadeIn() //Fades the black screen out (the name of the function is a little misleading but you get the point :P )
    {
        if (blackScreen.transform.localScale.x > 0 || blackScreen.transform.localScale.z > 0)
        {
            blackScreen.transform.localScale -= new Vector3(.005f, 0, .005f);
            if (blackScreen.transform.localScale.x < 0 || blackScreen.transform.localScale.z < 0)
            {
                fadingIn = false;
                blackScreen.transform.localScale = new Vector3(0, 1, 0);
            }
        }
        else
        {
            fadingIn = false;
        }
    }

}
