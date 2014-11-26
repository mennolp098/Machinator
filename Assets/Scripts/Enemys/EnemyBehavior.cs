using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyBehavior : MonoBehaviour, IComparable<EnemyBehavior> {
	public bool isOnStage;
	public float health;
	public Transform thisTransform;
	public int sort;
	public GameObject greenbar;
	public GameObject redbar;

	protected float _speed = 0.03f;
	protected float _myMaterials = 0;
	protected List<Material> allChildrenMaterials = new List<Material>();

	private GameObject target;
	private DateTime TimeAdded;
	private NavMeshAgent _navMesh;
	private float counter = 1;
	private float oldspeed;
	private bool isFreezed;
	private bool isDead;
   
	public int CompareTo(EnemyBehavior other)
	{
		if(this.health < other.health)
		{
			return this.health.CompareTo(other.health);
		} 
		else
		{
			if(other.sort == this.sort)
			{
				return this.TimeAdded.CompareTo(other.TimeAdded);
			}
			return other.sort.CompareTo(this.sort);
		}
	}

	protected virtual void Start () {
		target = GameObject.Find ("Waypoint-1");
		thisTransform = this.transform;
		TimeAdded = DateTime.Now;
		isOnStage = true;
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.SetDestination(target.transform.position);
		_navMesh.speed += _speed;
		oldspeed = _navMesh.speed;
		Renderer[] allChildrenRenderers = GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in allChildrenRenderers)
		{
			allChildrenMaterials.Add(renderer.material);
		}
	}
	void Update () {
		if(target && !isDead)
		{
			if(Vector2.Distance (new Vector2(transform.position.x,transform.position.z), new Vector2(target.transform.position.x,target.transform.position.z)) < 1.5f)
			{
				counter++;
				var newWaypointName = "Waypoint-" + counter;
				GameObject newWaypoint = GameObject.Find(newWaypointName);
				target = newWaypoint;
                
				if(target == null)
				{
					Debug.LogWarning("no waypoints found!");
				}
                else
                {
                    _navMesh.SetDestination(target.transform.position);
                }
			}
		} else if(transform.position.y >= 0.5f) {
			transform.Translate(Vector3.down * 4 * Time.deltaTime);
			transform.Rotate(Vector3.right * 50 * Time.deltaTime);
		}
	}
    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Base" && !isDead)
        {
			other.gameObject.GetComponent<FortScript>().hit();
			Destroy(this.gameObject);
			isOnStage = false;
        }
    }
	public void FreezeMe()
	{
		if(!isFreezed)
		{
			isFreezed = true;
			_navMesh.speed -= 0.5f;
			Invoke("StopFreeze", 2f);
			foreach(Material material in allChildrenMaterials)
			{
				Color newColor = material.color;
				newColor.b += 1f;
				material.color = newColor;
			}
		}
	}
	public void StopFreeze()
	{
		if(!isDead)
		{
			_navMesh.speed = oldspeed;
			foreach(Material material in allChildrenMaterials)
			{
				Color newColor = material.color;
				newColor.b -= 1f;
				material.color = newColor;
			}
		}
	}
	public void SetHealth(float newHealth)
	{
		health = newHealth;
	}
	public void GetDmg(float dmg)
	{
		health -= dmg;
		if(health <= 0)
		{
			Die();
		}
	}
	private void Die()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<MaterialHandler>().AddMaterials(_myMaterials);
		audio.Play();
		isDead = true;
		Destroy(this.rigidbody);
		Destroy(_navMesh);
		Destroy(greenbar.gameObject);
		Destroy(redbar.gameObject);
		Destroy(this.gameObject, 4f);
		isOnStage = false;
	}
}
