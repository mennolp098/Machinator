using UnityEngine;
using System.Collections;

public class deleteParticle : MonoBehaviour {
	void Start () {
		Destroy (this.gameObject, 0.5f);
	}
}
