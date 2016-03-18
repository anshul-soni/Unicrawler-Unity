using UnityEngine;
using System.Collections;

public class ShipCollider : MonoBehaviour 
{

    public static bool SHIELD;
    public float shieldTimer;
    public static float SHIELDSTART;
    public Material yellow;
    public Material normal;
    private bool notChanged;
    public GameObject points;
    public GameObject destroySound;
    private Vector3 lastAcc;
    public GameObject plus50;
    public GameObject plus100;
    public static int powerUpOrbsCounter;
    public GameObject powerUpIndicator;
    public static int powerUpCounter;
    private int indicatorCounter;
    private GameObject indicator;
    public static bool activateLaser;
    public GameObject CollisionSound;
    public GameObject Blackhole;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;

    public GameObject PowerUpSound;
    public static bool POWERTAP;

	void Start () 
    {
        SHIELD = false;
        notChanged = true;
	}
	
	void Update () 
    {
        if (Time.time - SHIELDSTART > shieldTimer)
        {
            SHIELD = false;
        }
        if(SHIELD )
        {
            this.transform.GetComponent<Renderer>().material = yellow;
            notChanged = true;
        }else
        {
            if (notChanged)
            {
                this.transform.GetComponent<Renderer>().material = normal;
                notChanged = false;
            }
        }
	}

    void OnCollisionEnter(Collision obstacles)
    {
        if (obstacles.collider.gameObject.tag == "SmallAsteroids" && !SHIELD)
        {
            GameScript.HEALTH -= 25;
            AsteroidsMovement myScript = obstacles.gameObject.GetComponent<AsteroidsMovement>();
            myScript.NOTSTRIKED = false;
            if (GameScript.SOUND)
            {
                GameObject sound = Instantiate(CollisionSound, Camera.main.transform.position, this.transform.rotation) as GameObject;
            }
            obstacles.rigidbody.velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obstacles.rigidbody.AddExplosionForce(1000, this.gameObject.transform.position, 3);
            StartCoroutine(WaitToDestroy(obstacles.gameObject));
        }
        else
        {
            if (SHIELD && obstacles.collider.gameObject.tag == "SmallAsteroids")
            {
                GameScript.SCORE += 50;
                GameObject point = Instantiate(points,obstacles.collider.gameObject.transform.position,Quaternion.Euler(0,0,0)) as GameObject;
                if (GameScript.SOUND)
                {
                    GameObject sound = Instantiate(destroySound, Camera.main.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                }
                Destroy(obstacles.collider.gameObject);
            }
        }
    }

    IEnumerator WaitToDestroy(GameObject obs)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(obs);
    }
}
