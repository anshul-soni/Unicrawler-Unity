using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {


    private int fingerID;
    private int fingerIDTouch;
    private GameScript GUICall;

	void Start () 
    {
        GUICall = Camera.main.GetComponent<GameScript>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                Touch touch = Input.touches[i];


                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        fingerID = touch.fingerId;
                        Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit touchHit;
                        if (Physics.Raycast(touchRay, out touchHit, 100))
                        {
                            if (touchHit.collider.gameObject.tag == "Pause")
                            {
                                GUICall.GUIButtons("Pause");
                            }
                        }
                        break;
                }
            }
        }
	}
}
