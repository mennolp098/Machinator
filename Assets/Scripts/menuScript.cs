using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {
    [SerializeField]
    private GameObject maninMenu;
    [SerializeField]
    private GameObject helpMenu;
    [SerializeField]
    private GameObject creditzMenu;
    void Update()
    {
        if (maninMenu.activeSelf == false)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                maninMenu.SetActive(true);
                helpMenu.SetActive(false);
                creditzMenu.SetActive(false);
            }
        }
    }

	void OnGUI()
    {
        if (maninMenu.activeSelf == true)
        {
            if (GUI.Button(new Rect(Screen.width /2 - 100, 230, 200, 75), ""))
            {
                Application.LoadLevel("towerdefence");
            }
			if (GUI.Button(new Rect(Screen.width /2 - 100, 315, 200, 75), ""))
            {
                helpMenu.SetActive(true);
                maninMenu.SetActive(false);
            }
			if (GUI.Button(new Rect(Screen.width /2 - 150, 400, 300, 75), ""))
            {
                creditzMenu.SetActive(true);
                maninMenu.SetActive(false);
            }
        }
    }
}
