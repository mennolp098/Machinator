using UnityEngine;
using System.Collections;

public class NormalEnemy : EnemyBehavior {

	// Use this for initialization
	protected override void Start () {
		base.Start();
		_speed = 0.03f;
		_myMaterials = 100f;
		health = 10;
		sort = 3;
	}
}
