using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour 
{
	private bool		follow;
	private GameObject 	spaceShip;
    private GameObject  bigAsteroid;
    private int         fingerID;
    private int         fingerIDLeft;
    private int         fingerIDRight;
    private BigAsteroid bigAsteroidScript;
    public GameObject asteroidClickSound;
    public static bool FOLLOW;
    public GameObject rightJoystick;
    public GameObject leftJoystick;
    public GameObject[] Bullet;
    public static int bulletNumber;


    private GameObject moveJoy;
    private GameObject _GameManager;
    private Vector3 endPositionShip;
    private Vector3 endPositionSwipe;
    public Vector3 movement;
    private float distance;
    private Vector3 direction;
    private Vector3 startPositionShip;
    private Vector3 startPositionSwipe;
    public float moveSpeed = 250.0f;
    public float jumpSpeed = 5.0f;
    public float drag = 2;
    private float laserGap;
    private Vector3 screenBoundsX;
    private Vector3 screenBoundsY;
    
	void Start () 
	{
        bulletNumber = 0;
        
	}
	
	void Update () 
	{
        if (GameScript.PLAY)
        {
            screenBoundsX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18.0f));
            screenBoundsY = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 18.0f));
            if (this.transform.position.x < screenBoundsX.x)
            {
                this.transform.position = new Vector3(screenBoundsX.x, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x > screenBoundsY.x)
            {
                this.transform.position = new Vector3(screenBoundsY.x, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.y < screenBoundsX.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, screenBoundsX.y, this.transform.position.z);
            }
            if (this.transform.position.y > screenBoundsY.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, screenBoundsY.y, this.transform.position.z);
            }
        }

        if (Application.isEditor)
        {
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.up);
            forward.x = 0;
            forward = forward.normalized;

            Vector3 forwardForce = new Vector3();
           
            forwardForce = forward * Input.GetAxis("Vertical") * moveSpeed;
 
            GetComponent<Rigidbody>().AddForce(forwardForce);

            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
            right.y = 0;
            right = right.normalized;

            Vector3 rightForce = new Vector3();
            rightForce = right * Input.GetAxis("Horizontal") * moveSpeed;
            GetComponent<Rigidbody>().AddForce(rightForce);

            if(Input.GetKeyUp(KeyCode.Alpha1)&& Time.time - laserGap >0.2f)
            {
                laserGap = Time.time;
                GameObject Laser = Instantiate(Bullet[0], new Vector3(this.transform.position.x + 3, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                Laser.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
                bulletNumber = 0;
            }
            if (Input.GetKeyUp(KeyCode.Alpha2) && Time.time - laserGap > 0.2f)
            {
                laserGap = Time.time;
                GameObject Laser = Instantiate(Bullet[1], new Vector3(this.transform.position.x + 3, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                Laser.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
                bulletNumber = 1;
            }
            if (Input.GetKeyUp(KeyCode.Alpha3) && Time.time - laserGap > 0.2f)
            {
                laserGap = Time.time;
                GameObject Laser = Instantiate(Bullet[2], new Vector3(this.transform.position.x + 3, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                Laser.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
                bulletNumber = 2;
            }
        }
		
		//for touch

        if (Input.touchCount > 0 && FOLLOW)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                Touch touch = Input.touches[i];


                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        fingerID = touch.fingerId;
                        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
                        if (touch.position.x < Screen.width / 2)
                        {
                            startPositionShip = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 18f));
                            fingerIDLeft = fingerID;
                        }
                        fingerIDRight = fingerID;
                        startPositionSwipe = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 18f));
                        break;
                    case TouchPhase.Moved:
                        if (touch.position.x < Screen.width / 2 && touch.fingerId == fingerIDLeft)
                        {
                            endPositionShip = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 18f));
                            distance = Vector3.Distance(startPositionShip,endPositionShip);
                            if(distance < 100)
                            {
                                direction = startPositionShip - endPositionShip;
                                direction = Vector3.Normalize(direction);
                                this.GetComponent<Rigidbody>().AddForce(-direction*450);
                            }
                        }
                        break;
                    case TouchPhase.Stationary:
                        if (touch.position.x < Screen.width / 2 && touch.fingerId == fingerIDLeft)
                        {
                            endPositionShip = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 18f));
                            distance = Vector3.Distance(startPositionShip, endPositionShip);
                            if (distance < 100 )
                            {
                                direction = startPositionShip - endPositionShip;
                                direction = Vector3.Normalize(direction);
                                this.GetComponent<Rigidbody>().AddForce(-direction * 450);
                            }
                        }
                        break;
                    case TouchPhase.Ended:
                        if (touch.fingerId == fingerIDRight)
                        {
                            endPositionSwipe = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 18f));
                            if (startPositionSwipe.x - endPositionSwipe.x > 5)
                            {
                                if (bulletNumber > 0)
                                    bulletNumber--;
                            }
                            if (startPositionSwipe.x - endPositionSwipe.x < -5)
                            {
                                if (bulletNumber < 2)
                                    bulletNumber++;
                            }
                            if ((startPositionSwipe.x - endPositionSwipe.x < 2 && startPositionSwipe.x - endPositionSwipe.x > -2) && touch.position.x > Screen.width / 2 && Time.time - laserGap > 0.2f)
                            {
                                laserGap = Time.time;
                                if (bulletNumber == 1)
                                {
                                    int gooCount = PlayerPrefs.GetInt("GOO_AMMO");
                                    if (gooCount > 0)
                                    {
                                        gooCount--;
                                        PlayerPrefs.SetInt("GOO_AMMO", gooCount);
                                        GameObject GooLaser = Instantiate(Bullet[bulletNumber], new Vector3(this.transform.position.x + 3, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                                        GooLaser.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
                                    }
                                }
                                else
                                {
                                    if (bulletNumber == 2)
                                    {
                                        int meteorCount = PlayerPrefs.GetInt("METEOR_AMMO");
                                        if (meteorCount > 0)
                                        {
                                            meteorCount--;
                                            PlayerPrefs.SetInt("METEOR_AMMO", meteorCount);
                                            GameObject MeteorLaser = Instantiate(Bullet[bulletNumber], new Vector3(this.transform.position.x + 3, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                                            MeteorLaser.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
                                        }
                                    }
                                    else
                                    {
                                        GameObject Laser = Instantiate(Bullet[bulletNumber], new Vector3(this.transform.position.x + 3, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                                        Laser.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
                                    }
                                }
                                
                            }
                        }
                        break;
                }
            }
        }


        if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
        

	}

    void SetPosition()
    {
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(45, 45, 18));
        this.gameObject.transform.position = startPosition;
    }
}
