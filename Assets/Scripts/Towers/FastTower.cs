using UnityEngine;
using System.Collections;

public class FastTower : TowerController {
	public GameObject upgradedPrefab;
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
		attackDamage += 0.5f;
		shootCooldown -= 0.05f;
		base.Upgrade ();
	}
	protected override void BecomeSuper ()
	{
		base.BecomeSuper ();
		Instantiate(upgradedPrefab,transform.position,transform.rotation);
		Destroy(this.gameObject);
	}
}
