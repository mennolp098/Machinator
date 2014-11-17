using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowerController : MonoBehaviour {
	public float shootCooldown = 1.0f;
	public float attackDamage = 10.0f;
	public float rotationSpeed;

	private List<EnemyBehavior> enemyScripts = new List<EnemyBehavior>();
	private float shootCoolDown = 0f;

	public GameObject bullet;
	public Transform spawnpoint;
	void Update() 
	{
		if(enemyScripts.Count != 0)
		{
			for(int i = 0; i < enemyScripts.Count; i++)
			{
				if(enemyScripts[0].thisTransform)
				{
					Vector3 relativePos = enemyScripts[0].thisTransform.position - transform.position;
					Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
					this.transform.rotation = Quaternion.Slerp(transform.rotation, enemyLookAt, Time.deltaTime * rotationSpeed);
					if (Time.time > shootCoolDown) 
					{
						Shoot ();
					}
				}
				if(!enemyScripts[i].isOnStage)
				{
					removeTarget(enemyScripts[i]);
				}
			}
		}
	}

	public void removeTarget(EnemyBehavior script)
	{
		enemyScripts.Remove(script);
		enemyScripts.Sort();
	}
	void OnTriggerEnter(Collider other) 
	{
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(other.transform.tag == "Enemy")
		{
			enemyScripts.Add(enemyScript);
			enemyScripts.Sort();
		}
	}
	void OnTriggerExit(Collider other) 
	{
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(enemyScripts.Contains(enemyScript))
		{
			enemyScripts.Remove(enemyScript);
			enemyScripts.Sort();
		}
	}
	void Shoot() 
	{
		shootCoolDown = Time.time + shootCooldown;
		GameObject newBullet = Instantiate (bullet, spawnpoint.position, spawnpoint.rotation) as GameObject;
		newBullet.transform.parent = GameObject.FindGameObjectWithTag("Bullets").transform;
	}
}
