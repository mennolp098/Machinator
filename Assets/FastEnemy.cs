using UnityEngine;
using System.Collections;

public class FastEnemy : EnemyBehavior {

	// Use this for initialization
	protected override void Start () {
		base.Start();
		speed = 0.03f;
		health = 6;
		sort = 1;
	}
}
