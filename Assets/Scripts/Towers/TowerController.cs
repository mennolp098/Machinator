using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowerController : MonoBehaviour {
	protected float shootCooldown = 0f;
	protected float attackDamage = 0f;
	protected float rotationSpeed = 7f;
	protected float requiredMaterials = 0f;
	protected float requiredHits = 3f;
	protected bool isComplete = false;
	protected float totalHits = 0f;

	public Animator _animator;
	private List<EnemyBehavior> _enemyScripts = new List<EnemyBehavior>();
	private float _shootCoolDown = 0f;

	public GameObject bulletPrefab;
	public GameObject fireSmokePrefab;
	public Transform cannon;
	public Transform spawnpoint;
	void Update() 
	{
		if(isComplete)
		{
			if(_enemyScripts.Count != 0)
			{
				for(int i = 0; i < _enemyScripts.Count; i++)
				{
					if(_enemyScripts[0].thisTransform)
					{
						Vector3 relativePos = _enemyScripts[0].thisTransform.position - cannon.position;
						Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
						cannon.rotation = Quaternion.Slerp(cannon.rotation, enemyLookAt, Time.deltaTime * rotationSpeed);
						if (Time.time > _shootCoolDown) 
						{
							Shoot ();
						}
					}
					if(!_enemyScripts[i].isOnStage)
					{
						RemoveTarget(_enemyScripts[i]);
					}
				}
			}
			if(requiredHits <= totalHits)
			{
				requiredHits += 3f;
				Upgrade();
			}
		} else {
			if(requiredHits <= totalHits)
			{
				isComplete = true;
				requiredHits += 3f;
			}
		}
	}
	public void HitTurret()
	{
		Debug.Log("Building turret");
		MaterialHandler currentMaterialScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MaterialHandler>();
		float currentMaterials = currentMaterialScript.GetMaterials();
		if(currentMaterials >= requiredMaterials)
		{
			currentMaterialScript.AddMaterials(-requiredMaterials);
			totalHits += 1;
		}
	}
	protected virtual void Upgrade()
	{
	}
	public float GetAttackDamage(){
		return attackDamage;
	}
	public float GetRequiredHits(){
		return requiredHits;
	}
	public float GetRequiredMaterials(){
		return requiredMaterials;
	}
	public void GetAttackDamage(float newAttackDamage){
		attackDamage = newAttackDamage;
	}
	public void RemoveTarget(EnemyBehavior script)
	{
		_enemyScripts.Remove(script);
		_enemyScripts.Sort();
	}
	void OnTriggerEnter(Collider other) 
	{
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(other.transform.tag == "Enemy")
		{
			_enemyScripts.Add(enemyScript);
			_enemyScripts.Sort();
		}
	}
	void OnTriggerExit(Collider other) 
	{
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(_enemyScripts.Contains(enemyScript))
		{
			_enemyScripts.Remove(enemyScript);
			_enemyScripts.Sort();
		}
	}
	void Shoot() 
	{
		_shootCoolDown = Time.time + shootCooldown;
		Instantiate(fireSmokePrefab, spawnpoint.position,spawnpoint.rotation);

		GameObject newBullet = Instantiate (bulletPrefab, spawnpoint.position, spawnpoint.rotation) as GameObject;
		newBullet.transform.parent = GameObject.FindGameObjectWithTag("Bullets").transform;
		newBullet.GetComponent<BulletController>().SetDamage(attackDamage);

		_animator.SetTrigger("shoot");
		
	}
	public void AddDamage(float damage)
	{
		attackDamage += damage;
	}
	public void LowerCooldown(float cooldown)
	{
		shootCooldown -= cooldown;
	}
}
