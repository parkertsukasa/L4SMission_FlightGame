using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRocks : MonoBehaviour 
{

  float rotSpeedX;
  float rotSpeedY;
  float rotSpeedZ;

	// Use this for initialization
	void Start () 
  {
		rotSpeedX = Random.Range (0.5f, 2.0f);
    rotSpeedY = Random.Range (0.5f, 2.0f);
    rotSpeedZ = Random.Range (0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () 
  {
		transform.Rotate (Vector3.right * rotSpeedX);
		transform.Rotate (Vector3.up * rotSpeedY);
		transform.Rotate (Vector3.forward * rotSpeedZ);
	}
}
