using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFewSecAfter : MonoBehaviour {

  [SerializeField]
  private float sec;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, sec);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
