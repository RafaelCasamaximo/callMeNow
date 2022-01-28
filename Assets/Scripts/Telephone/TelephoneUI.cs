using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelephoneUI : MonoBehaviour
{

    TMP_InputField numberTextField;
    string typedNumber;
    string[] buttonsNames = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "*", "0", "#"};
    Button[] telephoneButtons = new Button[12];

    void Start(){

        numberTextField = GameObject.Find("Telephone UI/Number").GetComponent<TMP_InputField>();
        numberTextField.onValueChanged.AddListener(delegate { OnInputNumberChange(); });

        FindButtons();
        ConfigureButtons();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            ProgressManager.Instance.CallNumber(typedNumber);
        }
        else if(Input.GetKeyDown(KeyCode.Backspace) && typedNumber.Length > 0){
            numberTextField.text = typedNumber.Remove(typedNumber.Length - 1);
        }
    }

    void FindButtons(){
        int index = 0;
        foreach(string btnName in buttonsNames){
            telephoneButtons[index] = GameObject.Find("Telephone UI/Telephone Base/Buttons/" + btnName).GetComponent<Button>();
            index++;
        }
    }

    void ConfigureButtons(){
        telephoneButtons[0].onClick.AddListener(delegate { OnNumberChange(buttonsNames[0]); });
        telephoneButtons[1].onClick.AddListener(delegate { OnNumberChange(buttonsNames[1]); });
        telephoneButtons[2].onClick.AddListener(delegate { OnNumberChange(buttonsNames[2]); });
        telephoneButtons[3].onClick.AddListener(delegate { OnNumberChange(buttonsNames[3]); });
        telephoneButtons[4].onClick.AddListener(delegate { OnNumberChange(buttonsNames[4]); });
        telephoneButtons[5].onClick.AddListener(delegate { OnNumberChange(buttonsNames[5]); });
        telephoneButtons[6].onClick.AddListener(delegate { OnNumberChange(buttonsNames[6]); });
        telephoneButtons[7].onClick.AddListener(delegate { OnNumberChange(buttonsNames[7]); });
        telephoneButtons[8].onClick.AddListener(delegate { OnNumberChange(buttonsNames[8]); });
        telephoneButtons[9].onClick.AddListener(delegate { OnNumberChange(buttonsNames[9]); });
        telephoneButtons[10].onClick.AddListener(delegate { OnNumberChange(buttonsNames[10]); });
        telephoneButtons[11].onClick.AddListener(delegate { OnNumberChange(buttonsNames[11]); });

    }

    void OnNumberChange(string number){
        typedNumber += number;
        ChangeInputText();
    }

    void ChangeInputText(){
        numberTextField.text = typedNumber;
        Debug.Log("ChangeInputText: " + numberTextField.text);
    }

    void OnInputNumberChange(){
        typedNumber = numberTextField.text;
        Debug.Log("OnInputNumberChange: " + numberTextField.text);
    }


    public void StartTelephone(){
        Debug.Log("Startando Telephone");
    }

    public void OpenTelephone(){
        gameObject.SetActive(true);
    }

    public void CloseTelephone(){
        gameObject.SetActive(false);
    }

}
