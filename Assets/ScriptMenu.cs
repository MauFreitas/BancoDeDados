using UnityEngine;

public class ScriptMenu : BaseUIManager
{
    public void OnclickLogin ()
    {
        
    }
    public void OnclickRegister()
    {
        _uiManager.OpenUI(UI.Register_Menu);
    }
    public void OnclickExit()
    {
        Application.Quit();
        print("Saiu");
        //Botão Sair não funciona
    }

}
