using UnityEngine;
using System.Collections;

public class ThemeSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!GetComponent<AudioSource>().isPlaying && GameScript.SOUND)
        {
            if (!GameScript.PLAY)
            {
                GetComponent<AudioSource>().Play();
            }
        }else
        {
            if(GameScript.PLAY || !GameScript.SOUND)
            {
                GetComponent<AudioSource>().Stop();
            }
        }
        this.transform.position = Camera.main.transform.position;
	}
}
