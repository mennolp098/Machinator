using UnityEngine;
using System.Collections;

public class FastTower : TowerController {

	protected override void Start () {
		base.Start();
		shootCooldown = 0.25f;
		attackDamage = 2f;
		requiredHits = 2f;
		requiredMaterials = 150f;
	}
	protected override void Upgrade ()
	{
		totalUpgrades++;
		requiredMaterials += 50f;
		attackDamage += 3f;
		shootCooldown -= 0.05f;
		base.Upgrade ();
	}
	protected override void BecomeSuper ()
	{
		base.BecomeSuper ();
	}
}
