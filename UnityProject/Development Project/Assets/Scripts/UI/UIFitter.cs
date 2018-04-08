using UnityEngine;
using System.Collections.Generic;

public class UIFitter : MonoBehaviour
{
    [SerializeField]
    private CircularUI circularUI;
    [SerializeField]
    private float anglePerButton = 5.0f;
    [SerializeField]
    private float spacingPerButton = 2.0f;
    [SerializeField]
    private float buttonYOffsetBelow = 2.0f;

    [SerializeField]
    private List<UIChoiceButton> buttons;

    public List<UIChoiceButton> Buttons { get { return buttons; } }

    public void AddItem(UIChoiceButton button)
    {
        if(button != null && !buttons.Contains(button))
        {
            buttons.Add(button);
        }
    }

    public void RemoveItem(UIChoiceButton button)
    {
        buttons.Remove(button);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the angular space requirements
        int count = buttons.Count;
        int spacingCount = count + 1;
        float totalAngleSpacing = (anglePerButton * count) + (spacingPerButton * spacingCount);
        circularUI.AngularLimit = totalAngleSpacing * 0.5f;

        // Calculate an offset value to move the start direction by.
        float offset = -(totalAngleSpacing * 0.5f) + spacingPerButton;

        // Create the start direction
        Vector3 centreDir = circularUI.CircleForward;
        Vector3 startDir = Quaternion.AngleAxis(offset, Vector3.up) * centreDir;

        // Create some running values for the iteration
        int totalCount = count + spacingCount - 1;
        int runningIndex = 0;
        float runningOffset = 0.0f;

        // Iterate over the total number of items + spaces
        for(int i = 0; i < totalCount; i++)
        {
            if(i % 2 == 0) // Even entries are ui elements
            {
                // Get the button and produce a final angle to use
                UIChoiceButton button = buttons[runningIndex];
                float finalAng = runningOffset + (anglePerButton * 0.5f);

                // Apply the position to the object
                Vector3 dir = Quaternion.AngleAxis(finalAng, Vector3.up) * startDir;
                button.transform.position = (circularUI.transform.position + dir.normalized * 8.0f) + new Vector3(0.0f, -buttonYOffsetBelow, 0.0f);

                button.transform.LookAt(transform.position);

                // Increment the running values
                runningOffset += anglePerButton;
                runningIndex++;
            }
            else // Odd entries are spacing elements
            {
                runningOffset += spacingPerButton;
            }
        }

        Vector3 pos = circularUI.transform.position;
        Debug.DrawLine(pos, pos + centreDir * 5.0f, Color.yellow);
        Debug.DrawLine(pos, pos + startDir * 5.0f, Color.cyan);
    }
}
