using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectVisibleOnSelection : SelectableMonoBehaviour
{
    [SerializeField]
    Canvas _menu;

    private void Start()
    {
        _menu.gameObject.SetActive(false);
    }
    public override void HandleSelected(GameObject selectedGameObject)
    {
        _menu.gameObject.SetActive(true);
    }

    public override void HandleUnselected(GameObject selectedGameObject)
    {
        _menu.gameObject.SetActive(false);
    }
}
