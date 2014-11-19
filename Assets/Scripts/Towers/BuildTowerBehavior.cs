using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildTowerBehavior : MonoBehaviour {
	public bool buildAble;
	private int counter;
	private List<GameObject> _CurrentObjects = new List<GameObject>();
	private List<Material> allChildrenMaterials = new List<Material>();
	void Start()
	{
		Renderer[] allChildrenRenderers = GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in allChildrenRenderers)
		{
			allChildrenMaterials.Add(renderer.material);
		}
	}
	void Update()
	{
		if(_CurrentObjects.Count == 0)
		{
			Color newColor = Color.white;
			newColor.a = 0.5f;
			foreach(Material material in allChildrenMaterials)
			{
				material.color = newColor;
			}
			buildAble = true;
		} else {
			Color newColor = Color.red;
			newColor.a = 0.5f;
			foreach(Material material in allChildrenMaterials)
			{
				material.color = newColor;
			}
			buildAble = false;
		}
		foreach(GameObject obstacle in _CurrentObjects)
		{
			if(obstacle == null)
			{
				_CurrentObjects.Remove(obstacle);
			}
		}
	}
	void OnTriggerStay(Collider other)
	{
		Debug.Log(other.transform.tag);
		if(other.transform.tag != "Floor" && other.transform.tag != "Player")
		{
			bool notAdded = true;
			foreach(GameObject obstacle in _CurrentObjects)
			{
				if(other.gameObject == obstacle)
				{
					notAdded = false;
					break;
				}
			}
			if(notAdded)
			{
				_CurrentObjects.Add(other.gameObject);
			}
			if(_CurrentObjects.Count == 0)
			{
				_CurrentObjects.Add(other.gameObject);
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		foreach(GameObject obstacle in _CurrentObjects)
		{
			if(other.gameObject == obstacle)
			{
				_CurrentObjects.Remove(other.gameObject);
			}
		}
	}
}
