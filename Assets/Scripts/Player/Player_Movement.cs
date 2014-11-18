using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

    private float _rotSpeed = 6;
    //private float _movmentSpeed = 6;
	void Start () 
    {
	
	}
	
	void Update () 
    {
        float AxisX = Input.GetAxis("Mouse X");
        float AxisY = Input.GetAxis("Mouse Y");
        //float direction = Input.GetAxis("Vertical");

        this.transform.rotation = transform.rotation * Quaternion.Euler(-AxisY * _rotSpeed, AxisX * _rotSpeed, 0);
        //rigidbody.velocity = new Vector3(transform.forward.x * direction * _movmentSpeed, -1, transform.forward.z * direction * _movmentSpeed);// transform.forward * direction * _movmentSpeed;
	}
}
