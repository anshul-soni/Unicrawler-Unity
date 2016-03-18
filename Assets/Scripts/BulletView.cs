using UnityEngine;
using System.Collections;

public class BulletView : MonoBehaviour {

    public Sprite[] Ammo;
    private SpriteRenderer sprite;

	void Start () 
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        sprite.sprite = Ammo[Follow.bulletNumber];
	}
}
