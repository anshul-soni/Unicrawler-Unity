using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void Start () 
    {
        this.gameObject.transform.GetComponent<Renderer>().enabled = false;
	}
	
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider objects)
    {
        if(objects.gameObject.GetComponent<Collider>().tag == "Planet")
        {
            Creator.PLANETCOUNTER--;
        }
        Destroy(objects.gameObject);
    }
}
