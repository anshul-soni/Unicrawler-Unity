using UnityEngine;
using System.Collections;

public class SpaceJelly : MonoBehaviour {

    private GameObject player;

    public float speed = 0.005F;

	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("SpaceShip");
	}
	
	void Update () 
    {
        if (!GameScript.PAUSE)
        {
            transform.position = Vector3.Lerp(this.transform.position, player.transform.position, speed);
            if (this.transform.position.x - player.transform.position.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

	}
}
