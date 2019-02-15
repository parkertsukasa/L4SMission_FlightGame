using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneMove : MonoBehaviour 
{
  [SerializeField]
  private float speed;

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
    transform.Rotate (Vector3.right * inputVert);
  }

  /* -------------------- Hundle () --------------------
   * ハンドル : yaw角およびroll角のコントロール
   */
  private void Hundle ()
  {
    float inputHundle = Input.GetAxis ("Horizontal");

    transform.Rotate (Vector3.up * inputHundle);
    Vector3 nowRot = transform.rotation.eulerAngles;
    Quaternion newRot = Quaternion.Euler(nowRot.x, nowRot.y, inputHundle * -30.0f);
    transform.rotation = newRot;
  }
}
