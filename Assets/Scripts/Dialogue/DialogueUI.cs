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
}
