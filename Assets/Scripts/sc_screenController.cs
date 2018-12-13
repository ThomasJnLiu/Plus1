using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sc_screenController : MonoBehaviour
{

    public Image black;
    // Use this for initialization
    void Start()
    {
        black.color = new Color(0f, 0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Fade()
    {
        StartCoroutine("StartFade");
    }
    IEnumerator StartFade()
    {
        Debug.Log("fading");
        for (float i = 0; i < 1f; i += 0.2f)
        {
            black.color = new Color(0f, 0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene("Ending2");
    }
}
