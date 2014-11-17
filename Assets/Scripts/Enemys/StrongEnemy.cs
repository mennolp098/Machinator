using UnityEngine;
using System.Collections;

public class StrongEnemy : EnemyBehavior {

	// Use this for initialization
	protected override void Start () {
		base.Start();
		health = 20;
		speed = 0.03f;
		sort = 2;
	}
}
