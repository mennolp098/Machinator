using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float destroyTime;
	public float speed;
	public GameObject explosion;
	private float damage;
	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, destroyTime);
	}
	public void SetDamage(float dmg)
	{
		damage = dmg;
	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Enemy")
		{
			Transform explosionParent = GameObject.FindGameObjectWithTag("Explosions").transform;
			other.gameObject.GetComponent<EnemyBehavior>().GetDmg(damage);
			GameObject newExplosion = Instantiate(explosion,transform.position,transform.rotation) as GameObject;
			newExplosion.transform.parent = explosionParent;
			Destroy(this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}
}
