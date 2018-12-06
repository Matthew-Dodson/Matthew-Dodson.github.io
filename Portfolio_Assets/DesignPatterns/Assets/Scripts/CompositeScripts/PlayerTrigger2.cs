using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger2 : MonoBehaviour
{

    public Dialogue dialogue;

    public void TriggerDialogue2()
    {
        FindObjectOfType<DialogueManager>().ContinueDialogue2(dialogue);
    }

}

