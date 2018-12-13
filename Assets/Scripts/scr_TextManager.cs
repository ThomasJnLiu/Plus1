using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class scr_TextManager : MonoBehaviour
{

    public TextAsset script;
    public Text textArea;
    public GameObject button;
    public GameObject player;
    public GameObject player2;
    public GameObject scrapper;

    public string[] tempArray;
    public string[,] scriptArray = new string[,] { };
    public string currentSentence;
    string sentenceHolder;

    int j = 0;
    int l = 0;
    int indexA = 0;
    int indexB = 0;

    //keeps track of when textbox is typing to see if player can skip through the text or not
    bool isTyping;

    public float delay;

    private AudioSource audio2;
    // Use this for initialization
    void Start()
    {
        audio2 = GetComponent<AudioSource>();
        button.SetActive(false);
        tempArray = script.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);

        /* I don't know how to make a 2D array of unknown length so I just set the max index to 
         * the number of lines in the script
        */
        scriptArray = new string[tempArray.Length, tempArray.Length];

        /* cycles through the array generated from text and splits up each line into dialogue sequences
         * each dialogue sequence ends with the line "BREAK" and a new one starts
         * j is the index of the dialogue sequence, l is the index of the sentences within the dialogue sequence
         */
        for (int i = 0; i < tempArray.Length; i++)
        {
            if (tempArray[i] == "BREAK")
            {
                j++;
                l = 0;
            }
            else
            {
                //Debug.Log(j + ", " + l);
                scriptArray[j, l] = tempArray[i];
                //Debug.Log(scriptArray[j, l]);
                l++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //test function to call and test textbox, delete later
        if (Input.GetKeyDown("l") || Input.GetKeyDown("b"))
        {
            if (button.activeInHierarchy)
                NextLine();
        }
    }

    //starts text sequence
    //called when text should be shown on screen
    //takes location/index of script, assigns it to textArea, opens textbox so player can advance script
    public void ShowTextbox(int indexC, int indexD, float delayInput)
    {
        button.SetActive(true);
        indexA = indexC;
        indexB = indexD;
        delay = delayInput;

        currentSentence = scriptArray[indexA, indexB];
        textArea.text = currentSentence;
        StartCoroutine(StartTyping(delay));
    }

    //advances to next line in script
    //called whenever textbox is clicked
    public void NextLine()
    {

        //check if the last line in script is being read, if so, close textbox
        /*
         * this is done by checking if the next line is null because idk how to intialize a 2D array
         * with a variable length so I just made the row and col have the same # of elements as the
         * initial string because it'll never result in an out of bounds exception.
         * Make this cleaner later.
        */
        if (isTyping)
        {
            isTyping = false;
            textArea.text = currentSentence;
        }
        else
        {
            if (scriptArray[indexA, indexB + 1] == null)
            {
                //remove the textbox, the current dialogue is finished
                Debug.Log("dialogue done");
                button.SetActive(false);
                player.GetComponent<sc_PlController>().PlayerUnlock();
                player2.GetComponent<sc_PlController>().PlayerUnlock();
                if (scrapper != null)
                {
                    scrapper.GetComponent<sc_scrapperCutscene>().CutsceneDone(indexA);
                }
            }
            else
            {
                //go to next line in script
                indexB++;

                //set currentSentence to next line in script
                currentSentence = scriptArray[indexA, indexB];

                StartCoroutine(StartTyping(delay));
            }
        }


    }

    /*makes each character in currentSentence visible
     * call whenever you assign a value to the textbox because it's supposed to be invisible
     */
    IEnumerator StartTyping(float delay)
    {
        isTyping = true;

        for (int i = 0; i <= currentSentence.Length; i++)
        {
            /* changes each character in the current sentence to be visible one at a time, while keeping the rest 
             * of the sentence invisible. This makes it so the entire sentence is typed out beforehand, preventing
             * line skipping while typing.
             */
            if (isTyping)
            {
                sentenceHolder = "<color=#FFFFFF>" + currentSentence.Substring(0, i) + "</color><color=#FFFFFF00>" + currentSentence.Substring(i) + "</color>";
                textArea.text = sentenceHolder;
                audio2.Play();
                yield return new WaitForSeconds(delay);
            }

        }

        isTyping = false;
    }


}

