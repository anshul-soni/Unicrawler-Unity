using UnityEngine;
using System.Collections;

public class Creator : MonoBehaviour
{

    public GameObject asteroids;
    public GameObject bigAsteroids;
    public GameObject bgPlanets;
    public GameObject[] pickUps;
    public GameObject blackHole;
    public static float smallAsteroidFrequency;
    public static float bigAsteroidFrequency;
    public static float pickUpFrequency;
    public float numberOfPlanets;
    public static float PLANETCOUNTER;
    private Vector3 DirectionVector;

    private float lastCreatedSmall;
    private float lastCreatedBig;
    private float lastCreatedPickUp;
    private Vector3 position;
    private bool change;
    public static int TutorialPickup;
    public GameObject point1;
    public GameObject point2;


    void Start()
    {
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 18));
        DirectionVector = bottomLeft - topRight;
        DirectionVector = Vector3.Normalize(DirectionVector);
        position.z = 8;
        change = true;
        PLANETCOUNTER = 0;
        lastCreatedSmall = 0;
        TutorialPickup = 0;
    }

    void Update()
    {
        if (GameScript.PLAY && !GameScript.PAUSE && PlayerPrefs.HasKey("TUTORIALDONE"))
        {
            randomPosition(change);
            if (Time.time - smallAsteroidFrequency > lastCreatedSmall && smallAsteroidFrequency != 0)
            {
                lastCreatedSmall = Time.time;
                GameObject asteroid = Instantiate(asteroids, position, this.transform.rotation) as GameObject;
            }
            if (Time.time - bigAsteroidFrequency > lastCreatedBig && bigAsteroidFrequency != 0)
            {
                randomPosition(change);
                lastCreatedBig = Time.time;
                GameObject bigAsteroid = Instantiate(bigAsteroids, position, this.transform.rotation) as GameObject;
            }
            if (Time.time - lastCreatedPickUp > pickUpFrequency && pickUpFrequency != 0)
            {
                randomPickupPosition();
                lastCreatedPickUp = Time.time;
                GameObject pickUp = Instantiate(pickUps[Random.Range(0, pickUps.Length)], position, this.transform.rotation) as GameObject;
            }
            if (PLANETCOUNTER < numberOfPlanets)
            {
                randomPlanetPosition(change);
                GameObject asteroid = Instantiate(bgPlanets, position, this.transform.rotation) as GameObject;
                PLANETCOUNTER++;
            }
            change = !change;
        }
    }

    void randomPosition(bool n)
    {
        if (change)
        {
            position.y = 26;
            position.x = Random.Range(0, 40);
        }
        else
        {
            position.x = 40;
            position.y = Random.Range(0, 26);
        }
    }
    // Position function for planet is different then other objects as we dont want
    // planets to go out of screen, other small objects may
    void randomPlanetPosition(bool n)
    {
        if (change)
        {
            position.y = 32;
            position.x = Random.Range(5, 40);
        }
        else
        {
            position.x = 45;
            position.y = Random.Range(5, 26);
        }
    }
    // Pickups has a separate postioning function as they travel vertically rather than diagonally
    
    void randomPickupPosition()
    {
        position.y = Random.Range(5, 18);
        position.x = 45;
    }
}

