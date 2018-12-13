using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_dialogueController : MonoBehaviour
{

    public sc_PlController player;
    public sc_PlController player2;
    public GameObject textManager;
    public int indexA = 0;
    public int indexB = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<sc_PlController>().DialogueStart(1);
            player2.GetComponent<sc_PlController>().DialogueStart(1);
            textManager.GetComponent<scr_TextManager>().ShowTextbox(indexA, indexB, 0.1f);
            gameObject.SetActive(false);
        }
    }
}
