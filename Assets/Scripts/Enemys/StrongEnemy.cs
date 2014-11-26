using UnityEngine;
using System.Collections;

public class StrongEnemy : EnemyBehavior {

	// Use this for initialization
	protected override void Start () {
		_myMaterials = 150f;
		_speed = 0;
		sort = 2;
		base.Start();
	}
}
