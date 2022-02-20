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
    public Telephone telephone;

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
        /*Pegar telephone*/
        telephone = GameObject.Find("telephone").GetComponent<Telephone>();

        sentences = new Queue<string>();
        nameText = GameObject.Find("Dialogue UI/Dialogue Box/Dialogue Name").GetComponent<TMP_Text>();
        contentText = GameObject.Find("Dialogue UI/Dialogue Box/Dialogue Content").GetComponent<TMP_Text>();
        dialogueUIGameObject.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        telephone.CloseTelephone();
        GameManager.Instance.StopPlayerInSeconds(1);
        dialogueUIGameObject.SetActive(true);

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

    public void EndDialogue()
    {
        dialogueUIGameObject.SetActive(false);

        GameManager.Instance.ResumePlayer();
        nameText.text = " ";
        contentText.text = " ";
    }

}
