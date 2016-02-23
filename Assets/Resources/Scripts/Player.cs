using UnityEngine;
using System.Collections;

public class Player : Physics2DBody {

	float speed = 4f;
	float boostSpeed = 10f;
	Timer boostTimer;
	Timer boostCooldownTimer;

	Color[] colors;
	int colorIndex;
	int setColorIndex;
	Timer colorTimer;

	AudioSource[] audios;

	string[] inputStrings = {"w", "a", "s", "d", "space"};
	bool[] inputs;

	bool disabled;
	// Use this for initialization
	protected override void Awake () {
		inputs = new bool[inputStrings.Length];
		disabled = false;

		colors = new Color[5];
		for (int i = 0; i < colors.Length; i++) {
			colors[i] = GetComponent<SpriteRenderer>().color;
		}
		colorIndex = 0;
		setColorIndex = 0;
		colorTimer = new Timer(0.2f);

		boostTimer = new Timer(0.2f);
		boostCooldownTimer = new Timer(0.8f);

		audios = GetComponents<AudioSource>();

		base.Awake();
	}

	public void SetNewColor (Color c) {
		colors[setColorIndex] = c;
		setColorIndex = Mathf.Min(setColorIndex + 1, colors.Length - 1);
	}

	public float Speed {
		get { return speed; }
		set { 
			boostSpeed = Mathf.Max(boostSpeed, value);
			speed = value; 
		}
	}

	public float BoostSpeed {
		get { return boostSpeed; }
		set { boostSpeed = value; }
	}

	public float Mass {
		get { return rigidbody2d.mass; }
		set { rigidbody2d.mass = value; }
	}

	public bool Disabled {
		get { return disabled; }
		set { 
			disabled = value; 
			rigidbody2d.velocity = Vector2.zero;
		}
	}

	public void AddOrb (int amount) {
		for (int i = 0; i < amount; i++) {
			GameObject g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Blob"));
			g.transform.position = transform.position;
			g.GetComponent<Joint2D>().connectedBody = rigidbody2d;
		}
	}

	// given vector, change facing direction to that way
	void FaceDirection (Vector2 d) {
		float origZ = rigidbody2d.rotation;
		float targetZ = Global.Angle(Vector2.down, d);
  	rigidbody2d.rotation = Mathf.LerpAngle(origZ, targetZ, 0.3f);
	}


	void OnCollisionEnter2D (Collision2D coll) {
		int i = (int)UnityEngine.Random.Range(1, 4);

		audios[i].volume = (coll.relativeVelocity.magnitude) / 10f;
		audios[i].Play();

	}


	void FixedUpdate () {
		if (!disabled) {
			float dx = 0f;
			float dy = 0f;

			// boost
			if (inputs[4] && boostCooldownTimer.IsOffCooldown) {
				boostTimer.Reset();
				boostCooldownTimer.Reset();
				audios[0].Play();
			}

			float s = boostTimer.IsOffCooldown ? speed : boostSpeed;
			//s = boostSpeed;
			if (inputs[0]) {
				dy = s;
			} else if (inputs[2]) {
				dy = -s;
			}

			if (inputs[1]) {
				dx = -s;
			} else if (inputs[3]) {
				dx = s;
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
