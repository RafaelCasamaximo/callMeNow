using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    public GameObject telephoneCanvas;

    GameObject telephoneCanvasInstance;
    TelephoneUI telephoneUI;

    GameObject cameraPoint;
    public bool onTelephone;
    CameraViewManager cvm;
    GameObject player;

    Vector3 originalPosition;
    Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        //Configurações para a câmera
        onTelephone = false;
        cameraPoint = GameObject.Find("telephone/Camera Point");
        player = GameObject.Find("First Person Player");
        cvm = Camera.main.GetComponent<CameraViewManager>();

        //Configurações para a UI
        telephoneCanvasInstance = Instantiate(telephoneCanvas);
        telephoneCanvasInstance.name = "Telephone UI";
        telephoneUI = telephoneCanvasInstance.GetComponent<TelephoneUI>();
        telephoneUI.StartTelephone();
        telephoneCanvasInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && onTelephone){
            CloseTelephone();
        }
    }

    public void Hit(){
        GameManager.Instance.StopPlayer();
        OpenTelephone();
    }

    public void OpenTelephone(){
        onTelephone = true;
        GameManager.Instance.HasTelephoneOpen = true;
        GameManager.Instance.SetActiveCoreUI(false);
        GameManager.Instance.StopPlayer();
        GameManager.Instance.FreeMouse();

        originalPosition = player.transform.position;
        originalRotation = player.transform.rotation;

        StartCoroutine(cvm.LerpPosition(player.transform, player.transform.position, cameraPoint.transform.position, 1f));
        StartCoroutine(cvm.LerpRotation(player.transform, player.transform.rotation, cameraPoint.transform.rotation, 1f));
        telephoneUI.OpenTelephone();
    }

    public void CloseTelephone(){
        onTelephone = false;
        GameManager.Instance.HasTelephoneOpen = false;
        
        telephoneUI.CloseTelephone();

        StartCoroutine(cvm.LerpPosition(player.transform, player.transform.position, originalPosition, 1f));
        StartCoroutine(cvm.LerpRotation(player.transform, player.transform.rotation, originalRotation, 1f));
        
        GameManager.Instance.ResumePlayerInSeconds(1f);
        GameManager.Instance.LockMouse();
        GameManager.Instance.SetActiveCoreUI(true);
    }
}