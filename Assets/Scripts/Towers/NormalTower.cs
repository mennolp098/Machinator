using UnityEngine;
using System.Collections;

public class NormalTower : TowerController {

	// Use this for initialization
	protected override void Start () {
		base.Start();
		shootCooldown = 0.5f;
		attackDamage = 3f;
		requiredHits = 3f;
		requiredMaterials = 100f;
	}
	protected override void Upgrade ()
	{
		totalUpgrades++;
		requiredMaterials += 50f;
		attackDamage += 0.5f;
		shootCooldown -= 0.05f;
		base.Upgrade ();
	}
	protected override void BecomeSuper ()
	{
		base.BecomeSuper ();
		foreach(Material material in allChildrenMaterials)
		{
			Color newColor = material.color;
			newColor.b += 0.5f;
			material.color = newColor;
		}
		canFreeze = true;
	}
}
