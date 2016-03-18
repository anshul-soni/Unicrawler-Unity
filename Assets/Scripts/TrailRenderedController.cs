using UnityEngine;
using System.Collections;

public class TrailRenderedController : MonoBehaviour {

    private TrailRenderer Trail;
    private float time;
    private bool change;
	void Start () 
    {
        change = false;
        Trail = this.gameObject.GetComponent<TrailRenderer>();
        time = Trail.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(GameScript.PAUSE)
        {
            Trail.gameObject.GetComponent<Renderer>().enabled = false;
            change = true;
        }
        if(change && !GameScript.PAUSE)
        {
            Trail.gameObject.GetComponent<Renderer>().enabled = true;
            change = false;
        }
	}
}
