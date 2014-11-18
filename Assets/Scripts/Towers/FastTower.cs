using UnityEngine;
using System.Collections;

public class FastTower : TowerController {

	void Start () {
		shootCooldown = 0.25f;
		attackDamage = 2f;
		requiredHits = 2f;
		requiredMaterials = 150f;
	}
	protected override void Upgrade ()
	{
		base.Upgrade ();
		requiredMaterials += 50f;
		attackDamage += 3f;
		shootCooldown -= 0.05f;
	}
}
