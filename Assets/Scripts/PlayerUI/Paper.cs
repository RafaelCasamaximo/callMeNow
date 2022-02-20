using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public bool onUi;
    public GameObject paperUIPrefab;
    GameObject paperUI;

    // Start is called before the first frame update
    void Start()
    {
        paperUI = Instantiate(paperUIPrefab);
        paperUI.name = "Paper UI";
        GameManager.Instance.paperUI = paperUI;
        onUi = false;
        paperUI.SetActive(onUi);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            if(!GameManager.Instance.HasUIOpen && !GameManager.Instance.HasTelephoneOpen){
               ToggleUI(); 
            }
        }
    }

    void ToggleUI(){
        onUi = !onUi;
        paperUI.SetActive(onUi);
        if(!onUi){
            GameManager.Instance.LockMouse();
            GameManager.Instance.SetActiveCoreUI(true);
            GameManager.Instance.ResumePlayer();
        }
        else if(onUi){
            GameManager.Instance.FreeMouse();
            GameManager.Instance.SetActiveCoreUI(false);
            GameManager.Instance.StopPlayer();
        }
    }

}
