using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_button : MonoBehaviour
{

    public GameObject wall;
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
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Bullet2")
        {
            Destroy(wall);
        }
    }
}
