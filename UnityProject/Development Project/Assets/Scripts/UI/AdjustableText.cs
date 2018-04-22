using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableText : MonoBehaviour
{
    [SerializeField]
    private TextMesh textMesh;
    [SerializeField]
    private Transform backgroundBox;
    [SerializeField]
    private BoxCollider textCollider;
    [SerializeField]
    private float textThickness = 0.2f;
    [SerializeField]
    private float textPadding = 0.2f;
    [SerializeField]
    private MeshRenderer textRenderer;

    [SerializeField]
    private Color highlighColour;
    [SerializeField]
    private Color standardColour;

    private bool highlighted;

    private void Awake()
    {

    }

    /// <summary>
    /// Set the text and re-calculate the collision box to fit the text.
    /// </summary>
    /// <param name="text"></param>
    public void SetText(string text)
    {
        textMesh.text = text;
        FixBox();
    }

    public void Highlight(bool highlight)
    {
        highlighted = highlight;
    }

    /// <summary>
    /// Adjust the collision box to fit the text.
    /// </summary>
    private void FixBox()
    {
        // TODO: Improvement to this mechanic, it's inefficient but works

        // Re-create the component which resets the bounds of the box correctly
        GameObject parentObj = textCollider.gameObject;
        Destroy(textCollider);
        textCollider = parentObj.AddComponent<BoxCollider>();

        Vector3 size = textCollider.size;
        size.x += textPadding;
        size.y += textPadding;
        size.z = textThickness;

        backgroundBox.position = textCollider.transform.position;
        backgroundBox.transform.localScale = size;

        //Vector3 size = textCollider.size;
        ////size.z = textThickness;
        //textCollider.size = size;

        //Bounds rBounds = textRenderer.bounds;
        //Vector3 rSize = rBounds.size;

        //// Calculate the required size to fit the collision box around the text contents
        //Vector3 fScale = textMesh.transform.localScale;
        ////Vector3 fScale = Vector3.one;

        //Vector3 fSize = new Vector3((rSize.x * (1.0f / fScale.x)) + (textPadding * 2.0f), rSize.y * (1.0f / fScale.y), 1.0f);
        ////Vector3 fSize = rSize;

        //// Scale the box collider to fit the text.
        //boxCollider.transform.localScale = fSize;

        //// Apply the box scale values
        //boxCollider.size = new Vector3(1.0f, 1.0f, textThickness);
        //boxCollider.center = Vector3.zero;
    }

    private void Update()
    {
        

       // FixBox();

        if (highlighted)
        {
            textRenderer.material.color = highlighColour;
        }
        else
        {
            textRenderer.material.color = standardColour;
        }
    }
}
