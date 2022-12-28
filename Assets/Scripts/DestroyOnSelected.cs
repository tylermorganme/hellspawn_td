using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSelected : SelectableMonoBehaviour
{
    public override void HandleSelected(GameObject selectedGameObject)
    {
        Destroy(gameObject);
    }

    public override void HandleUnselected(GameObject selectedGameObject)
    {
    }

}
