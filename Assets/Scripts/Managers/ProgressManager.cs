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
                Debug.Log("FUNCIONA");
                break;
            default:
                Debug.Log("NUMERO NAO EXISTE");
                break;
        }
    }
}
