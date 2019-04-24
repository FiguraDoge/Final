using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HeliController : MonoBehaviour
{
    public GameObject goal;
    public float speed;
    public float finalDistance;
    public GameObject[] destroyOnSceneSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, goal.transform.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, goal.transform.position) < finalDistance)
        {
            endingScene();
        }
    }

    void endingScene()
    {
        foreach(GameObject o in destroyOnSceneSwitch)
        {
            Destroy(o, 1f);
        }
        SteamVR_LoadLevel.Begin("End");
    }


}
