using UnityEngine;
using System.Collections;

public class RemoveParticleSystem : MonoBehaviour {
	private ParticleSystem _particleSystem;
	// Use this for initialization
	void Start () {
		_particleSystem = GetComponent<ParticleSystem>();
		Invoke("removeMe", _particleSystem.duration);
	}
	
	// Update is called once per frame
	void removeMe()
	{
		Destroy(this.gameObject);
	}
}
