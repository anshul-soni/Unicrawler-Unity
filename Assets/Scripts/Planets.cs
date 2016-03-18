using UnityEngine;
using System.Collections;

public class Planets : MonoBehaviour {

    public Sprite[] planetSprites;

    private Vector3 DirectionVector;

	void Start () 
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = planetSprites[Random.Range(0, planetSprites.Length)];
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 18));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 18));
        DirectionVector = topRight - bottomLeft;
        DirectionVector = Vector3.Normalize(DirectionVector);
	}
	
	void Update () 
    {
        if(GameScript.PLAY)
            this.GetComponent<Rigidbody>().velocity = DirectionVector *-1;
        if(GameScript.PAUSE)
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}
