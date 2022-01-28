using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{

    private static DialogueManager _instance;
    public TMP_Text nameText;
    public TMP_Text contentText;
    public Queue<string> sentences;
    public GameObject dialogueUIGameObject;
    public DialogueUI dialogueUI;

    public static DialogueManager Instance{
        get{
            if(_instance == null){
                GameObject go = new GameObject("DialogueManager");
                go.AddComponent<DialogueManager>();
            }
            return _instance;
        }
    }

    void Awake(){
        _instance = this;
    }

    void Start(){
        nameText = GameObject.Find("Dialogue UI/Dialogue Box/Dialogue Name").GetComponent<TMP_Text>();
        contentText = GameObject.Find("Dialogue UI/Dialogue Box/Dialogue Content").GetComponent<TMP_Text>();
        dialogueUIGameObject.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        contentText.text = sentence;
    }

    private void EndDialogue()
    {
        nameText.text = " ";
        contentText.text = " ";
    }

    public void TestFunction(){
        Debug.Log("");
    }
}
