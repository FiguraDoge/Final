using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndingController : MonoBehaviour
{
    public GameObject helicopter;
    public NavMeshAgent humanNM;

    public GameObject walkTo;
    public float upSpeed;
    public float flyHeight;
    public float forwardSpeed;
    public float flyTo;

    private int step; /* 0 - start 1 - walk 2 - fly 3 - fly away */
    private Vector3 heightDest;
    private Vector3 forwardDest;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;
        heightDest = helicopter.transform.position + new Vector3(0, flyHeight, 0);
        forwardDest = heightDest + new Vector3(-flyTo, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case 0:
                humanNM.baseOffset = -0.1f;
                humanNM.SetDestination(walkTo.transform.position);
                step = 1;
                break;
            case 1:
                if(Vector3.Distance(humanNM.gameObject.transform.position, walkTo.transform.position) < 0.3f)
                {
                    step = 2;
                    Destroy(humanNM.gameObject);
                }
                break;
            case 2:
                helicopter.transform.position = Vector3.Lerp(helicopter.transform.position, heightDest, upSpeed * Time.deltaTime);
                if (Vector3.Distance(helicopter.gameObject.transform.position, heightDest) < 0.1f)
                {
                    step = 3;
                }
                break;
            case 3:
                helicopter.transform.position = Vector3.MoveTowards(helicopter.transform.position, forwardDest, forwardSpeed * Time.deltaTime);
                if (Vector3.Distance(helicopter.gameObject.transform.position, forwardDest) < 1f)
                {
                    step = 4;
                }
                break;
        }
    }
}
