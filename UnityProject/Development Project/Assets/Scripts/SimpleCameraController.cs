using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 45.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float axisX = Input.GetAxis("Mouse X");
        float axisY = -Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(axisY * Time.deltaTime * moveSpeed, axisX * Time.deltaTime * moveSpeed, 0.0f));
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 0.0f;
        transform.rotation = Quaternion.Euler(rot);
	}
}
