using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceCaster : MonoBehaviour
{
    [SerializeField]
    private float castDistance = 5.0f;
    [SerializeField]
    private Transform cursor;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 hitPos, dirBack;

        ProjectToSurface(transform.position, transform.forward, castDistance, out hitPos, out dirBack);

        // Move the cursor to the point on the surface of the casted sphere
        cursor.transform.position = hitPos;

        // Make the cursor look at the caster
        cursor.transform.LookAt(transform.position);

        // Zero the Z rotation to ensure that the cursor always remains upright
        Vector3 rot = cursor.transform.rotation.eulerAngles;
        rot.z = 0.0f;
        cursor.transform.rotation = Quaternion.Euler(rot);
	}

    void ProjectToSurface(Vector3 origin, Vector3 outDirection, float surfaceDistance, out Vector3 hitPosition, out Vector3 dirBack)
    {
        Vector3 normDir = outDirection.normalized;
        hitPosition = origin + (normDir * surfaceDistance);
        dirBack = -normDir;
    }
}
