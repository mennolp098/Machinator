using UnityEngine;
using System.Collections;

public class FastEnemy : EnemyBehavior {

	// Use this for initialization
	protected override void Start () {
		_speed = 1f;
		_myMaterials = 50f;
		health = 6;
		sort = 1;
		base.Start();
	}
}
