using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* ALL this code is from the youtube video first link in the word document about dialog boxes*/

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText; 
    public TextMeshProUGUI dialogueText;
    public Queue<string> sentences= new Queue<string>();

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
        dialogueText.text = sentence;
    }
    void EndDialogue(){
        Debug.Log("End of conversation. ");
    }
}

