using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_bulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D bulRigidBody;
    public Vector2 direction;

    public GameObject collisionFX;

    public int playerBullet;

    // Use this for initialization
    void Start()
    {
        bulRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        bulRigidBody.velocity = direction * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            RemoveBullet();
        }
        if (col.gameObject.tag == "Enemy")
        {
            RemoveBullet();
        }
    }
    public void RemoveBullet()
    {
        Instantiate(collisionFX, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
