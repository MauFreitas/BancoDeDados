using UnityEngine.Networking;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuLogin : BaseUIManager
{
    [Space()]

    [SerializeField]
    private TextMeshProUGUI _feedbackText = null;

    [Space()]

    [SerializeField]
    private ChangeFieldSelection _emailFieldSelection = null;

    [Space()]

    [SerializeField]
    private Button _loginButton = null;
    [SerializeField]
    private Button _backButton = null;

    [Space()]

    [SerializeField]
    private TMP_InputField _emailField = null;
    [SerializeField]
    private TMP_InputField _passWordField = null;

    private bool _wait = false;
    private bool _updateTime = false;

    private float _time = 0.0f;
    private const float _maxFeedBackTime = 4.0f;
    private const int _minPassWordSize = 8;
    private const int _maxPassWordSize = 16;
    private const string _loginLink = "http://localhost/aula/login.php";

    private void Update()
    {
        if (_updateTime)
        {
            _time += Time.deltaTime;
            if(_time >= _maxFeedBackTime)
            {
                _feedbackText.text = "";
                _updateTime = false;
            }
        }
    }
    public override void Open()
    {
        base.Open();
        ClearUI();
    }
    public void OnClickLogin()
    {
        string email = _emailField.text;

        if (string.IsNullOrEmpty(email))
        {
            UpdateFeedback("Você não digiou um email");
            return;
        }
        string password = _passWordField.text;
        if(password.Length< _minPassWordSize)
        {
            UpdateFeedback($"A senha digitada tem menos de {_minPassWordSize} caracteres");
            return;
        }
        if(password.Length > _maxPassWordSize)
        {
            UpdateFeedback($"A senha digitada tem mais de {_maxPassWordSize} caracteres");
            return;
        }        
        StartCoroutine(Login());
    }
    
    private void ClearUI()
    {   
        _wait =false;
        _emailField.text = "";
        _passWordField.text = "";

        _emailField.interactable = true;
        _passWordField.interactable = true;

        _loginButton.interactable = true;
        _backButton.interactable = true;

        _emailFieldSelection.SetFocus();


    }
    public void OnClickBack()
    {
        _uiManager.OpenUI(UI.Main_Menu);
    }
    private void UpdateFeedback(string text)
    {
        _time = 0.0f;
        _updateTime = true;

        _feedbackText.text = text;
    }
    private string nomeDoUsuario;
    private IEnumerator Login()
    {
        _emailField.interactable = false;
        _passWordField.interactable = false;

        _loginButton.interactable = false;
        _backButton.interactable = false;

        WWWForm form = new WWWForm();
        form.AddField("login", _emailField.text);
        form.AddField("password", _passWordField.text);
        UnityWebRequest request = UnityWebRequest.Post(_loginLink, form);

        yield return request.SendWebRequest();

        string result = request.downloadHandler.text;
        string[] split = result.Split('\t');

        if (int.TryParse(split[0],out int _))
        {   
            //login executado com sucesso
            nomeDoUsuario = split[1];
            _uiManager.OpenUI(UI.Main_Menu);
        }
        else
        {
            UpdateFeedback(result);
            ClearUI(); 
        }
        //conseguiu acessar o banco de dados
        //_uiManager.OpenUI(UI.Characters_Menu);
        
        /*1 pde para o banco de dados verificar se o email e senha são validos
         * 2 banco de dados responde um codigo
         * codigo 0 -> sucesso login
         * codigo 1 -> email errado
         * codigo 2 -> senha errada
         * 
         * if(respostaServidor == 0)
        {
            conseguiu acesas o banco de dados 
        _uiManager.OpenUI(UI.Characters_Menu);
        
        }
        else if(respostaServidor == 1)
        {
            UpdateFeedback("Você não digitou um email valido");
        }
        else if(respostaServidor == 2)
        {
            UpdateFeedback("Você não digitou uma senha valida");
        }
         */
    }

}
