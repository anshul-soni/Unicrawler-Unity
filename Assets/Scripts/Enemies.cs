using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour {

    private int deathCounter;
    public GameObject deathAnimation;
    public GameObject deathSound;
    public GameObject orb;
    private Vector3 DirectionVector;
    public float Velocity;
    public GameObject PickUpSound;
    public static int shieldCounter;
    private bool meteorDeath;
    public GameObject AnimEffect;

    void Start()
    {
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 18));
        DirectionVector = bottomLeft - topRight;
        DirectionVector = Vector3.Normalize(DirectionVector);
        deathCounter = 2;
        meteorDeath = false;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (deathCounter <= 0)
        {
            GameObject animation = Instantiate(deathAnimation, new Vector3(this.transform.position.x,this.transform.position.y,1), this.transform.rotation) as GameObject;
            //GameObject sound = Instantiate(deathSound, Camera.main.transform.position, this.transform.rotation) as GameObject;
            GameObject ORB = Instantiate(orb, this.transform.position, this.transform.rotation) as GameObject;
            if (meteorDeath)
            {
                meteorDeath = false;
                GameObject ORB2 = Instantiate(orb, new Vector3(this.transform.position.x+2,this.transform.position.y,this.transform.position.z), this.transform.rotation) as GameObject;
            }
            Destroy(this.gameObject);
        }
        if (GameScript.PLAY)
        {
            this.GetComponent<Rigidbody>().velocity = DirectionVector * Velocity;
        }
        if (GameScript.PAUSE)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}

    void OnTriggerEnter(Collider objects)
    {
        if (objects.gameObject.tag == "SpaceShip")
        {
            GameObject eff = Instantiate(AnimEffect, this.transform.position, this.transform.rotation) as GameObject;
            GameScript.HEALTH -= 25;
            Destroy(this.gameObject);
        }

        if (objects.gameObject.tag == "GooAmmo")
        {
            meteorDeath = false;
            deathCounter--;
            Destroy(objects.gameObject);
        }

        if (objects.gameObject.tag == "MeteorAmmo")
        {
            meteorDeath = true;
            deathCounter -= 2;
            Destroy(objects.gameObject);
        }
    }
}
