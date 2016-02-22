using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Joint2D))]
public class Blob : Physics2DBody {

	Joint2D joint2d;

	bool isBroken;
	GameObject player;
	AudioSource[] audios;

	// Use this for initialization
	protected override void Awake () {
		player = GameObject.Find("Player");
		joint2d = GetComponent<Joint2D>();
		isBroken = false;
		audios = GetComponents<AudioSource>();
		base.Awake();
	}

	
	void OnMouseDrag () {
	 	Vector3 v3 = Input.mousePosition;
 		v3.z = 10f;
 		v3 = Camera.main.ScreenToWorldPoint(v3);
		rigidbody2d.velocity = ((Vector2)v3 - (Vector2)rigidbody2d.position) * 7f;
	}

	// given vector, change facing direction to that way
	void FaceDirection (Vector2 d) {
		float origZ = rigidbody2d.rotation;
		float targetZ = Global.Angle(Vector2.down, d);
  	rigidbody2d.rotation = Mathf.LerpAngle(origZ, targetZ, 0.3f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 d = player.transform.position - transform.position;
		FaceDirection(d);

		// broken
		if (joint2d == null && !isBroken) {
			isBroken = true;
			audios[(int)UnityEngine.Random.Range(0, audios.Length)].Play();
		}
	}
}
