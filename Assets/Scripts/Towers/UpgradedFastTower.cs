using UnityEngine;
using System.Collections;

public class UpgradedFastTower : TowerController {
	protected override void Start () {
		base.Start();
		shootCooldown = 0.1f;
		attackDamage = 1f;
		requiredHits = 999f;
		requiredMaterials = 0f;
		isComplete = true;
		isBuilded = true;
	}
}
