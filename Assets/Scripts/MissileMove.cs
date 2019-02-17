using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour 
{

  [SerializeField]
  private Transform targetTransform;
  public Transform TargetTransform { set{ targetTransform = value;}}

  [SerializeField]
  private float kRatio;

  [SerializeField, Range(1000, 2000)]
  private float speed;

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
    velocity = transform.forward * 10.0f * Time.deltaTime;

    timer = 5.0f; //寿命

    Invoke ("InstanceEffect", timer);
  
    Destroy (this.gameObject , timer + 0.1f);
	}
	
	// Update is called once per frame
	void Update () 
  {
		//MoveTowardTarget ();
    CollisionCheck ();
	}

  void FixedUpdate ()
  {
    TurnTowardTarget ();
  }

  /* ---------- MoveTowardTarget () ----------
   * ターゲットの方向へ向かって移動する
   * 着弾時刻を指定して絶対当たるミサイル
   */
  private void MoveTowardTarget ()
  {
    // --- Targetが存在しなければ爆発して消える ---
    if (targetTransform == null)
    {
      InstanceEffect ();

      Destroy (this.gameObject);
      return;
    }

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
    rb.velocity = transform.forward * speed * Time.deltaTime;

    //  --- ターゲットがなければ直進し続ける ---
    if (targetTransform == null)
      return;

    // --- 自分の位置から目標位置へのベクトルを計算 ---
    Vector3 diff = targetTransform.position - this.transform.position;

    // --- 目標へ向かう角度を計算 ---
    Quaternion toTargetRot = Quaternion.LookRotation (diff);

    // --- 現在の角度の逆をかけることで今向いている方向からTarget方向へ向くための角度を求める ---
    // --- 要は自身のモデリング変換を打ち消してからTargetの方向を向かせるという2つの回転を合成するということ ---
    Quaternion tempToTarget = toTargetRot * Quaternion.Inverse (this.transform.rotation);

    // --- RigidBodyに角速度を加える ---
    Vector3 torque = new Vector3 (tempToTarget.x, tempToTarget.y, tempToTarget.z) * kRatio;
    rb.AddTorque (torque);
  }


  /* ---------- CollisionCheck () ----------
   * ターゲットに当たったかどうかを判定する
   */
  private void CollisionCheck ()
  {
    if (targetTransform != null)
    {
      Vector3 diff = targetTransform.position - transform.position;
      float length = diff.magnitude;

      if (length < 3.0f)
      {
        InstanceEffect ();
        DestroyMe ();
      }
    }
  }

  /* ---------- InstanceEffect () ----------
   *
   */
  private void InstanceEffect ()
  {
    Instantiate (bomb, transform.position, transform.rotation);
  }

  /* ---------- DestroyMe () ----------
   * 自分とターゲットを消す
   */
  private void DestroyMe ()
  {
    if (targetTransform != null)
      Destroy (targetTransform.gameObject);

    Destroy (this.gameObject);
  }
}
