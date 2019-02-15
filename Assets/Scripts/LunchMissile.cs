using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchMissile : MonoBehaviour {

  [SerializeField]
  private GameObject missile;

  [SerializeField]
  private Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
      Lunch ();
	}

  private void Lunch ()
  {
    float r = 3.0f; 
    int number = 12;
    float rootRot = 360.0f / (float)number;

    Vector3 diff = target.position - transform.position;
    float length = diff.magnitude;

    for (int i = 0; i < number; i++)
    {

      float rot = rootRot * i;
      float radianTheta = rot * Mathf.Deg2Rad;

      Vector3 ring = new Vector3 (Mathf.Cos (radianTheta), Mathf.Sin (radianTheta), 0.0f);
      ring *= r;
      Vector3 pos =  this.transform.position + ring;

      Vector3 diffPos = transform.position - pos;
      Quaternion toPlayer = Quaternion.identity * Quaternion.LookRotation (diffPos);

      Quaternion rollRot = Quaternion.Euler (90, rot, 0);

      GameObject missileObj = Instantiate (missile, pos, Quaternion.Inverse (rollRot)) as GameObject;
      MissileMove missileMove = missileObj.GetComponent<MissileMove> ();

      missileMove.TargetTransform = target;
      missileMove.Timer = length * 0.05f;

      if (i == 0)
        missileMove.captain = true;
    }
  }
}
