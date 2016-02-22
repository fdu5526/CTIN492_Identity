using UnityEngine;
using System.Collections;

public class OtherBlob : Physics2DBody {
	
	float speed;
	float angularV = 100f;

	// Use this for initialization
	protected override void Awake () {
		speed = UnityEngine.Random.Range(1f, 3f);
		base.Awake();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2d.velocity = Vector2.right * speed;
		rigidbody2d.angularVelocity = angularV;
	}
}
