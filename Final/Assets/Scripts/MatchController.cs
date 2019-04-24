using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public GameObject fire;
    private bool used;
    // Start is called before the first frame update
    void Start()
    {
        fire.SetActive(false);
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFire()
    {
        if (!used)
        {
            fire.SetActive(true);
            used = true;
        }
    }

    public bool isOnFire()
    {
        return fire.activeSelf;
    }

    public void destroyFire()
    {
        fire.SetActive(false);
    }
    
}
