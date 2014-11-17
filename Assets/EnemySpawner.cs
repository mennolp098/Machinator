using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject[] Enemys;
	public float coolDown;
	// Use this for initialization
	void Start () {
		Invoke ("spawnEnemy", coolDown);
	}
	
	// Update is called once per frame
	void spawnEnemy () {
		int random = Random.Range (0, Enemys.Length);
		GameObject newEnemy = Instantiate(Enemys[random].gameObject,this.transform.position,this.transform.rotation) as GameObject;
		newEnemy.transform.parent = GameObject.FindGameObjectWithTag ("Enemys").transform;
		Invoke ("spawnEnemy", coolDown);
	}
}
