using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject nextSentence;

    void Start(){
        DialogueManager.Instance.dialogueUIGameObject = this.gameObject;
        nextSentence = GameObject.Find("Dialogue UI/Dialogue Box/Next Sentence");
    }
    
    void Update(){

        // Next Sentence
        if(Input.GetMouseButtonDown(1)){
            DialogueManager.Instance.DisplayNextSentence();
        }

        // Skip Dialogue
        if(Input.GetMouseButtonDown(0)){
            DialogueManager.Instance.EndDialogue();
        }
    }
}
