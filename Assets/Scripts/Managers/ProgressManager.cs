using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{

    /*
    Singleton Pattern
    */
    private static ProgressManager _instance;

    public static ProgressManager Instance{
        get{
            if(_instance == null){
                GameObject go = new GameObject("ProgressManager");
                go.AddComponent<ProgressManager>();
            }
            return _instance;
        }
    }

    void Awake(){
        _instance = this;
    }

    void Start(){
        ContentManager.Instance.LoadCharacterDataFromJson();
    }

    public void CallNumber(string number){
        switch (number)
        {
            case "12345678":
                /*
                    TODO Alguma coisa aqui vai fazer o dialogue manager começar o dialogo caso seja a opção certa
                */
                break;

            /*
                TODO colocar outros dialogos e números aqui
            */
            default:
                /*
                    TODO Dialogo padrão pra quando não existe o número (ou sortear em um dos dialogos possíveis)
                */
                Dialogue wrongNumberDialogue = new Dialogue();

                wrongNumberDialogue.name = "Me";
                wrongNumberDialogue.sentences = new string[] {
                    "I don't think that this is a valid number...",
                    "I should try another number."
                };

                DialogueTrigger wrongNumberDialogueTrigger = new DialogueTrigger();
                wrongNumberDialogueTrigger.dialogue = wrongNumberDialogue;
                wrongNumberDialogueTrigger.TriggerDialogue();

                break;
        }
    }
}
