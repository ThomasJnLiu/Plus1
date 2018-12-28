using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sc_PlController : MonoBehaviour
{
    public int playerNum;

    private Animator plAnimator;

    private Rigidbody2D plRigidbody;
    //reads input of horizontal axis for calculating movement
    float horMovement;
    //reads input of vertical axis for looking up/down
    float verDirection;

    //variables related to movement
    public float moveSpeed;

    private bool facingRight = true;
    public bool canMove = true;
    public bool isDead = false;

    //variables related to jumping
    public GameObject groundPoint;

    private float jumpForce = 400f;
    public bool canJump2;
    public sc_GroundDetect groundDetect;

    //variables related to firing
    private bool canFire = true;
    public GameObject bullet;

    //variables related to knockback
    public float knockback;

    //sprite renderer
    private SpriteRenderer plRenderer;
    public int health = 50;

    //UI
    public Text healthText;

    public int index;
    public bool inDialogue = false;

    public string horAxis;
    public string verAxis;
    public string fireButton2;
    public string jumpButton2;
    public KeyCode fireButton;
    public KeyCode jumpButton;
    public KeyCode reviveButton;

    private Collider2D plCollider;

    public GameObject player2;
    public sc_screenController screen;
    bool lastScene = false;

    private AudioSource audio;
    public AudioClip shoot;
    public AudioClip jump;
    public AudioClip hurt;
    void Start()
    {
        plRigidbody = GetComponent<Rigidbody2D>();
        plAnimator = GetComponent<Animator>();
        plRenderer = GetComponent<SpriteRenderer>();
        plCollider = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();


    }

    private void Update()
    {
        healthText.text = health.ToString();
        if (canMove && !isDead && !inDialogue)
            SenseJump();
        if (inDialogue == true)
        {
            PlayerStop();
        }

        checkDead();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        horMovement = Input.GetAxis(horAxis);
        verDirection = Input.GetAxis(verAxis);

        if (canMove && !isDead && !inDialogue)
        {
            Movement();
            Flip(horMovement);
        }

        if (Input.GetButtonDown(fireButton2) && canFire)
        {
            HandleFire();
        }

        if (Input.GetKeyDown(KeyCode.R) && isDead)
        {

            health = 50;
            plAnimator.Play("Default");
            canMove = true;
            isDead = false;
        }

        if (isDead)
        {
            PlayerStop();
        }
    }
    public void checkDead()
    {
        if (isDead && player2.GetComponent<sc_PlController>().isDead)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    public void playerRevive()
    {
        health = 50;
        plAnimator.Play("Default");
        canMove = true;
        isDead = false;
        canFire = true;
        plCollider.isTrigger = false;
        plRigidbody.gravityScale = 1f;

    }
    private void Flip(float horMovement)
    {
        if (horMovement > 0 && !facingRight || horMovement < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 plScale = transform.localScale;
            plScale.x *= -1;

            transform.localScale = plScale;
        }
    }


    private void Movement()
    {
        plAnimator.SetFloat("verDirection", verDirection);
        plRigidbody.velocity = new Vector2(horMovement * moveSpeed, plRigidbody.velocity.y);
        plAnimator.SetFloat("speed", Mathf.Abs(horMovement));
    }

    private void SenseJump()
    {
        if (Input.GetButtonDown(jumpButton2) && groundDetect.canJump)
        {
            audio.clip = jump;
            audio.Play();
            plRigidbody.AddForce(new Vector2(plRigidbody.velocity.x, jumpForce));
        }
    }

    private void HandleFire()
    {
        if (canFire && !inDialogue)
        {
            audio.clip = shoot;
            audio.Play();
            if (facingRight && verDirection == 0)
            {
                bullet.GetComponent<sc_bulletController>().direction = Vector2.right;
            }
            else if (!facingRight && verDirection == 0)
            {
                bullet.GetComponent<sc_bulletController>().direction = Vector2.left;
            }
            else if (verDirection > 0.01)
            {
                bullet.GetComponent<sc_bulletController>().direction = Vector2.up;
            }
            else if (verDirection < -0.01)
            {
                bullet.GetComponent<sc_bulletController>().direction = Vector2.down;
            }

            Instantiate(bullet, transform.position, transform.rotation);
            canFire = false;
            StartCoroutine("Reload");
        }

    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.1f);
        canFire = true;
    }

    public void TakenDamage(bool knockRight, int damage)
    {
        if (canMove)
        {
            audio.clip = hurt;
            audio.Play();
            if (health > 0)
            {
                health -= damage;
                StartCoroutine("Blink");
            }
            if (health <= 0)
            {
                health = 0;
                PlayerStop();
                canMove = false;
                isDead = true;
                canFire = false;
                plCollider.isTrigger = true;
                plRigidbody.gravityScale = 0f;
                plAnimator.Play("playerScrap");

                Scene currentScene = SceneManager.GetActiveScene();

                // Retrieve the name of this scene.
                string sceneName = currentScene.name;
                if (sceneName == "Final")
                {
                    Debug.Log("final");
                    screen.Fade();
                }

            }
        }
        if (!isDead)
        {

            canMove = false;
            if (knockRight)
            {
                Debug.Log("knockLeft");
                plRigidbody.velocity = new Vector2(-knockback, knockback);
            }
            else
            {
                Debug.Log("knockRight");
                plRigidbody.velocity = new Vector2(knockback, knockback);
            }
        }
    }

    IEnumerator Blink()
    {
        plRenderer.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(0.5f);
        plRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    //stops player's velocity
    public void PlayerStop()
    {
        plRigidbody.velocity = new Vector2(0, 0);
    }

    public void DialogueStart(int index)
    {
        PlayerLock();

    }

    //stops player from moving
    public void PlayerLock()
    {
        PlayerStop();
        canMove = false;
        inDialogue = true;
        canFire = false;
        plAnimator.SetFloat("verDirection", 0);
        plAnimator.SetFloat("speed", 0);
        plAnimator.Play("Default");
    }

    public void PlayerUnlock()
    {
        inDialogue = false;
        canFire = true;
        canMove = true;
    }

    //function for friendly fire
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerNum == 1 && collision.gameObject.tag == "Bullet2")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                TakenDamage(false, 5);
            }
            else
            {
                TakenDamage(true, 5);
            }
        }
        if (playerNum == 2 && collision.gameObject.tag == "Bullet")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                TakenDamage(false, 5);
            }
            else
            {
                TakenDamage(true, 5);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.GetComponent<sc_PlController>().isDead == true)
        {

            if (Input.GetKeyDown(reviveButton))
            {
                collision.GetComponent<sc_PlController>().playerRevive();
            }
        }
    }
}
