using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

   
    public float forceConstant;
    private GameObject Objects;
    private float force;
    private bool PULL;
    private float radius;
    private Vector3 DirectionVector;
    public float Velocity;
    public GameObject blackHoleSound;
    private AsteroidsMovement myMovement;
    private BigAsteroid BigMovement;
    private float startTime;
    public float lifeTime;
    bool insideBlackHole;
	void Start () 
    {
        if (GameScript.SOUND)
        {
            GameObject sound = Instantiate(blackHoleSound, Camera.main.transform.position, this.transform.rotation) as GameObject;
        }
        SphereCollider myCollider = this.transform.GetComponent<SphereCollider>();
        insideBlackHole = false;
        PULL = false;
        radius = myCollider.radius;
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 18));
        DirectionVector = bottomLeft - topRight;
        DirectionVector = Vector3.Normalize(DirectionVector);
        startTime = Time.time;
	}
	
	
	void Update () 
    {
        if(Time.time-startTime > lifeTime)
        {
            Destroy(this.gameObject);
        }
        if(Objects!=null)
        {
            force = PullingForce(Objects);
            Vector3 directionVector = Objects.transform.position - this.transform.position;
            directionVector = Vector3.Normalize(directionVector);
            Objects.GetComponent<Rigidbody>().AddForce(directionVector * +force);
            Debug.Log("applyingforce");
        }
        if (!PULL && Objects != null)
        {
            Objects.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (GameScript.PLAY)
        {
            //this.rigidbody.velocity = DirectionVector * Velocity;
            //this.rigidbody.AddTorque(new Vector3(0, 0, 5));
        }
	}

    void OnTriggerEnter(Collider objects)
    {
        //if (object.gameObject.tag == "SpaceShip")
        //{
        //    SpaceShip = spaceShip.gameObject;
        //    insideBlackHole = true;
        //}
        if(objects.gameObject.tag == "SmallAsteroids" || objects.gameObject.tag == "BigAsteroids")
        {
            Objects = objects.gameObject;
            myMovement = Objects.gameObject.GetComponent<AsteroidsMovement>();
            insideBlackHole = true;
            PULL = true;
            myMovement.INSIDEBLACKHOLE = true;
        }
        if (objects.gameObject.tag == "SmallAsteroids" || objects.gameObject.tag == "BigAsteroids")
        {
            Objects = objects.gameObject;
            BigMovement = Objects.gameObject.GetComponent<BigAsteroid>();
            insideBlackHole = true;
            PULL = true;
            BigMovement.INSIDEBLACKHOLE = true;
        }
    }

    float PullingForce(GameObject spaceShip)
    {
        float dx = Mathf.Abs(this.gameObject.transform.position.x - spaceShip.transform.position.x);
        float dy = Mathf.Abs(this.gameObject.transform.position.y - spaceShip.transform.position.y);
        float distance= Mathf.Sqrt((dx * dx) + (dy * dy));
        float Force=0;
        if(distance < radius)
        {
            Force = forceConstant - distance;
        }else
        {
            Force = 0;
        }
        return Force;
    }
}
