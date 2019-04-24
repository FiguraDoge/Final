using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    public float followDistance;
    private NavMeshAgent navMesh;
    private PlayerManager pm;
    private GameObject playerObject;
    private Animator anim;
    public bool startFollowing;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        pm = PlayerManager.instance;
        playerObject = pm.gameObject;
        anim = GetComponent<Animator>();


        navMesh.baseOffset = -0.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        startFollowing = transform.position.y - playerObject.transform.position.y < 1f ;
        if (startFollowing)
        {
            navMesh.SetDestination(playerObject.transform.position);
            navMesh.isStopped = Vector3.Distance(transform.position, playerObject.transform.position) < followDistance;
            anim.SetBool("isWalking", !navMesh.isStopped);
        }
        else
        {
            navMesh.isStopped = true;
            anim.SetBool("isWalking", false);
        }
    }
}
