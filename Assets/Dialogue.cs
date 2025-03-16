using UnityEngine;
/* ALL this code is from the youtube video first link in the word document about dialog boxes*/
 [System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3,20)]
    public string[] sentences; 
}
