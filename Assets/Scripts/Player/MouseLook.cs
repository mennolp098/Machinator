using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {
	public Texture2D cursorTexture;

	private  enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;
    private float sensitivityX = 15F;
    private float sensitivityY = 15F;

    //private float minimumX = -360F;
    //private float maximumX = 360F;

    private float minimumY = -30F;
    private float maximumY = 30F;

    private float rotationY = 0F;
    private float speed = 3;


	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width/2, Screen.height/2, 32, 32), cursorTexture);
	}
	void Awake()
	{
		Screen.lockCursor = true;
	}
    void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
	void Update ()
	{
        float directionX = Input.GetAxis ("Horizontal");
		float directionY = Input.GetAxis ("Vertical");
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
		{
			transform.position += transform.forward*directionY * speed * Time.deltaTime;
		}  
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			transform.position += transform.right*directionX * speed * Time.deltaTime;
		}
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}