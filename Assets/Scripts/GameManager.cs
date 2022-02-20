using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject coreUI;
    public GameObject paperUI;
    public GameObject dialogueUI;
    public bool HasUIOpen {get; set;}
    public bool HasTelephoneOpen {get; set;}
    public Camera PlayerCamera;



    /*
    Singleton Pattern
    */
    private static GameManager _instance;

    public static GameManager Instance{
        get{
            if(_instance == null){
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    void Awake(){
        _instance = this;
    }


    void Start(){
        PlayerCamera = Camera.main;
        coreUI = GameObject.Find("Core UI");
        HasUIOpen = false;
        HasTelephoneOpen = false;
    }

    /*
    Fecha o Jogo
    */
    public void Quit(){
        Application.Quit();
    }

    /*
    Carrega a cena de gameplay do jogo
    */

    public void StartGame(){
        SceneManager.LoadScene("Gameplay");
    }

    /*
    Faz o player parar de poder se movimentar
    */

    public void StopMovement(){
        GameObject.Find("First Person Player").gameObject.GetComponent<PlayerMovement>().canMove = false;
        Cursor.lockState = CursorLockMode.None;
    }

    /*
    Faz o player parar de poder movimentar a câmera
    */

    public void StopRotation(){
        Camera.main.gameObject.GetComponent<MouseLook>().canMove = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /*
    Faz o player poder se movimentar
    */
    public void ResumeMovement(){
        GameObject.Find("First Person Player").gameObject.GetComponent<PlayerMovement>().canMove = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /*
    Faz o player poder movimentar a câmera
    */
    public void ResumeRotation(){
        Camera.main.gameObject.GetComponent<MouseLook>().canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /*
    Faz o player parar de poder movimentar a câmera e se movimentar
    */
    public void StopPlayer(){
        StopMovement();
        StopRotation();
        StopRayCasting();
    }

    /*
    Faz o player poder movimentar a câmera e se movimentar
    */
    public void ResumePlayer(){
        ResumeMovement();
        ResumeRotation();
        ResumeRayCasting();
    }



    public void StopPlayerInSeconds(float secs){
        StartCoroutine(StopPlayerInSecondsCoroutine(secs));
    }

    public IEnumerator StopPlayerInSecondsCoroutine(float secs){
        yield return new WaitForSeconds(secs);
        StopMovement();
        StopRotation();
        StopRayCasting();
    }

    public void ResumePlayerInSeconds(float secs){
        StartCoroutine(ResumePlayerInSecondsCoroutine(secs));
    }
    
    public IEnumerator ResumePlayerInSecondsCoroutine(float secs){
        yield return new WaitForSeconds(secs);
        ResumeMovement();
        ResumeRotation();
        ResumeRayCasting();
    }




    public void SetActiveCoreUI(bool isActive){
        if(coreUI != null){
            coreUI.SetActive(isActive);
        }
    }

    public void SetActivePaperUI(bool isActive){
        if(paperUI != null){
            paperUI.SetActive(isActive);
        }
    }
    
    public void SetActiveDialogueUI(bool isActive){
        if(dialogueUI != null){
            dialogueUI.SetActive(isActive);
        }
    }

    public void LockMouse(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void FreeMouse(){
        Cursor.lockState = CursorLockMode.None;
    }

    public void StopRayCasting(){
        Camera.main.gameObject.GetComponent<RayShooter>().canShoot = false;
    }

    public void ResumeRayCasting(){
        Camera.main.gameObject.GetComponent<RayShooter>().canShoot = true;
    }
}
