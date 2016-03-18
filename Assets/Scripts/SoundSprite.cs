using UnityEngine;
using System.Collections;

public class SoundSprite : MonoBehaviour {

    public Sprite soundOn;
    public Sprite soundOff;
    private SpriteRenderer sprite;
    private int fingerID;
    private int fingerIDTouch;
	void Start () 
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = soundOn;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(GameScript.SOUND)
        {
            if(sprite.sprite != soundOn)
                sprite.sprite = soundOn;
        }else
        {
            if(sprite.sprite!= soundOff)
                sprite.sprite = soundOff;
        }

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                Touch touch = Input.touches[i];


                switch (touch.phase)
                {
                    case TouchPhase.Ended:
                        fingerID = touch.fingerId;
                        Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit touchHit;
                        if (Physics.Raycast(touchRay, out touchHit, 100))
                        {
                            if (touchHit.collider.gameObject.tag == "Sound")
                            {
                                GameScript.SOUND = !  GameScript.SOUND;
                            }
                        }
                        break;
                }
            }
        }
	}
}
