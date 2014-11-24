using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

	public GameObject[] buildTowers = new GameObject[0];
	public Transform spawnPoint;

	public GameObject[] allTowers = new GameObject[0];

	private int _towerToBuild;
	private bool _isBuilding = false;
	private bool _isSwinging = false;
	private GameObject _currentTower;

	void Update () {
		if(Input.GetMouseButton(0) && !_isBuilding && !_isSwinging)
		{
			GetComponentInChildren<Animation>().Play();
			_isSwinging = true;
			RaycastHit hit;
			Ray ray;
			
			ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.transform.tag == "Tower")
				{
					if(hit.distance <= 2f)
					{
						hit.transform.GetComponent<TowerController>().HitTurret();
					}
				}
			}
			Invoke("StopSwing", 1f);
		} else if(Input.GetMouseButtonDown(0) && _isBuilding)
		{
			if(_currentTower.GetComponent<BuildTowerBehavior>().buildAble)
			{
				Vector3 spawnPos = _currentTower.transform.position;
				spawnPos.y = 0.5f;
				GameObject newTower = Instantiate(allTowers[_towerToBuild], spawnPos,_currentTower.transform.rotation) as GameObject;
				GameObject hierachyTowers = GameObject.FindGameObjectWithTag("AllTowers");
				newTower.transform.parent = hierachyTowers.transform;
				Destroy(_currentTower.gameObject);
				_isBuilding = false;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			BuildTower(0);
		} else if(Input.GetKeyDown(KeyCode.Alpha2)) 
		{
			BuildTower(1);
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) 
		{
			BuildTower(2);
		} else if(Input.GetKeyDown(KeyCode.Alpha4)) 
		{
			BuildTower(3);
		}
		if(_isBuilding)
		{
			Vector3 newSpawnPointPos = spawnPoint.position;
			newSpawnPointPos.y = 0.5f;
			Quaternion newRot = spawnPoint.rotation;
			newRot.z = 0;
			newRot.x = 0;
			_currentTower.transform.position = newSpawnPointPos;
			_currentTower.transform.rotation = newRot;
		}
	}
	private void StopSwing()
	{
		_isSwinging = false;
	}
	private void BuildTower(int towerSort)
	{
		_isBuilding = true;
		_towerToBuild = towerSort;
		if(_currentTower == null)
		{
			_currentTower = Instantiate(buildTowers[_towerToBuild], Vector3.zero, Quaternion.identity) as GameObject;
		} else {
			Destroy(_currentTower.gameObject);
			_currentTower = Instantiate(buildTowers[_towerToBuild], Vector3.zero, Quaternion.identity) as GameObject;
		}
	}
}
