using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/* ALL this code is from the youtube video first link in the word document about dialog boxes*/

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText; 
    public TextMeshProUGUI dialogueText;
    public Queue<string> sentences= new Queue<string>();
    public enum DialogueValue
    {
        Intro = 1,
        FarmIntro = 2,
        Year1End = 3,
        Year2Start = 4,
        Year2End = 5,
        Year3Start = 6,
        Year3End = 7,
    }
    public DialogueValue dialogueID;

    public GameObject destroyMe;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            //DisplayNextSentence();
        }
    }

    public void StartDialogue (Dialogue dialogue){
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence () {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (dialogueID == DialogueValue.FarmIntro)
        {
            if (sentence.Contains("Here is your quinoa seed"))
            {
                GameManager.main.seed += 25;
                GameManager.main.water += 25;
                GameManager.main.fert += 25;
                GameManager.main.UpdateMeterVisuals();
            }
        }
        dialogueText.text = sentence;
    }
    void EndDialogue(){
        Debug.Log("End of conversation. ");
        switch (dialogueID)
        {
            case DialogueValue.Intro:
                SceneManager.LoadScene("GameScene");
                break;
            case DialogueValue.FarmIntro:
                GameManager.main.StartGame();
                destroyMe.SetActive(false);
                break;
            case DialogueValue.Year1End:
                GameManager.main.AfterYear1EndDialogue();
                destroyMe.SetActive(false);
                break;
            case DialogueValue.Year2Start:
                GameManager.main.StartGame();
                destroyMe.SetActive(false);
                break;
            case DialogueValue.Year2End:
                GameManager.main.AfterYear1EndDialogue();
                destroyMe.SetActive(false);
                break;
            case DialogueValue.Year3Start:
                GameManager.main.StartGame();
                destroyMe.SetActive(false);
                break;
            case DialogueValue.Year3End:
                GameManager.main.AfterYear1EndDialogue();
                destroyMe.SetActive(false);
                break;
        }
    }
}

