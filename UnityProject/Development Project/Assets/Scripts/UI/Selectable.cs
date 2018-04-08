using UnityEngine;

public class Selectable : MonoBehaviour
{
    private bool selected;

    public bool Selected { get { return selected; } }

    /// <summary>
    /// Called when the object is being focused.
    /// </summary>
    public void Select()
    {
        if (!selected)
        {
            Debug.Log("SELECTED");
            selected = true;
            SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// Called when the object is being unfocused.
    /// </summary>
    public void Deselect()
    {
        if (selected)
        {
            Debug.Log("DESELECTED");
            selected = false;
            SendMessage("OnDeselected", SendMessageOptions.DontRequireReceiver);
        }
    }
}