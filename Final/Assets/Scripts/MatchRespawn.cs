using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRespawn : MonoBehaviour
{
    private bool done;
    public GameObject matchPrefab;
    public GameObject pos;
    // Start is called before the first frame update
    void Start()
    {
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateRespawning()
    {
        if (!done)
        {
            Instantiate(matchPrefab, pos.transform.position, pos.transform.rotation);
            Destroy(gameObject, 3f);
            done = true;
        }
    }
}
