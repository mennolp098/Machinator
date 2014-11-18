using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyBehavior : MonoBehaviour, IComparable<EnemyBehavior> {
	protected float speed = 0.03f;
	private GameObject target;
	private float counter = 1;
	private DateTime TimeAdded;

	public bool isOnStage;
	public float health = 10;
	public Transform thisTransform;
	public int sort;

    private NavMeshAgent _navMesh;
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
	}
	void Update () {
		if(target)
		{
			if(Vector3.Distance (this.transform.position, target.transform.position) < 1.5f)
			{
				counter++;
				var newWaypointName = "Waypoint-" + counter;
				GameObject newWaypoint = GameObject.Find(newWaypointName);
				target = newWaypoint;
                
				if(target == null)
				{
					GetDmg(1000);
				}
                else
                {
                    _navMesh.SetDestination(target.transform.position);
                }
			}
		}
	}
	public void FreezeMe()
	{
		//TODO: NavMesh Acceleration slow
	}
	public void GetDmg(float dmg)
	{
		health -= dmg;
		if(health <= 0)
		{
			Destroy(this.gameObject);
			isOnStage = false;
		}
	}
}
