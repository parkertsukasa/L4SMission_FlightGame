using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchMissile : MonoBehaviour {

  [SerializeField]
  private GameObject missile;

  [SerializeField]
  private GameObject lockOnObj;

  [SerializeField]
  private Camera camera;



	// Use this for initialization
	void Start () 
  {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () 
  {
		if (Input.GetMouseButtonDown (0))
      LockOn ();
	}


  private void LockOn ()
  {
    int distance = 1000;
    Vector3 center = new Vector3 (Screen.width/2, Screen.height/2, 0);
    Ray ray = camera.ScreenPointToRay (center);
    RaycastHit raycastHit;


    if (Physics.Raycast (ray, out raycastHit, distance))
    {
      if (raycastHit.collider.gameObject.tag == "Rock")
      {
        GameObject lockOnMark = Instantiate (lockOnObj, raycastHit.point, Quaternion.identity);
        lockOnMark.transform.parent = raycastHit.transform;
        lockOnMark.transform.localPosition = Vector3.zero;
        Lunch (raycastHit.transform);
      }
    }
  }

  private void Lunch (Transform target)
  {
    float r = 3.0f; // 発射位置と飛行機の距離
    int number = 6; // 一度に発射する数
    float rootRot = 360.0f / (float)number;

    Vector3 diff = target.position - transform.position;
    float length = diff.magnitude;

    for (int i = 0; i < number; i++)
    {

      float rot = rootRot * i;
      float radianTheta = rot * Mathf.Deg2Rad;

      // --- 三角関数を用いて飛行機の周囲一周に配置していく ---
      Vector3 ring = new Vector3 (Mathf.Cos (radianTheta), Mathf.Sin (radianTheta), 0.0f);

      ring *= r;
      Vector3 pos =  this.transform.position + ring;

      Quaternion rollRot = Quaternion.Euler (90, rot, 0);

      GameObject missileObj = Instantiate (missile, pos, Quaternion.Inverse (rollRot)) as GameObject;
      MissileMove missileMove = missileObj.GetComponent<MissileMove> ();
      missileMove.TargetTransform = target;

      // --- 飛行機の向きを考慮して移動する ---
      Vector3 myRot = transform.rotation.eulerAngles;
      missileObj.transform.RotateAround (transform.position, Vector3.right, myRot.x);
      missileObj.transform.RotateAround (transform.position, Vector3.up, myRot.y);
      missileObj.transform.RotateAround (transform.position, Vector3.forward, myRot.z);

    }
  }
}
