using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private GameObject[] _enemys;
    [SerializeField]
    private float _enemyCoolDown;
    [SerializeField]
    private float _waveCoolDown;
    [SerializeField]
    private int _startingEnemys;
    [SerializeField]
    private int _enemyMultiplayer;
    private int _maxEnemys;
    private int _currentEnemys;
    private int _wave;
	// Use this for initialization
	void Start () 
    {
        Invoke("startWave", _waveCoolDown);
        _maxEnemys = _startingEnemys;
	}
	
    void startWave()
    {
        Invoke("spawnEnemy", _enemyCoolDown);
    }
	void spawnEnemy () 
    {
        _currentEnemys++;
		int random = Random.Range (0, _enemys.Length);
		GameObject newEnemy = Instantiate(_enemys[random].gameObject,this.transform.position,this.transform.rotation) as GameObject;
		newEnemy.transform.parent = GameObject.FindGameObjectWithTag ("Enemys").transform;
        if(random == 0)
        {
            newEnemy.GetComponent<NormalEnemy>().health += 2 * _wave;
        }
        if (random == 1)
        {
            newEnemy.GetComponent<FastEnemy>().health += 2 * _wave;
        }
        if (random == 2)
        {
            newEnemy.GetComponent<StrongEnemy>().health += 5 *_wave;
        }
        if(_currentEnemys == _maxEnemys)
        {
            _currentEnemys = 0;
            _wave++;
            _maxEnemys += _enemyMultiplayer * _wave;
            
            Invoke("startWave", _waveCoolDown);
        }
        else
        {
            Invoke("spawnEnemy", _enemyCoolDown);
        }
        
	}
}
