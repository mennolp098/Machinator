using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

	public GameObject buildTower;
	public Transform spawnPoint;

	public GameObject[] allTowers = new GameObject[0];

	private int _towerToBuild;
	private bool _isBuilding = false;
	private GameObject _currentTower;

	void Update () {
		if(Input.GetMouseButtonDown(0) && !_isBuilding)
		{
			RaycastHit hit;
			Ray ray;
			
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.transform.tag == "Tower")
				{
					if(hit.distance <= 0.8f)
					{
						hit.transform.GetComponent<TowerController>().HitTurret();
					}
				}
			}
		} else if(Input.GetMouseButtonDown(0) && _isBuilding)
		{
			Instantiate(allTowers[_towerToBuild], _currentTower.transform.position,_currentTower.transform.rotation);
			Destroy(_currentTower.gameObject);
			_isBuilding = false;
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
			_currentTower.transform.position = newSpawnPointPos;
			_currentTower.transform.rotation = spawnPoint.rotation;
		}
	}
	private void BuildTower(int towerSort)
	{
		_currentTower = Instantiate(buildTower, Vector3.zero, Quaternion.identity) as GameObject;
		_isBuilding = true;
		_towerToBuild = towerSort;
	}
}
