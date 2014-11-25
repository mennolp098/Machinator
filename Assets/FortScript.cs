using UnityEngine;
using System.Collections;

public class FortScript : MonoBehaviour {
    [SerializeField]
    private int health;
	
	void Update()
    {
        if (health <= 0)
        {
            Application.LoadLevel("lose");
            Destroy(this.gameObject);
        }
    }
    public void hit()
    {
        health--;
    }
}
