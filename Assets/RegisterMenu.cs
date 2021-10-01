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
    private TMP_InputField _userNameField = null;
    [Space()]
    [SerializeField]
    private ChangeFieldSelection _userNameFieldSelection = null;
    [Space()]
    [SerializeField]
    private TMP_InputField _EmailNameField = null;
    [Space()]
    [SerializeField]
    private TMP_InputField _PassWordNameField = null;
    [Space()]
    
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
        string password = _PassWordNameField.text;
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
        //Não fica verde de jeito nenhum quando da certo!!!!!!!!!
    }
    private IEnumerator Register()
    {
        _wait = true;

        _userNameField.interactable = false;
        _EmailNameField.interactable = false;
        _PassWordNameField.interactable = false;
       
        yield return null;
        UpdateFeedBack("Registro feito com sucesso", true);
        _wait = false;

        _userNameField.interactable = true;
        _EmailNameField.interactable = true;
        _PassWordNameField.interactable = true;

        _userNameField.text = "";
        _EmailNameField.text = "";
        _PassWordNameField.text = "";
    }

    public override void ResetUI()
    {
        //throw new System.NotImplementedException();
    }
}
