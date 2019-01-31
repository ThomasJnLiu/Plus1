using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_GroundDetect : MonoBehaviour
{
    public bool canJump;
    public Animator plAnimator;
    public sc_PlController player;
    public bool onPlayer;

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
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<sc_PlController>().health > 0)
            {
                onPlayer = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Player")
        {
            canJump = false;
            onPlayer = false;
            plAnimator.SetBool("isJumping", true);
        }

    }
}
