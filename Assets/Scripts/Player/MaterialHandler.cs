using UnityEngine;
using System.Collections;

public class MaterialHandler : MonoBehaviour {
	private float _materials = 60000f;
	public float GetMaterials()
	{
		return _materials;
	}
	public void SetMaterials(float newMaterials)
	{
		_materials = newMaterials;
	}
	public void AddMaterials(float materials)
	{
		_materials += materials;
	}
}
