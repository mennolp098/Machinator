using UnityEngine;
using System.Collections;

public class ExplosionHandler : MonoBehaviour {

	private float explosionDmg;
	void Start () {
		explosionDmg = 5f;
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 10f);
		for (int i = 0; i < hitColliders.Length; i++) {
			if(hitColliders[i].transform.tag == "Enemy")
			{
				hitColliders[i].GetComponent<EnemyBehavior>().GetDmg(explosionDmg);
			}
		}
	}
}
