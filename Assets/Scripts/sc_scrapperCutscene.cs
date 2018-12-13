using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sc_scrapperCutscene : MonoBehaviour
{
    private SpriteRenderer scRenderer;
    public GameObject textManager;
    int hp = 3;
    public sc_PlController player;
    public sc_PlController player2;
    public GameObject audio;
    // Use this for initialization
    void Start()
    {
        scRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Fade()
    {
        StartCoroutine("FadeRoutine");
    }
    IEnumerator FadeRoutine()
    {
        for (float i = 1; i > 0f; i -= 0.2f)
        {
            scRenderer.color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void CutsceneDone(int index)
    {
        if (index == 0)
        {
            Fade();
        }
        else if (index == 1)
        {
            audio.SetActive(true);
        }
        else if (index == 2)
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Bullet2")
        {
            hp--;
            if (hp == 0)
            {
                player.GetComponent<sc_PlController>().DialogueStart(1);
                player2.GetComponent<sc_PlController>().DialogueStart(1);
                textManager.GetComponent<scr_TextManager>().ShowTextbox(2, 0, 0.1f);
            }

        }
    }
}
