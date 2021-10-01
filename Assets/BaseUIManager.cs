using UnityEngine;

public abstract class BaseUIManager : MonoBehaviour

{
    [SerializeField]
    protected UIManager _uiManager = null;

    [SerializeField]
    protected UI _ui = UI.None;
    public UI UI
    {
        get
        {
            return _ui;
        }
    }
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    public virtual void ResetUI()
    {
        Close();
    }
}
