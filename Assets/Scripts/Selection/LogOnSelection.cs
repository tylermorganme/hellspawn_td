using UnityEngine;

public class LogOnSelection : SelectableMonoBehaviour
{
    [SerializeField]
    private string _selectedText = "Object was Selected";
    [SerializeField]
    private string _unSelectedText = "Object was Unselected";
    public override void HandleSelected(GameObject selectedGameObject)
    {
        Debug.Log(_selectedText);
    }

    public override void HandleUnselected(GameObject selectedGameObject){
        Debug.Log(_unSelectedText);
    }
}
