using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyBehavior : MonoBehaviour, IComparable<EnemyBehavior> {
	public bool isOnStage;
	public float health = 10;
	public Transform thisTransform;
	public int sort;
	public bool isDead;
	public GameObject greenBar;
	public GameObject redBar;


	protected float _speed = 0.03f;
	protected float _myMaterials = 0;

	private GameObject target;
	private float counter = 1;
	private DateTime TimeAdded;
	private bool isFreezed;
    private NavMeshAgent _navMesh;
	private float oldspeed;

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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Base" && !isDead)
        {
            collision.gameObject.GetComponent<FortScript>().hit();
			Destroy(this.gameObject);
			isOnStage = false;
        }
    }
	public void FreezeMe()
	{
		if(!isFreezed)
		{
			isFreezed = true;
			_navMesh.speed -= 0.25f;
			Invoke("StopFreeze", 2f);
		}
	}
	public void StopFreeze()
	{
		if(!isDead)
		{
			_navMesh.speed = oldspeed;
		}
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
		Destroy(greenBar.gameObject);
		Destroy(redBar.gameObject);
		Destroy(this.gameObject, 4f);
		isOnStage = false;
	}
}
