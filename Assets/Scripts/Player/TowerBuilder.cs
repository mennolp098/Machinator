
using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

	public GameObject[] buildTowers = new GameObject[0];
	public Transform spawnPoint;
	public GameObject[] allTowers = new GameObject[0];
	public GUIStyle textStyle;
	public GameObject clash;

	private int _towerToBuild = -1;
	private bool _isBuilding = false;
	private bool _isSwinging = false;
	private GameObject _currentTower;
	
    [SerializeField]
    private Texture2D[] UIButtons;
    [SerializeField]
    private Texture2D UiBack;

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
					if(hit.distance <= 1f)
					{
						hit.transform.GetComponent<TowerController>().HitTurret();
						Vector3 eulerRot = this.transform.eulerAngles;
						eulerRot.y -= 180;
						Quaternion spawnRot = Quaternion.Euler(eulerRot);
						Instantiate(clash, hit.point, spawnRot);

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
                _towerToBuild = -1;
			}
		} else if(Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			Ray ray;
			
			ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
			ray.origin = Camera.main.transform.position;
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.transform.tag == "Tower")
				{
					if(hit.distance <= 1f)
					{
						hit.transform.GetComponent<TowerController>().ShowLevel();
					}
				}
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
			ClearTower();
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
	private void ClearTower()
	{
		if(_currentTower != null)
		{
			Destroy(_currentTower.gameObject);
			_isBuilding = false;
			_towerToBuild = -1;
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
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width /2 - 150, Screen.height - UiBack.height /2, UiBack.width/2, UiBack.height/2), UiBack);
		float totalMaterials = GetComponentInParent<MaterialHandler>().GetMaterials();
        GUI.DrawTexture(new Rect(Screen.width /2 - 150, Screen.height - UiBack.height /2, UiBack.width / 2, UiBack.height / 2), UiBack);
		GUI.TextField(new Rect(Screen.width/2 + 50, Screen.height + UiBack.height - 150, 50,50), "" + totalMaterials, textStyle);
        if (_towerToBuild >= 0)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - 150 + 17 + (45 * _towerToBuild), Screen.height - UiBack.height / 2 + 13, 40, 30), UIButtons[_towerToBuild]);
        }
    }
}