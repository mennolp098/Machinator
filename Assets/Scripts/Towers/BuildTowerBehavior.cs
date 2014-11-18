using UnityEngine;
using System.Collections;

public class BuildTowerBehavior : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		Color newColor = Color.red;
		this.renderer.material.color = newColor;
	}
	void OnTriggerExit(Collider other)
	{
		Color newColor = Color.white;
		newColor.a = 0.5f;
		this.renderer.material.color = newColor;
	}
}
