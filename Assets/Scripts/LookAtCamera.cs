using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

  private Transform camera;

	// Use this for initialization
	void Start () 
  {
		camera = GameObject.Find ("Main Camera").transform;
	}
	
	// Update is called once per frame
	void Update () 
  {
		transform.LookAt (camera.position);
	}
}
