using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    private UILabel score;
    public static bool ACTIVE;
	void Start () 
    {
        score = this.gameObject.GetComponent<UILabel>();
        //score.fontSize = Screen.height / 10;
        score.fontSize = 70;
	}
	
	void Update () 
    {
        score.text = GameScript.SCOREINT.ToString();
	}
}
