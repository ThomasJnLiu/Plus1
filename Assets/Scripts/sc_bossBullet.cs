using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_bossBullet : MonoBehaviour
{
    public float moveSpeed = 7f;
    private Rigidbody2D myRbd;
    public GameObject player1;
    Vector2 moveDir;
    // Use this for initialization
    void Start()
    {
        player1 = GameObject.FindWithTag("Player");
        myRbd = GetComponent<Rigidbody2D>();
        moveDir = (player1.transform.position - transform.position).normalized * moveSpeed;
        myRbd.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                collision.GetComponent<sc_PlController>().TakenDamage(true, 10);
            }
            else
            {
                collision.GetComponent<sc_PlController>().TakenDamage(false, 10);
            }
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}