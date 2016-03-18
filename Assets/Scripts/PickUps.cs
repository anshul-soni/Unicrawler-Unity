using UnityEngine;
using System.Collections;

public class PickUps : MonoBehaviour {

    public GameObject PickUpSound;
    private Vector3 DirectionVector;

    public float Velocity;
	void Start () 
    {
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 18));
        DirectionVector = bottomLeft - topRight;
        DirectionVector = Vector3.Normalize(DirectionVector);    
	}
	
	void Update () 
    {
        //if (this.gameObject.tag == "Health")
        //{
            if (GameScript.PLAY)
            {
                if (this.GetComponent<Rigidbody>() != null)
                {
                    this.GetComponent<Rigidbody>().velocity = DirectionVector * Velocity;
                }
            }
            if (GameScript.PAUSE)
            {
                if (this.GetComponent<Rigidbody>() != null)
                {
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                
            }
        //}
	}

    void OnTriggerEnter(Collider spaceShip)
    {
        if (spaceShip.GetComponent<Collider>().gameObject.tag == "SpaceShip")
        {
            int orbCounter = PlayerPrefs.GetInt("ORBS");
            orbCounter++;
            GameScript.OrbCounter++;
            PlayerPrefs.SetInt("ORBS", orbCounter);
            if (GameScript.SOUND)
            {
                //GameObject sound = Instantiate(PickUpSound, Camera.main.transform.position, this.transform.rotation) as GameObject;
            }
            Destroy(this.gameObject);
        }
    }
}
