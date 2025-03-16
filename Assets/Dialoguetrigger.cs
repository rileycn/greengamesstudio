using UnityEngine;
/* ALL this code is from the youtube video first link in the word document about dialog boxes*/
public class Dialougetrigger : MonoBehaviour
{
   public Dialogue dialogue; 
   public void TriggerDialouge () {
    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
   }

   void Start() {
    TriggerDialouge ();
   }
}
