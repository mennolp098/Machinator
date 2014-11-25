using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowerController : MonoBehaviour {
	public Animator animator;
	public GameObject bulletPrefab;
	public GameObject fireSmokePrefab;
	public GameObject upgradePrefab;
	public Transform cannon;
	public Transform spawnpoint;
	public AudioClip[] sounds = new AudioClip[0];

	protected float shootCooldown = 0f;
	protected float attackDamage = 0f;
	protected float rotationSpeed = 7f;
	protected float requiredMaterials = 0f;
	protected float requiredHits = 3f;
	protected float totalHits = 0f;
	protected float totalUpgrades = 0;
	protected bool isComplete = false;
	protected bool isBuilded = false;
	protected bool canFreeze = false;
	protected bool canExplode = false;
	protected List<Material> allChildrenMaterials = new List<Material>();

	private List<EnemyBehavior> _enemyScripts = new List<EnemyBehavior>();
	private float _shootCoolDown = 0f;
	
	protected virtual void Start()
	{
		audio.clip = sounds[0];
		audio.Play();
		audio.loop = true;
		Renderer[] allChildrenRenderers = GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in allChildrenRenderers)
		{
			allChildrenMaterials.Add(renderer.material);
		}
	}
	void Update() 
	{
		if(!isComplete)
		{
			if(isBuilded)
			{
				if(cannon.eulerAngles.x > 0f)
				{
					Vector3 cannonRot = cannon.eulerAngles;
					cannonRot.x = 0;
					cannon.eulerAngles = Vector3.Slerp(cannon.eulerAngles, cannonRot, 7f * Time.deltaTime);
				} else {
					isComplete = true;
					audio.Stop();
					audio.loop = false;
				}
			} 
			else if(cannon.eulerAngles.x <= 30f)
			{
				Vector3 cannonRot = cannon.eulerAngles;
				cannonRot.x = 30;
				cannon.eulerAngles = Vector3.Slerp(cannon.eulerAngles, cannonRot, 7f * Time.deltaTime);
			}
			if(requiredHits <= totalHits)
			{
				isBuilded = true;
				requiredHits += 3f;
			} 
		}
		else
		{
			//check if enemys are in the list to attack
			if(_enemyScripts.Count != 0)
			{
				for(int i = 0; i < _enemyScripts.Count; i++)
				{
					//check first enemy in list
					if(_enemyScripts[0].thisTransform)
					{
						Vector3 relativePos = _enemyScripts[0].thisTransform.position - cannon.position;
						Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
						//check rotation relative to the pos to slerp towards enemypos
						cannon.rotation = Quaternion.Slerp(cannon.rotation, enemyLookAt, Time.deltaTime * rotationSpeed);
						if (Time.time > _shootCoolDown) 
						{
							Shoot ();
						}
					}
					//if enemy is not onstage remove out of list
					if(!_enemyScripts[i].isOnStage)
					{
						RemoveTarget(_enemyScripts[i]);
					}
				}
			}
			//check if possible to upgrade
			if(totalUpgrades != 3)
			{
				if(requiredHits <= totalHits)
				{
					requiredHits += 3f;
					Upgrade();
				}
			}
		}
	}
	public void HitTurret()
	{
		//hit te turret to add total hits
		MaterialHandler currentMaterialScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MaterialHandler>();
		float currentMaterials = currentMaterialScript.GetMaterials();
		if(currentMaterials >= requiredMaterials && totalUpgrades != 3)
		{
			currentMaterialScript.AddMaterials(-requiredMaterials);
			totalHits += 1;
		}
	}
	//upgrade function to upgrade the tower
	protected virtual void Upgrade()
	{
		if(totalUpgrades == 3)
			BecomeSuper();
		Instantiate(upgradePrefab,transform.position,Quaternion.identity);
	}
	protected virtual void BecomeSuper()
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
		//add enemys in list while they enter the trigger
		EnemyBehavior enemyScript = other.GetComponent<EnemyBehavior> ();
		if(other.transform.tag == "Enemy")
		{
			_enemyScripts.Add(enemyScript);
			_enemyScripts.Sort();
		}
	}
	void OnTriggerExit(Collider other) 
	{
		//remove enemys in list while they exit the trigger
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
		BulletController newBulletScript = newBullet.GetComponent<BulletController>();
		newBulletScript.SetDamage(attackDamage);
		newBulletScript.SetTarget(_enemyScripts[0].thisTransform);
		if(canFreeze)
			newBulletScript.SetFreeze();
		if(canExplode)
			newBulletScript.SetExplosion();

		animator.SetTrigger("shoot");
		audio.clip = sounds[1];
		audio.Play();
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
