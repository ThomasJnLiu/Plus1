using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_GroundDetect : MonoBehaviour
{
    public bool canJump;
    public Animator plAnimator;
    public sc_PlController player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Player")
        {
            player.canMove = true;
            canJump = true;
            plAnimator.SetBool("isJumping", false);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Player")
        {
            canJump = false;
            plAnimator.SetBool("isJumping", true);
        }

    }
}
