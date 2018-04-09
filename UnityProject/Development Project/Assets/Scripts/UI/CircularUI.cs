using UnityEngine;
using System.Collections;

/// <summary>
/// This class manages the orientation of the circular UI and rotates when necessary. The UI
/// will automatically rotate when the forward vector of the object is outside the angle limit. 
/// </summary>
public class CircularUI : MonoBehaviour
{
    [SerializeField]
    private Transform circleUI;
    [SerializeField]
    private Vector3 circleForward = new Vector3(0, 0, 1);
    [SerializeField]
    private float angleLimit;

    public float AngularLimit { get { return angleLimit; } set { angleLimit = value; } }
    public Vector3 CircleForward { get { return circleForward; } }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 d1 = circleForward;
        Vector3 d2 = transform.forward;
        d2.y = 0.0f;

        // Draw the vector values
        Debug.DrawLine(transform.position, transform.position + d1 * 5.0f, Color.red);
        Debug.DrawLine(transform.position, transform.position + d2 * 5.0f, Color.blue);

        // Calculate the angle remainder, less than zero is outside the angle range and
        // some movement is required to push it into range again
        float ang = Vector3.Angle(d1, d2);
        float angRes = angleLimit - ang;

        // Is the angle out of range?
        if (angRes < 0.0f)
        {
            float signedAngle = Vector3.SignedAngle(d1, d2, Vector3.up);

            if (signedAngle > 0.0f) // Positive angle
            {
                circleForward = Quaternion.AngleAxis(-angRes, Vector3.up) * circleForward;
            }
            else // Negative angle
            {
                circleForward = Quaternion.AngleAxis(angRes, Vector3.up) * circleForward;
            }
        }

        // Apply the forward vector to the circle UI parent object
        circleUI.transform.forward = circleForward.normalized;
    }
}
