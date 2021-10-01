using System.Collections.Generic;
using UnityEngine;

public enum UI
{   
    None = 0,
    Main_Menu,
    Register_Menu,
    Login_Menu,
    Characters_Menu, 
}
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<BaseUIManager> _uis = null;
    private BaseUIManager _currentUI = null;
    private void Start()
    {
        OpenUI(UI.Main_Menu);
    }

    public void OpenUI (UI ui)
    {
        _currentUI?.Close();
        for (int i = 0; i < _uis.Count; i++)
        {
            if (_uis[i].UI == ui)
            {
                _currentUI = _uis[i];
                _currentUI.Open();

                break;
            }
        }
    }
    public void CloseUIs()
    {
        _currentUI = null;
        for (int i = 0; i < _uis.Count; i++)
        {
            _currentUI.ResetUI();
        }

    }
}
