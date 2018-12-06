using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger1 : MonoBehaviour
{

    public Dialogue dialogue;

    public void TriggerDialogue1()
    {
        FindObjectOfType<DialogueManager>().ContinueDialogue1(dialogue);
    }

}

