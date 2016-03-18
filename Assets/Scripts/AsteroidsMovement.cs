using UnityEngine;
using System.Collections;

public class AsteroidsMovement : MonoBehaviour 
{
    private Vector3 DirectionVector;
    public Sprite[] asteroidSprites;
    public bool NOTSTRIKED;
    public static float VELOCITY_SmallAsteroid;
    public bool INSIDEBLACKHOLE;
	void Start () 
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
        NOTSTRIKED = true;
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 18));
        DirectionVector = bottomLeft - topRight;
        DirectionVector = Vector3.Normalize(DirectionVector);
        INSIDEBLACKHOLE = false;
	}
	
	void Update () 
    {
        if(NOTSTRIKED && GameScript.PLAY && !INSIDEBLACKHOLE)
            this.GetComponent<Rigidbody>().velocity = DirectionVector * VELOCITY_SmallAsteroid;
        if(GameScript.PAUSE)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}
}
