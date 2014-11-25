using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float destroyTime;
	public float speed;
	public GameObject hitParticle;
	public GameObject explosion;
	private bool canFreeze;
	private bool canExplode;
	private float damage;
	private Transform target;
	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, destroyTime);
	}
	public void SetDamage(float dmg)
	{
		damage = dmg;
	}
	public void SetTarget(Transform trgt)
	{
		target = trgt;
	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.transform == target)
		{
			Transform explosionsParent = GameObject.FindGameObjectWithTag("Explosions").transform;
			other.gameObject.GetComponent<EnemyBehavior>().GetDmg(damage);
			GameObject newhitParticle = Instantiate(hitParticle,transform.position,transform.rotation) as GameObject;
			newhitParticle.transform.parent = explosionsParent;
			if(canExplode)
			{
				GameObject newExplosion = Instantiate(explosion,transform.position,transform.rotation) as GameObject;
				newExplosion.transform.parent = explosionsParent;
			}
			else if(canFreeze)
			{
				other.gameObject.GetComponent<EnemyBehavior>().FreezeMe();
			}
			Destroy(this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(target != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
	}
	public void SetFreeze()
	{
		canFreeze = true;
	}
	public void SetExplosion()
	{
		canExplode = true;
	}
}
