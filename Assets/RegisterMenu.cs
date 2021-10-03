using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;
public class RegisterMenu : BaseUIManager
{
   
    [Space()]
    [SerializeField]
    private TextMeshProUGUI _feedBackText = null;
    [Space()]
    
    [SerializeField]
    private ChangeFieldSelection _userNameFieldSelection = null;
    [Space()]
    [SerializeField]
    
    
    private bool _wait = false;
    private bool _updateTime = false;

    private const int minNameSize = 2;
    private const int maxNameSize = 10;

    private const int minPasswordSize = 8;
    private const int maxPasswordSize = 16;

    private readonly Color _red = Color.red;
    private readonly Color _green = Color.green;

    private float _time = 0.0f;
    private const float _maxFeedBackTime = 4.0f;
    
    [SerializeField]
    private TMP_InputField _userNameField = null;
    [Space()]
    [SerializeField]
    private TMP_InputField _emailField = null;
    [SerializeField]
    private TMP_InputField _passWordField = null;
    [Space()]
    private const string _registerLink = "http://localhost/aula/registro.php";

    [SerializeField]
    private Button _registerButton = null;
    [SerializeField]
    private Button _backButton = null;

    private void Update()
    {
        if (_updateTime)
        {
            _time += Time.deltaTime;
            if(_time >= _maxFeedBackTime)
            {
                _feedBackText.text = "";
                _updateTime = false;
            }
        }
    }
    public override void Open()
    {
        base.Open();
        _userNameFieldSelection.SetFocus();
    }
    private void ClearUI()
    {
        _wait = false;

        _emailField.text = "";
        _passWordField.text = "";
        _userNameField.text = "";

        _emailField.interactable = true;
        _passWordField.interactable = true;
        _userNameField.interactable = true;

        _registerButton.interactable = true;
        _backButton.interactable = true;

        _userNameFieldSelection.SetFocus();
     }
    public void onClickRegister()
    {
        if (_wait)
        {
            return;
        }
        string username = _userNameField.text;
        if(username.Length<minNameSize||username.Length> maxNameSize)
        {
            UpdateFeedBack($"Username deve ter entre {minNameSize}  e {maxNameSize} caracteres", false);
            return;
        }
        string password = _passWordField.text;
        if (password.Length < minPasswordSize || username.Length > maxPasswordSize)
        {
            UpdateFeedBack($"Password deve ter entre {minPasswordSize} e {maxPasswordSize} caracteres", false);
            return;
        }
        StartCoroutine(Register());
        
    }
    public void OnClickBack()
    {
        if (_wait)
        {
            return;
        }
        _uiManager.OpenUI(UI.Main_Menu);
    }

    private void UpdateFeedBack(string text, bool sucess)
    {
        _time = 0.0f;
        _updateTime = true;
        _feedBackText.text = text;
        _feedBackText.color = sucess ? _green : _red;
        //Não fica verde de jeito nenhum quando da certo!!!!!!!! 
    }
    private IEnumerator Register()
    {
        _wait = true;

        _userNameField.interactable = false;
        _emailField.interactable = false;
        _passWordField.interactable = false;

        _registerButton.interactable = false;
        _backButton.interactable = false;

        WWWForm form = new WWWForm();
        form.AddField("username", _userNameField.text);
        form.AddField("email", _emailField.text);
        form.AddField("password", _passWordField.text);
 
        UnityWebRequest request = UnityWebRequest.Post(_registerLink, form);
        yield return request.SendWebRequest();
        //Debug.Log(request.downloadHandler.text);
        if (string.Compare("success", request.downloadHandler.text)==0)
        {
            UpdateFeedBack("Registro feito com sucesso", true);
        }
        else
        {
            UpdateFeedBack(request.downloadHandler.text, false);
        }

        ClearUI();
    }
     

    public override void ResetUI()
    {
        //throw new System.NotImplementedException();
    }
}
