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
            if (GUI.Button(new Rect(650, 260, 200, 75), "", GUIStyle.none))
            {
                Application.LoadLevel("towerdefence");
            }
            if (GUI.Button(new Rect(650, 360, 200, 75), "", GUIStyle.none))
            {
                helpMenu.SetActive(true);
                maninMenu.SetActive(false);
            }
            if (GUI.Button(new Rect(585, 440, 300, 75), "", GUIStyle.none))
            {
                creditzMenu.SetActive(true);
                maninMenu.SetActive(false);
            }
        }
    }
}
