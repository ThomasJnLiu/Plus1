using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_slimeController : MonoBehaviour
{
    public GameObject player;
    private bool facingRight = false;
    private Rigidbody2D slRigidbody;
    public float time;
    private SpriteRenderer myRenderer;
    int hp = 3;

    public GameObject bullet;
    public GameObject slimeDeathFX;
    // Use this for initialization
    void Start()
    {
        slRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(moveTimer(time));
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (facingRight)
        {
            slRigidbody.velocity = new Vector2(1, 0);
        }
        else
        {
            slRigidbody.velocity = new Vector2(-1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        }

        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Bullet2")
        {
            Destroy(collision.gameObject);
            if (hp == 1)
            {
                Instantiate(slimeDeathFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                hp--;
                StartCoroutine("Blink");
            }
        }
    }

    IEnumerator moveTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("turn");
        facingRight = !facingRight;
        Vector3 slScale = transform.localScale;
        slScale.x *= -1;
        transform.localScale = slScale;

        StartCoroutine(moveTimer(time));
    }

    IEnumerator Blink()
    {
        myRenderer.color = new Color(1f, 1f, 1f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        myRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
