using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_batTrigger : MonoBehaviour
{
    public sc_batControllerFollow bat;
    public Animator batAnimator;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (bat == null)
        {

        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                bat.chasingPlayer = true;
                bat.getTarget(other.gameObject);
                batAnimator.Play("batFollow4");
                Destroy(gameObject);
            }
        }

    }
}
