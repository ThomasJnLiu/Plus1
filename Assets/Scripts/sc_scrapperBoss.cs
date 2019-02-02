using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sc_scrapperBoss : MonoBehaviour
{
    public int phase = 1;

    public GameObject player;
    public GameObject player2;
    private bool facingRight = false;
    private Rigidbody2D slRigidbody;
    private Transform sltransform;

    public float time;
    private SpriteRenderer myRenderer;
    public int hp = 100;

    public Text healthText;

    public GameObject bullet;
    bool inPosition = false;
    bool isDead = false;
    private bool playerTarget = true;
    private int count = 0;
    public Image black;
    // Use this for initialization
    void Start()
    {
        slRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(moveTimer(time));
        myRenderer = GetComponent<SpriteRenderer>();
        sltransform = GetComponent<Transform>();
        StartCoroutine("Reload");
        black.color = new Color(1f, 1f, 1f, 0f);
    }
    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
    IEnumerator Reload()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.9f);
            if (playerTarget)
            {
                bullet.GetComponent<sc_bossBullet>().moveDir = (player.transform.position - transform.position).normalized * 7f;
            }
            else
            {
                bullet.GetComponent<sc_bossBullet>().moveDir = (player2.transform.position - transform.position).normalized * 7f;
            }
            Instantiate(bullet, transform.position, transform.rotation);

            //moveDir = (player1.transform.position - transform.position).normalized * moveSpeed;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("Reload");
        count++;

        if (count == 3)
        {
            playerTarget = !playerTarget;
            count = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = hp.ToString();
    }

    private void FixedUpdate()
    {
        if (facingRight && !isDead)
        {
            slRigidbody.velocity = new Vector2(3, 0);
        }
        else
        {
            slRigidbody.velocity = new Vector2(-3, 0);
        }
    }

    void Turn()
    {
        facingRight = !facingRight;
        Vector3 slScale = transform.localScale;
        slScale.x *= -1;
        transform.localScale = slScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                collision.GetComponent<sc_PlController>().TakenDamage(true, 20);
            }
            else
            {
                collision.GetComponent<sc_PlController>().TakenDamage(false, 20);
            }
        }

        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Bullet2")
        {
            hp -= 5;
            Debug.Log(hp);
            Destroy(collision.gameObject);
            if (hp < 1 && !isDead)
            {
                isDead = true;
                StartCoroutine("StartFade");
            }
            else
            {
                StartCoroutine("Blink");
            }
        }
    }

    IEnumerator Blink()
    {
        myRenderer.color = new Color(1f, 1f, 1f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        myRenderer.color = new Color(1f, 1f, 1f, 1f);
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

    IEnumerator StartFade()
    {
        Debug.Log("fading");
        for (float i = 0; i < 1f; i += 0.2f)
        {
            black.color = new Color(0f, 0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene("Ending");
    }
}
