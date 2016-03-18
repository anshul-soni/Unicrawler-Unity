using UnityEngine;
using System.Collections;

public class FinalScore : MonoBehaviour 
{
    private GUIText text;
    private UILabel label;
    private bool newHighScore;
    private int highScore;
    public static bool SHOWSCORE;
    private UILabel score;
	void Start () 
    {
        score = this.gameObject.GetComponent<UILabel>();
        newHighScore = false;
        if(PlayerPrefs.HasKey("Highscore"))
        {

        }else
        {

        }
	}
	
	
	void Update () 
    {
	    if(SHOWSCORE)
        {
            ShowScore();
        }
        if (this.gameObject.tag == "HighScore")
        {
            highScore = PlayerPrefs.GetInt("Highscore");
            score.text = "HIGHSCORE : " + highScore.ToString();
        }
	}

    void ShowScore()
    {
        SHOWSCORE = false;
        if (GameScript.GAMEOVER)
        {
            highScore = PlayerPrefs.GetInt("Highscore");
            if (highScore < GameScript.SCOREINT)
            {
                newHighScore = true;
            }
            if (this.gameObject.tag == "Score")
            {
                if (newHighScore)
                {
                    PlayerPrefs.SetInt("Highscore", GameScript.SCOREINT);
                    //GameScript.HIGHSCORE = GameScript.SCOREINT;
                    highScore = GameScript.SCOREINT;
                    score.text = "HIGHSCORE : " + GameScript.SCOREINT.ToString();
                }
                else
                {
                    score.text = "SCORE : " + GameScript.SCOREINT.ToString();
                }

            }
            
        }
    }

}
