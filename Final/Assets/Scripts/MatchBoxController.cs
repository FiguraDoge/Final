using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        MatchController mc = other.gameObject.GetComponent<MatchController>();
        if (mc != null)
        {
            mc.setFire();
        }
    }

    
}
