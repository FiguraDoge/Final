using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private float killDistance = 20000;
    private Animator anim;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.Play("fly");
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * 1;
        if (Vector3.Distance(startPos, transform.position) > killDistance)
        {
            Destroy(gameObject);
        }
    }
}
