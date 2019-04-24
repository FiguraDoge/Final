using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public GameObject fire;
    public GameObject heli;
    private bool fired;
    // Start is called before the first frame update
    void Start()
    {
        fired = false;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        MatchController mc = other.gameObject.GetComponent<MatchController>();
        if(mc != null)
        {
            if (mc.isOnFire() && !fired)
            {
                fire.SetActive(true);
                fired = true;
                heli.SetActive(true);
            }
        }
    }
    
}
