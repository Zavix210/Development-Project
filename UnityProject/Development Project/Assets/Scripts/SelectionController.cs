using UnityEngine;
using System.Collections;

public class SelectionController : MonoBehaviour
{
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private Selectable selectable;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        bool selectFlag = false;

        // Was a physics ray fired correctly?
        if (Physics.Raycast(ray, out hit, 1000.0f, mask))
        {
            // Was something collidable hit?
            if (hit.collider != null)
            {
                // Try to get the selectable on the hit object
                Selectable newSelectable = hit.collider.GetComponent<Selectable>();

                // Does the object have a selectable on it?
                if (newSelectable != null)
                {
                    // Set the flag to true because something was selected
                    selectFlag = true;

                    // Deselect the previous selection
                    if (selectable != null && selectable != newSelectable)
                    {
                        DeselectCurrent();
                    }

                    newSelectable.Select();
                    selectable = newSelectable;
                }
            }
        }

        // Was nothing selected?
        if(!selectFlag)
        {
            DeselectCurrent();
        }
    }

    /// <summary>
    /// Deselect the current selectable if it's assigned.
    /// </summary>
    private void DeselectCurrent()
    {
        // Deselect the current selectable
        if (selectable != null)
        {
            selectable.Deselect();
            selectable = null;
        }
    }
}
