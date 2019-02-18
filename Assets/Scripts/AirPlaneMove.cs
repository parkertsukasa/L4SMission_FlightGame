using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneMove : MonoBehaviour 
{
  [SerializeField]
  private float speed;

  [SerializeField]
  private float kRoll;


	// Use this for initialization
	void Start () 
  {
		
	}
	
	// Update is called once per frame
	void Update () 
  {
    Accelaration ();
		Hundle ();
    RiseFall ();
	}

  /* -------------------- Accelaration() --------------------
   * ハンドル : yaw角およびroll角のコントロール
   */
  private void Accelaration ()
  {
    transform.Translate (Vector3.forward * speed * Time.deltaTime);
  }

  /* -------------------- RiseFall () --------------------
   * 上昇下降 : pitch角のコントロール
   */
  private void RiseFall ()
  {
    float inputVert = Input.GetAxis ("Vertical");
    transform.Rotate (Vector3.right * inputVert * 2.0f);
  }

  /* -------------------- Hundle () --------------------
   * ハンドル : yaw角およびroll角のコントロール
   */
  private void Hundle ()
  {
    float inputHundle = Input.GetAxis ("Horizontal");

    transform.Rotate (Vector3.up * inputHundle * 2.0f);
    Vector3 nowRot = transform.rotation.eulerAngles;
    Quaternion newRot = Quaternion.Euler(nowRot.x, nowRot.y, inputHundle * -30.0f);
    //transform.rotation = newRot;

    if (inputHundle == 0.0f)
    {
      /* 
      if (transform.rotation.z > 0.1f)
        transform.Rotate (Vector3.forward * kRoll);
      else if (transform.rotation.z < -0.1f)
        transform.Rotate (Vector3.forward * -kRoll);
      else
        transform.rotation = Quaternion.Euler (nowRot.x, nowRot.y, 0.0f);

        */
    }
    else
    {
      if (transform.rotation.z < 45.0f && transform.rotation.z > -45.0f)
        transform.Rotate (Vector3.forward * kRoll * inputHundle);
      else
      {
        if (transform.rotation.z > 45.0f)
          transform.rotation = Quaternion.Euler (nowRot.x, nowRot.y, 45.0f);
        else if (transform.rotation.z < -45.0f)
          transform.rotation = Quaternion.Euler (nowRot.x, nowRot.y, -45.0f);
      }
    }
  }
}
