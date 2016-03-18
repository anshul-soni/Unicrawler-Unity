using UnityEngine;
using System.Collections;

public class PlaceButton : MonoBehaviour 
{
    private bool set;
	void Start () 
    {
        set = true;
	}
	
	void Update () 
    {
	    if(GameScript.PLAY && set)
        {
            set = false;
            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/35, Screen.height - 25, 18));
            this.transform.position = position;
        }
	}
}
