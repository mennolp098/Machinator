using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildTowerBehavior : MonoBehaviour {
	public bool buildAble;
	private bool hitting = false;
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
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1f);
		for (int i = 0; i < hitColliders.Length; i++) {
			if(hitColliders[i].transform.tag != "Floor" && hitColliders[i].transform.tag != "Player" && hitColliders[i].transform.tag
			    != "BuildTower" && !hitColliders[i].isTrigger)
			{
				hitting = true;
				break;
			} else {
				hitting = false;
			}
		}
		if(!hitting)
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
	}
}
