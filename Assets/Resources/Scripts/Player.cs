using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Collider2D))]
public class Player : MonoBehaviour {

	float speed = 4f;
	string[] inputStrings = {"w", "a", "s", "d"};
	bool[] inputs;

	bool disabled;
	Rigidbody2D rigidbody2d;
	Collider2D collider2d;

	// Use this for initialization
	void Awake () {
		inputs = new bool[inputStrings.Length];
		disabled = false;
		rigidbody2d = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
	}

	// given vector, change facing direction to that way
	void FaceDirection (Vector2 d) {
		float origZ = rigidbody2d.rotation;
		float targetZ = Global.Angle(Vector2.down, d);
  	rigidbody2d.rotation = Mathf.LerpAngle(origZ, targetZ, 0.3f);
	}


	void FixedUpdate () {
		if (!disabled) {
			float dx = 0f;
			float dy = 0f;
			if (inputs[0]) {
				dy = speed;
			} else if (inputs[2]) {
				dy = -speed;
			}

			if (inputs[1]) {
				dx = -speed;
			} else if (inputs[3]) {
				dx = speed;
			}
			rigidbody2d.velocity = new Vector2(dx, dy);
			if (rigidbody2d.velocity.sqrMagnitude > 0.2f) {
				FaceDirection(rigidbody2d.velocity);
			}
			
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if (!disabled) {
			for (int i = 0; i < inputs.Length; i++) {
				inputs[i] = Input.GetKey(inputStrings[i]);
			}
		}
		
	}
}
