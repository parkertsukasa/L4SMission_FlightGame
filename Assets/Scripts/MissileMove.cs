using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour 
{
  public bool captain = false;

  [SerializeField]
  private Transform targetTransform;
  public Transform TargetTransform { set{ targetTransform = value;}}

  [SerializeField]
  private float kRatio;

  private Rigidbody rb;

  private Vector3 velocity;
  private Vector3 position;

  [SerializeField]
  private float timer;
  public float Timer { set {timer = value;}}

  [SerializeField]
  private GameObject bomb;

	// Use this for initialization
	void Start () 
  {
		rb = GetComponent<Rigidbody> ();
    position = transform.position;
    velocity = transform.forward * 150.0f * Time.deltaTime;

  if (captain)
      Invoke ("InstanceEffect", timer);
  
    Invoke ("DestroyMe", timer + 0.1f);
	}
	
	// Update is called once per frame
	void Update () 
  {
		MoveTowardTarget ();
	}

  void FixedUpdate ()
  {
    TurnTowardTarget ();
  }

  /* ---------- MoveTowardTarget () ----------
   * ターゲットの方向へ向かって移動する
   */
  private void MoveTowardTarget ()
  {
    Vector3 acceleration = Vector3.zero;

    Vector3 diff = targetTransform.position - position;
    acceleration += (diff - (velocity * timer)) * 2f / (timer * timer);

    timer -= Time.deltaTime;
    if (timer < 0.0f)
      return;

    velocity += acceleration * Time.deltaTime;
    position += velocity * Time.deltaTime;

    transform.position = position; 
  }

  /* ---------- TurnTowardTarget () ----------
   * ターゲットの方向へ向きを変える
   */
  private void TurnTowardTarget ()
  {
    // --- 自分の位置から目標位置へのベクトルを計算 ---
    Vector3 diff = targetTransform.position - this.transform.position;

    // --- 目標へ向かう角度を計算 ---
    Quaternion toTargetRot = Quaternion.LookRotation (diff);

    // --- 現在の角度の逆をかけることで今向いている方向からTarget方向へ向くための角度を求める ---
    // --- 要は自身のモデリング変換を打ち消してからTargetの方向を向かせるという2つの回転を合成するということ ---
    Quaternion tempToTarget = toTargetRot * Quaternion.Inverse (this.transform.rotation);

    // --- RigidBodyに角速度を加える ---
    Vector3 torque = new Vector3 (tempToTarget.x, tempToTarget.y, tempToTarget.z);
    rb.AddTorque (torque);
  }

  /* ---------- InstanceEffect () ----------
   *
   */
  private void InstanceEffect ()
  {
    Instantiate (bomb, transform.position, transform.rotation);
  }

  /* ---------- DestroyMe () ----------
   * 自分を消す
   */
  private void DestroyMe ()
  {
    Destroy (this.gameObject);
  }
}
