using UnityEngine;
using System.Collections;

public class BigAsteroid : MonoBehaviour 
{
    public int Counter;
    public Sprite[] bigAsteroidSprites;
    private Vector3 DirectionVector;
    public static float VELOCITY_BigAsteroids;
    public static bool DestroyedInTutorial;
    public static float timer;
    private static int multiplier;
    public GameObject points;
    public GameObject particle;
    public GameObject BACollSound;
    public bool INSIDEBLACKHOLE;
	void Start () 
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = bigAsteroidSprites[Random.Range(0, bigAsteroidSprites.Length)];
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 18));
        DirectionVector = bottomLeft - topRight;
        DirectionVector = Vector3.Normalize(DirectionVector);
        DestroyedInTutorial = false;
	}
	
	
	void Update () 
    {
        if (GameScript.PLAY && !INSIDEBLACKHOLE)
        {
            this.GetComponent<Rigidbody>().velocity = DirectionVector * VELOCITY_BigAsteroids;
        }
        if(GameScript.PAUSE)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (Counter <= 0)
        {
            if(!PlayerPrefs.HasKey("BIGASTEROID"))
            {
                PlayerPrefs.SetInt("BIGASTEROID", 1);
            }   
            Vector3 position = Camera.main.WorldToScreenPoint(this.transform.position);
            GameScript.SCORE += 100;
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider Ship)
    {
        if(Ship.GetComponent<Collider>().gameObject.tag == "SpaceShip" && !ShipCollider.SHIELD)
        {
            if (GameScript.SOUND)
            {
                GameObject sound = Instantiate(BACollSound, Camera.main.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            }
            GameScript.HEALTH -= 50;
        }
        if (Ship.GetComponent<Collider>().gameObject.tag == "Laser")
        {
            Destroy(Ship);
            Counter--;
        }
    }
}
