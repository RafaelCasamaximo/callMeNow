using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    //Variáveis do Manager
    public int computerCounter;


    /*
    Singleton Pattern
    */
    private static ComputerManager _instance;

    public static ComputerManager Instance{
        get{
            if(_instance == null){
                GameObject go = new GameObject("ComputerManager");
                go.AddComponent<ComputerManager>();
            }
            return _instance;
        }
    }

    void Awake(){
        _instance = this;
    }

    void Start(){
        computerCounter = 0;
    }

    public int NewComputer(){
        int aux = computerCounter;
        computerCounter += 1;
        return aux; 
    }
}