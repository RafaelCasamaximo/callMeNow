using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
    public Button playBtn;
    public Button quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        playBtn.gameObject.GetComponent<Button>().onClick.AddListener(GameManager.Instance.StartGame);
        quitBtn.gameObject.GetComponent<Button>().onClick.AddListener(GameManager.Instance.Quit);
    }

}
