using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_batControllerFollow : MonoBehaviour
{
    private bool facingRight = false;
    private Rigidbody2D slRigidbody;
    public float time;
    private SpriteRenderer myRenderer;
    int hp = 3;

    public bool moveUp;
    public bool goingUp;
    public GameObject bullet;
    public GameObject slimeDeathFX;

    private Transform target;
    public float speed;
    float prevX;
    public bool chasingPlayer = false;





    // Use this for initialization
    void Start()
    {
        slRigidbody = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (chasingPlayer)
        {
            Vector3 batScale = transform.localScale;
            prevX = transform.position.x;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (prevX > transform.position.x)
            {
                Debug.Log("going left");

                batScale.x = 0.5f;
                transform.localScale = batScale;
            }
            else if (prevX < transform.position.x)
            {
                Debug.Log("going right");
                batScale.x = -0.5f;
                transform.localScale = batScale;
            }
        }

    }
    public void getTarget(GameObject target)
    {
        this.target = target.GetComponent<Transform>();
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
        if (moveUp)
        {
            yield return new WaitForSeconds(time);
            goingUp = !goingUp;
            StartCoroutine(moveTimer(time));
        }
        else
        {
            yield return new WaitForSeconds(time);
            Debug.Log("turn");
            facingRight = !facingRight;
            Vector3 slScale = transform.localScale;
            slScale.x *= -1;
            transform.localScale = slScale;

            StartCoroutine(moveTimer(time));
        }

    }

    IEnumerator Blink()
    {
        myRenderer.color = new Color(1f, 1f, 1f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        myRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
