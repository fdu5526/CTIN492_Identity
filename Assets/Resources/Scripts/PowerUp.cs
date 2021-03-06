using UnityEngine;
using System.Collections;

public class PowerUp : Physics2DBody {
	

	public enum PowerUpType { Speed, Mass, BoostSpeed, OrbCount };
	public PowerUpType type;
	public float amount;


	Player player;

	// Use this for initialization
	protected override void Awake () {
		player = GameObject.Find("PlayerSystem/Player").GetComponent<Player>();
		base.Awake();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		switch (type) {
			case PowerUpType.Speed:
				player.Speed += amount;
				break;
			case PowerUpType.Mass:
				player.Mass += amount;
				break;
			case PowerUpType.BoostSpeed:
				player.BoostSpeed += amount;
				break;
			case PowerUpType.OrbCount:
				player.AddOrb((int)	amount);
				break;
		}
		player.SetNewColor(GetComponent<SpriteRenderer>().color);
		this.collider2d.enabled = false;
		this.GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<AudioSource>().Play();
	}
}
