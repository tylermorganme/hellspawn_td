using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Might need to require that there is a collider because the OnMouseDown depends on the presence on one.
public class Selectable: MonoBehaviour
{
    [SerializeField]
    private SOGameObjectChannel _onSelectionChanged;
    [SerializeField]
    private UnityEvent _triggerOnSelected;
    [SerializeField]
    private UnityEvent _triggerOnUnselected;
    private bool _isSelected = false;

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
        if (_isSelected)
            return;
        _onSelectionChanged.RaiseEvent(gameObject);
    }

    bool IsSelectedGameObjectThisGameObject(GameObject otherGameObject)
    {
        if (otherGameObject == null)
        {
            return false;
        }
        return gameObject.GetInstanceID() == otherGameObject.GetInstanceID();
    }

    private void HandleSelected()
    {
        _isSelected = true;
        _triggerOnSelected.Invoke();
    }

    private void HandleUnselected()
    {
        _isSelected = false;
        _triggerOnUnselected.Invoke();
    }

    public virtual void HandleSelectionUpdate(GameObject selectedGameObject)
    {
        if (_isSelected & !IsSelectedGameObjectThisGameObject(selectedGameObject))
        {
            HandleUnselected();
        }
        else if (!_isSelected & IsSelectedGameObjectThisGameObject(selectedGameObject))
        {
            HandleSelected();
        }
    }
}
