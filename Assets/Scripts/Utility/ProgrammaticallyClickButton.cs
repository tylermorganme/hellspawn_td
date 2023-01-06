using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ProgrammaticallyClickButton : MonoBehaviour
{
    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
    }

    [ContextMenu("ClickButton")]
    void ClickButton()
    {
        _button.onClick.Invoke();
    }
}
