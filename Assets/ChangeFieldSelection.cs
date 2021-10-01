using TMPro;
using UnityEngine;

public class ChangeFieldSelection : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _InputField = null;
    [SerializeField]
    private ChangeFieldSelection _nextField = null;

    private void Update()
    {
        if (_InputField.isFocused)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                _nextField.SetFocus();
            }
        }
    }

    public void SetFocus()
    {
        _InputField.Select();

    }
}
