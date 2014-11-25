using UnityEngine;
using System.Collections;

public class EndTimer : MonoBehaviour {
    [SerializeField]
    private float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= 1 * Time.deltaTime;
        if(timer <= 0)
        {
            Application.LoadLevel("Menu");
        }
	}
}
