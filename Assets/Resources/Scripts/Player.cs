﻿using UnityEngine;
using System.Collections;

public class Player : Physics2DBody {

	float speed = 4f;
	float boostSpeed = 4f;

	Color[] colors;
	int colorIndex;
	int setColorIndex;
	Timer colorTimer;

	string[] inputStrings = {"w", "a", "s", "d"};
	bool[] inputs;

	bool disabled;
	// Use this for initialization
	protected override void Awake () {
		inputs = new bool[inputStrings.Length];
		disabled = false;

		colors = new Color[10];
		for (int i = 0; i < colors.Length; i++) {
			colors[i] = GetComponent<SpriteRenderer>().color;
		}
		colorIndex = 0;
		setColorIndex = 0;
		colorTimer = new Timer(0.1f);

		base.Awake();
	}

	public void SetNewColor (Color c) {
		colors[setColorIndex] = c;
		setColorIndex = Mathf.Min(setColorIndex + 1, colors.Length - 1);
	}

	public float Speed {
		get { return speed; }
		set {speed = value; }
	}

	public float Mass {
		get { return rigidbody2d.mass; }
		set { rigidbody2d.mass = value; }
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


			if (colorTimer.IsOffCooldown) {
				colorTimer.Reset();
				GetComponent<SpriteRenderer>().color = colors[colorIndex];
				colorIndex++;
				colorIndex = colorIndex == colors.Length ? 0 : colorIndex;

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
