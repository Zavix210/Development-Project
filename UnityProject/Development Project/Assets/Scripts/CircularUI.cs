using UnityEngine;
using System.Collections;

public class CircularUI : MonoBehaviour
{
    [SerializeField]
    private Transform circleUI;
    [SerializeField]
    private Vector3 circleForward = new Vector3(0, 0, 1);
    [SerializeField]
    private float angleLimit;

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

        Debug.DrawLine(transform.position, transform.position + d1 * 5.0f, Color.red);
        Debug.DrawLine(transform.position, transform.position + d2 * 5.0f, Color.blue);

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

        circleUI.transform.forward = circleForward.normalized;

        Debug.Log("Ang: " + ang + " , " + angRes);

        //Vector3 fwd = GetFwdAngle();
        //float angle = Vector3.SignedAngle(circleForward, fwd, Vector3.up);

        //float absAngle = Mathf.Abs(angle);
        //float diff = angleLimit - absAngle;

        //// Is there some angle change required to match the target
        //if (diff < 0)
        //{
        //    if (angle > 0.0f) // To positive
        //    {
        //        Quaternion q = Quaternion.AngleAxis(diff, Vector3.up);
        //        Vector3 final = q * circleForward;
        //        circleForward = final;
        //    }
        //    else // To negative
        //    {

        //    }
        //}

        //Debug.Log("ANGLE: " + angle);

        //Debug.DrawLine(transform.position, circleForward * 5.0f, Color.blue);
    }

    private Vector3 GetFwdAngle()
    {
        Camera cam = Camera.main;
        Vector3 fwd = transform.forward;
        fwd.y = 0.0f;
        return fwd;
    }
}
