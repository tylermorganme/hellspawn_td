using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMenuVisibleOnSelected : SelectableMonoBehaviour
{
    [SerializeField]
    Canvas _radialMenu;

    private void Start()
    {
        _radialMenu.gameObject.SetActive(false);
    }
    public override void HandleSelected(GameObject selectedGameObject)
    {
        _radialMenu.gameObject.SetActive(true);
    }

    public override void HandleUnselected(GameObject selectedGameObject)
    {
        _radialMenu.gameObject.SetActive(false);
    }
}
