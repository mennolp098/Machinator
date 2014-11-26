using UnityEngine;
using System.Collections;

public class FastEnemy : EnemyBehavior {

	// Use this for initialization
	protected override void Start () {
		_speed = 1f;
		_myMaterials = 50f;
		sort = 1;
		base.Start();
	}
}
