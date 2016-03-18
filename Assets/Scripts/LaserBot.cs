using UnityEngine;
using System.Collections;

public class LaserBot : MonoBehaviour {

    private GameObject player;
    private Vector3 endPosition;
    public float speed = 0.005F;

    private Vector3 DirectionVector;
    public float Velocity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("SpaceShip");
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 18));
        DirectionVector = bottomLeft - topRight;
        DirectionVector = Vector3.Normalize(DirectionVector);
    }
	void Update () 
    {
        

        if (GameScript.PLAY)
        {
            endPosition.x = this.transform.position.x;
            endPosition.y = player.transform.position.y; 
            endPosition.z = this.transform.position.z;
            transform.position = Vector3.Lerp(this.transform.position, endPosition, speed);
            this.GetComponent<Rigidbody>().velocity = DirectionVector * Velocity;
        }
        if (GameScript.PAUSE)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}
}
