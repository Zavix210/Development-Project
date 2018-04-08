﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableText : MonoBehaviour
{
    [SerializeField]
    private TextMesh textMesh;
    [SerializeField]
    private BoxCollider boxCollider;
    [SerializeField]
    private float textThickness = 0.2f;
    [SerializeField]
    private MeshRenderer textRenderer;

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

    /// <summary>
    /// Adjust the collision box to fit the text.
    /// </summary>
    private void FixBox()
    {
        Bounds rBounds = textRenderer.bounds;
        Vector3 rSize = rBounds.size;

        // Calculate the required size to fit the collision box around the text contents
        Vector3 fScale = textMesh.transform.localScale;
        Vector3 fSize = new Vector3(rSize.x * (1.0f / fScale.x), rSize.y * (1.0f / fScale.y), textThickness);

        // Apply the box scale values
        boxCollider.size = fSize;
        boxCollider.center = Vector3.zero;
    }
}
