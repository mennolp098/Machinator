using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {
    [SerializeField]
    private GameObject maninMenu;
    [SerializeField]
    private GameObject helpMenu;
    [SerializeField]
    private GameObject creditzMenu;
    [SerializeField]
    private GameObject PlayButton;
    [SerializeField]
    private GameObject helpButton;
    [SerializeField]
    private GameObject creditButton;
    private Vector3 PlayCods;
    private Vector3 helpCods;
    private Vector3 credCods;
    private Animator anime;
    void Start()
    {
        anime = maninMenu.GetComponent<Animator>();
        PlayCods = Camera.main.WorldToScreenPoint(PlayButton.transform.position);
        helpCods = Camera.main.WorldToScreenPoint(helpButton.transform.position);
        credCods = Camera.main.WorldToScreenPoint(creditButton.transform.position);
    }
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
            if (GUI.Button(new Rect(PlayCods.x - 100, Screen.height - PlayCods.y - 30, 200, 50), "", GUIStyle.none))
            {
                Application.LoadLevel("towerdefence");
            }
            if (GUI.Button(new Rect(helpCods.x - 100,  Screen.height-helpCods.y -20, 200, 60), "",GUIStyle.none))
            {
                helpMenu.SetActive(true);
                maninMenu.SetActive(false);
            }
            if (GUI.Button(new Rect(credCods.x - 150, Screen.height - credCods.y - 20, 300, 60), "", GUIStyle.none))
            {
                creditzMenu.SetActive(true);
                maninMenu.SetActive(false);
            }
        }
    }
}
