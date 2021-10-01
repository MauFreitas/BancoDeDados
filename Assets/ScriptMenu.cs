using UnityEngine;

public class ScriptMenu : BaseUIManager
{   

    public void OnclickLogin ()
    {
        _uiManager.OpenUI(UI.Login_Menu);

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
    public override void ResetUI()
    {
       
    }

}
