using System;
using UnityEngine;

[CreateAssetMenu(menuName="Events/Game Object Event Channel")]
public class SOGameObjectChannel : ScriptableObject
{
    public Action<GameObject> OnEventRaised;
    // Start is called before the first frame update
    
    public void RaiseEvent(GameObject selectedGameObject)
    {
        OnEventRaised?.Invoke(selectedGameObject);
    }
}
