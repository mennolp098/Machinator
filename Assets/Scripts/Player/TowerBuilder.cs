using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

	public GameObject buildTower;
	public Transform spawnPoint;

	public GameObject[] allTowers = new GameObject[0];

	private int _towerToBuild;
	private bool _isBuilding = false;
	private bool _isSwinging = false;
	private GameObject _currentTower;

	void Update () {
		if(Input.GetMouseButton(0) && !_isBuilding && !_isSwinging)
		{
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
						Debug.Log("Hitting tower");
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
				Instantiate(allTowers[_towerToBuild], spawnPos,_currentTower.transform.rotation);
				Destroy(_currentTower.gameObject);
				_isBuilding = false;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha1) && !_isBuilding)
		{
			BuildTower(0);
		} else if(Input.GetKeyDown(KeyCode.Alpha2) && !_isBuilding) 
		{
			BuildTower(1);
		} else if(Input.GetKeyDown(KeyCode.Alpha3) && !_isBuilding) 
		{
			BuildTower(2);
		} else if(Input.GetKeyDown(KeyCode.Alpha4) && !_isBuilding) 
		{
			BuildTower(3);
		}
		if(_isBuilding)
		{
			Vector3 newSpawnPointPos = spawnPoint.position;
			newSpawnPointPos.y = 1f;
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
		_currentTower = Instantiate(buildTower, Vector3.zero, Quaternion.identity) as GameObject;
		_isBuilding = true;
		_towerToBuild = towerSort;
	}
}
