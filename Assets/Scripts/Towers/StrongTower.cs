using UnityEngine;
using System.Collections;

public class StrongTower : TowerController {
	void Start () {
		shootCooldown = 1f;
		attackDamage = 6f;
		requiredHits = 4f;
		requiredMaterials = 120f;
	}
	protected override void Upgrade ()
	{
		base.Upgrade ();
		requiredMaterials += 75f;
		attackDamage += 5f;
		shootCooldown -= 0.05f;
	}
}
