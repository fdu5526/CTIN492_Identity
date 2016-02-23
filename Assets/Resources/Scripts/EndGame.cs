using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGame : MonoBehaviour {

	bool ended;
	public CanvasGroup endText;

	// Use this for initialization
	void Start () {
		ended = false;
	}

	void OnTriggerEnter2D (Collider2D other) {
		Player p = other.GetComponent<Player>();
		if (p != null && !p.Disabled) {
			// TODO win
			p.Disabled = true;
			ended = true;
		}
	}


	void FixedUpdate () {
		if (ended) {
			endText.alpha += 0.01f;
		}
	}
}
