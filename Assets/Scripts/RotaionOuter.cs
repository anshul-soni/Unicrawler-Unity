using UnityEngine;
using System.Collections;

public class RotaionOuter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -3));
	}
}
