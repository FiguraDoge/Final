using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject bird; //Bird Prefab
    public float frequency; //How often a bird will spawn in seconds
    public float startTime; //After how many seconds the first bird will spawn
    private float beginTime; //Time when script starts
    private float lastTime; //Time of last spawn

    void Start()
    {
        beginTime = Time.time;
        lastTime = Time.time;
    }
    
    void Update()
    {
        if (beginTime + startTime < Time.time)
        {
            if (lastTime + frequency < Time.time)
            {
                Instantiate(bird, transform.position, Quaternion.identity);
                lastTime = Time.time;
            }
        }
    }
}
