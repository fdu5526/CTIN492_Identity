using UnityEngine;
using System.Collections;

public class PowerUp : Physics2DBody {
	

	public enum PowerUpType { Speed, Mass };
	public PowerUpType type;
	public float amount;


	Player player;

	// Use this for initialization
	void Awake () {
		player = GameObject.Find("Player").GetComponent<Player>();
		base.Awake();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		switch (type) {
			case PowerUpType.Speed:
				player.Speed += amount;
				break;
			case PowerUpType.Mass:
				player.Mass += amount / 2f;
				break;
		}
		player.SetNewColor(GetComponent<SpriteRenderer>().color);
		this.collider2d.enabled = false;
		this.GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<AudioSource>().Play();
	}
}
