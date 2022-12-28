using UnityEngine;

public class LogOnSelection : SelectableMonoBehaviour
{
    public override void HandleSelected(GameObject selectedGameObject)
    {
        Debug.Log("Object Was Selected");
    }

    public override void HandleUnselected(GameObject selectedGameObject){ }
}
