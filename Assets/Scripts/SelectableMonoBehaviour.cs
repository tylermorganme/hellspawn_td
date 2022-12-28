using UnityEngine;
using UnityEngine.EventSystems;

// Might need to require that there is a collider because the OnMouseDown depends on the presence on one.
public abstract class SelectableMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private SOGameObjectChannel _onSelectionChanged;

    public virtual void Awake()
    {
        _onSelectionChanged.OnEventRaised += HandleSelectionUpdate;
    }

    public virtual void OnDestroy()
    {
        _onSelectionChanged.OnEventRaised -= HandleSelectionUpdate;
    }

    public virtual void OnMouseDown()
    {
        // Not sure if this will work. Theoretically blocks this from happening if I'm over UI.
        // Found here https://www.youtube.com/watch?v=rATAnkClkWU
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        _onSelectionChanged.RaiseEvent(gameObject);
    }

    bool ThisIsSelected(GameObject otherGameObject)
    {
        if(otherGameObject == null)
        {
            return false;
        }
        return gameObject.GetInstanceID() == otherGameObject.GetInstanceID();
    }

    public virtual void HandleSelectionUpdate(GameObject selectedGameObject)
    {
        if (ThisIsSelected(selectedGameObject)) { 
            HandleSelected(selectedGameObject);
        }
        else { 
            HandleUnselected(selectedGameObject);
        }
    }
    public abstract void HandleSelected(GameObject selectedGameObject);

    public abstract void HandleUnselected(GameObject selectedGameObject);
}
