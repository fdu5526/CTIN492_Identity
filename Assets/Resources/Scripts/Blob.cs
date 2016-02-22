using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Collider2D))]
//[RequireComponent (typeof (Joint2D))]
public class Blob : MonoBehaviour {

	Rigidbody2D rigidbody2d;
	Collider2D collider2d;
	Joint2D joint2d;

	// Use this for initialization
	void Start () {
		rigidbody2d = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
		joint2d = GetComponent<Joint2D>();
	}


	void OnMouseDrag () {
	 	Vector3 v3 = Input.mousePosition;
 		v3.z = 10f;
 		v3 = Camera.main.ScreenToWorldPoint(v3);
		rigidbody2d.velocity = ((Vector2)v3 - (Vector2)rigidbody2d.position) * 5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
