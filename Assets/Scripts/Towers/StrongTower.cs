using UnityEngine;
using System.Collections;

public class StrongTower : TowerController {
	protected override void Start () {
		base.Start();
		shootCooldown = 1f;
		attackDamage = 6f;
		requiredHits = 4f;
		requiredMaterials = 120f;
	}
	protected override void Upgrade ()
	{
		totalUpgrades++;
		requiredMaterials += 75f;
		attackDamage += 5f;
		shootCooldown -= 0.05f;
		base.Upgrade ();
	}
	protected override void BecomeSuper ()
	{
		base.BecomeSuper ();
		foreach(Material material in allChildrenMaterials)
		{
			Color newColor = material.color;
			newColor.r += 0.5f;
			material.color = newColor;
		}
		canExplode = true;
	}
}
