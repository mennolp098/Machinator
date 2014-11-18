using UnityEngine;
using System.Collections;

public class BuildTowerBehavior : MonoBehaviour {
	public bool buildAble;
	void OnTriggerEnter(Collider other)
	{
		Color newColor = Color.red;
		this.renderer.material.color = newColor;
		buildAble = false;
	}
	void OnTriggerExit(Collider other)
	{
		Color newColor = Color.white;
		newColor.a = 0.5f;
		this.renderer.material.color = newColor;
		buildAble = true;
	}
}
