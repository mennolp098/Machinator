using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	[SerializeField] private Texture2D hpBar;
	[SerializeField] private Texture2D hbBackground;

	private float enemyHealth;
	private float maxHealth = 0.0f;
	private Vector2 position;
	
	void Start() {
		enemyHealth = gameObject.GetComponentInParent<EnemyBehavior>().health;
		if (enemyHealth != 0) {
			maxHealth = enemyHealth;
		}
	}
	
	void Update() {
		position = Camera.main.WorldToScreenPoint(transform.position);
		enemyHealth = gameObject.GetComponentInParent<EnemyBehavior>().health;
	}
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(position.x - 40, (Screen.height - position.y) + (35) + 1, hbBackground.width / 7, hbBackground.height / 5), hbBackground);
		GUI.DrawTexture(new Rect(position.x - 40, ((Screen.height - position.y) + (35) + 1), (hpBar.width / 7) / (maxHealth / enemyHealth), hpBar.height / 5), hpBar);
	}
}