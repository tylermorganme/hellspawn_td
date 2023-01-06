using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Might need to require that there is a collider because the OnMouseDown depends on the presence on one.
public abstract class SelectableMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private SOGameObjectChannel _onSelectionChanged;
    public event Action<GameObject> _triggerOnSelected = delegate { };
    public event Action<GameObject> _triggerOnUnselected = delegate { };


    protected virtual void Awake()
    {

    }

    protected virtual void OnEnable()
    {
        _onSelectionChanged.OnEventRaised += HandleSelectionUpdate;
    }

    protected virtual void OnDisable()
    {
        _onSelectionChanged.OnEventRaised -= HandleSelectionUpdate;
    }

    protected virtual void OnMouseDown()
    {
        // Theoretically blocks this from happening if I'm over UI. Seems to work.
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
            _triggerOnSelected(gameObject);
            HandleSelected(selectedGameObject);
        }
        else {
            _triggerOnUnselected(gameObject);
            HandleUnselected(selectedGameObject);
        }
    }
    public abstract void HandleSelected(GameObject selectedGameObject);

    public abstract void HandleUnselected(GameObject selectedGameObject);
}
