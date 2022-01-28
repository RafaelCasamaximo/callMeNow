using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ComputerUI : MonoBehaviour
{
    //Variáveis base
    public string computerName;
    public GameObject computerUIPrefab;
    public GameObject player;
    public ComputerInfo computer;
    GameObject computerUI;

    //Variáveis taskbar
    TMP_Text hourTxt;
    Button closeComputer;

    //Variáveis para a lockscreen
    public float tipTime = 3f;
    TMP_InputField passwordInput;
    Button enterPassword;
    string typedPassword;
    GameObject lockScreen;
    Button tipButton;
    TMP_Text tipText;
    GameObject tipTextGameObject;
    Image lockScreenPfp;
    TMP_Text lockScreenWelcomeText;

    //Variáveis para a criação dos ícones
    Button[] filesButton;
    TMP_Text[] filesText;
    bool[] filesUtilized;

    //Variáveis da tela de arquivo
    GameObject fileScreen;
    Button closeFile;
    TMP_Text fileTitleScreen;
    TMP_Text fileContentScreen;


    // Start is called before the first frame update
    void Start()
    {

        computerName = "Computer-" + ComputerManager.Instance.NewComputer();

        //Instancia novo objeto na cena e altera o nome dele
        computerUI = Instantiate(computerUIPrefab);
        computerUI.name = computerName;

        //Procura pelo texto da hora do computador pra poder startar a corrotina
        hourTxt = GameObject.Find(computerName + "/Background Computer/Taskbar/hour").GetComponent<TMP_Text>();
        //Procura pelo botão da dica da senha
        tipButton = GameObject.Find(computerName + "/Lock Screen/Password/Tip Password").GetComponent<Button>();
        //Procura pelo texto de dica da senha p/ poder mudar conteúdo
        tipText = GameObject.Find(computerName + "/Lock Screen/Password/Tip Message").GetComponent<TMP_Text>();
        //Procura pelo gameObject do texto da dica para poder desabilitar
        tipTextGameObject = GameObject.Find(computerName + "/Lock Screen/Password/Tip Message");
        //Procura a interface do arquivo e desabilita ela no começo
        fileScreen = GameObject.Find(computerName + "/File Screen");
        //Procura pelo texto de título do arquivo
        fileTitleScreen = GameObject.Find(computerName + "/File Screen/File Title").GetComponent<TMP_Text>();
        //Procura pelo texto de conteúdo do arquivo
        fileContentScreen = GameObject.Find(computerName + "/File Screen/File Content").GetComponent<TMP_Text>();
        //Procura pelo botão de fechar o computador
        closeComputer = GameObject.Find(computerName + "/Background Computer/Taskbar/Close Button").GetComponent<Button>();
        //Procura pelo objeto lockScreen e salva ele numa variável
        lockScreen = GameObject.Find(computerName + "/Lock Screen");
        //Procura o InputField pra senha no gameObject
        passwordInput = GameObject.Find(computerName + "/Lock Screen/Password/Input Password").GetComponent<TMP_InputField>();
        //Procura o Enter Password pra confirmar a senha no gameObject
        enterPassword = GameObject.Find(computerName + "/Lock Screen/Password/Enter Password").GetComponent<Button>();
        //Procura o botão de fechar arquivo e adiciona o evento
        closeFile = GameObject.Find(computerName + "/File Screen/Close File Button").GetComponent<Button>();
        //Procura pela imagem de usuário da lockscreen
        lockScreenPfp = GameObject.Find(computerName + "/Lock Screen/Profile Picture").GetComponent<Image>();
        //Procura pelo texto de boas-vindas da lockscreen
        lockScreenWelcomeText = GameObject.Find(computerName + "/Lock Screen/Welcome Text").GetComponent<TMP_Text>();


        CreateFiles();
        FillUI();
        StartCoroutine(UpdateTime());
        computerUI.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO Submeter a senha quando apertar enter
        //TODO Sair da UI quando apertar esq
    }

    /*
        Atualiza o horário da UI do computador com base na data do PC
    */
    IEnumerator UpdateTime()
    {
        while(true)
        {
            DateTime today = System.DateTime.Now;
            
            //TMP_Text txt = computerUI.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
            hourTxt.text = today.ToString("HH:mm:ss - MM/dd/yyyy");

            yield return new WaitForSeconds(0.2f);
        }
    }

    /*
    Esse método é chamado quando esse objeto é atingido por um RayCast
    Sempre que um raycast atinge algo, ele envia a execução da função Hit
    */
    public void Hit(){
        StartComputer();
    }

    /*
        Executado quando o PC é aberto (quando o jogador interagem com o computador)
    */
    public void StartComputer(){
        computerUI.SetActive(true);


        GameManager.Instance.HasUIOpen = true;
        GameManager.Instance.SetActiveCoreUI(false);
        GameManager.Instance.SetActivePaperUI(false);
        GameManager.Instance.StopPlayer();
        GameManager.Instance.FreeMouse();


        closeComputer.onClick.AddListener(delegate { CloseComputer(); });


        //Quando acionar o botão, executa a função p/ mostrar tip
        tipButton.onClick.AddListener(delegate { DisplayTipForSeconds(tipTime); });
        //Altera o texto da tip
        tipText.text = computer.tip;
        //Esconde a tip no começo
        tipTextGameObject.SetActive(false);

        //Toda vez que o texto for editado ele vai chamar o método onPasswordChange
        passwordInput.onValueChanged.AddListener(delegate {OnPasswordChange(); });
        //Toda vez que o botão for clicado ele vai chamar o método verifyPassword
        enterPassword.onClick.AddListener(delegate {VerifyPassword(); });
        //Toda vez que o botão for clicado ele vai chamar CloseFileScreen
        closeFile.onClick.AddListener(delegate { CloseFileScreen(); });

        //Desabilita a tela depois de já ter pego as referências do que precisava
        fileScreen.SetActive(false);
    }


    /*
        Fecha a UI do arquivo aberto
        Usado no delegate dfo cluseFile
    */
    public void CloseFileScreen(){
        fileScreen.SetActive(false);
    }

    /*
        Chamada quando o botão da tip é ativado, chama um IEnumerator que dura uma quantia de tempo e esconde a tip depois disso
    */
    public void DisplayTipForSeconds(float seconds){
        tipTextGameObject.SetActive(true);
        StartCoroutine(HideTip(seconds));
    }

    /*
        Esconde a tip depois de seconds segundos
    */
    IEnumerator HideTip(float seconds){
        yield return new WaitForSeconds(seconds);
        tipTextGameObject.SetActive(false);
    }

    /*
        Executado quando o PC é fechado (quando o jogador sai do computador)
    */
    public void CloseComputer(){
        GameManager.Instance.HasUIOpen = false;
        computerUI.SetActive(false);
        GameManager.Instance.SetActiveCoreUI(true);
        GameManager.Instance.ResumePlayer();
        GameManager.Instance.LockMouse();
    }

    public void DebugComputerInfo(){
        Debug.Log("Username: " + computer.username);
        Debug.Log("Password: " + computer.password);
        Debug.Log("Files: ");
        foreach(TextFile file in computer.files){
            Debug.Log("   -Filename: " + file.filename);
            Debug.Log("   -Content: " + file.content);
        }
    }

    /*
        Executado quando a UI é instanciada no começo da cena. Atualiza os atributos da UI com base no ComputerInfo
    */
    public void FillUI(){
        lockScreenPfp.sprite = computer.profilePicture;

        lockScreenWelcomeText.text = "Welcome, <b>" + computer.username + "</b>.";
    }

    public void CreateFiles(){

        //FIXME Isso funciona com mais arquivos? Ele não vai pegar o nome errado? Se não funcionar procurar algo pra dar find em gameObjects childs

        filesButton = new Button[8];
        filesText = new TMP_Text[8];
        filesUtilized = new bool[8];

        int fileNumber = 0;
        foreach(TextFile tf in computer.files){
            if(fileNumber > 7){
                break;
            }
            
            //Adiciona o botão no vetor
            filesButton[fileNumber] = GameObject.Find(computerName + "/Background Computer/files/file " + (fileNumber + 1)).GetComponent<Button>();
            filesText[fileNumber] = GameObject.Find(computerName + "/Background Computer/files/file " + (fileNumber + 1) + "/fileName " + (fileNumber + 1)).GetComponent<TMP_Text>();
            //Adiciona o texto no vetor

            //Adiciona o texto do nome
            filesText[fileNumber].text = "<b>" + tf.filename + "</b>.txt";
            //Define o isUtilized daquele arquivo como true para poder esconder os ícones não utilizados depois
            filesUtilized[fileNumber] = true;

            //Adiciona função para o botão
            //TODO Fazer a interface do arquivo e colocar o link para o botão aqui (precisa do scroll text)
            filesButton[fileNumber].onClick.AddListener(delegate { OpenTextFile(tf); });

            fileNumber++;
        }

        fileNumber = 0;
        foreach(bool isUtilized in filesUtilized){
            GameObject.Find("file " + (fileNumber + 1)).SetActive(isUtilized);
            fileNumber++;
        }
    }

    /*
        Carrega o conteúdo do textfile passado por parâmetro na interface e define a
        interface como ativa
    */
    public void OpenTextFile(TextFile textFile){
        //Ativa a interface
        fileScreen.SetActive(true);
        //File Name
        fileTitleScreen.text = "<b>" + textFile.filename + "</b>.txt";
        //Fle Content
        fileContentScreen.text = textFile.content;
    }

    /*
        Executado a cada caractere que o jogador digita no campo de senha
    */
    public void OnPasswordChange(){
        typedPassword = passwordInput.text;
    }

    /*
        Executado quando o jogador clica no botão para submeter senha
    */
    public void VerifyPassword(){
        //Debug.Log(typedPassword == computer.password ? "Senha Certa" : "Senha Errada");
        if(typedPassword == computer.password){
            lockScreen.SetActive(false);
        }
    }

}
