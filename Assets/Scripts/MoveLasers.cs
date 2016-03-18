using UnityEngine;
using System.Collections;

public class MoveLasers : MonoBehaviour {

    public GameObject prefab50;
    public GameObject prefab100;

	void Start () {
	}

	void Update () 
    {
       // this.gameObject.rigidbody.AddRelativeForce(20, 20, 0);
	}

    void OnTriggerEnter(Collider objects)
    {
        if(objects.GetComponent<Collider>().gameObject.tag == "SmallAsteroids")
        {
            Destroy(objects.gameObject);
            GameObject score = Instantiate(prefab50, this.gameObject.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            Destroy(this.gameObject);
        }
    }
}
