using UnityEngine;
using System.Collections;

public class OtherBlob : Physics2DBody {
	
	float speed;
	float angularV = 100f;

	GameObject player;

	// Use this for initialization
	protected override void Awake () {
		speed = UnityEngine.Random.Range(1f, 3f);
		player = GameObject.Find("PlayerSystem/Player");
		base.Awake();

		Transform[] t = transform.GetComponentsInChildren<Transform>();
		int deleteCount = (int)UnityEngine.Random.Range(0f, 3f);
		for (int i = 1; i < deleteCount + 1; i++) {
			Destroy(t[i].gameObject);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2d.velocity = Vector2.right * speed;
		rigidbody2d.angularVelocity = angularV;

		if (Mathf.Abs(player.transform.position.x - transform.position.x) > 30f) {
			Destroy(this.gameObject);
		}
	}
}
