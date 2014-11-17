using UnityEngine;
using System.Collections;

public class NormalTower : TowerController {

	// Use this for initialization
	void Start () {
		shootCooldown = 0.5f;
		attackDamage = 3f;
		requiredHits = 3f;
		requiredMaterials = 100f;
	}
}
