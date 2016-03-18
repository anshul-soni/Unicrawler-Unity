using UnityEngine;
using System.Collections;

using System.Runtime.InteropServices;

public class GlobalVariables1 : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        if (!PlayerPrefs.HasKey("PREFS_SET"))
        {
            SET_PLAYER_PREFS();
        }
	}
	
	void Update () 
    {
	
	}

    void SET_PLAYER_PREFS()
    {
        PlayerPrefs.SetInt("ORBS", 0);

        PlayerPrefs.SetInt("SMALL_ASTEROIDS_DESTROYED", 0);

        PlayerPrefs.SetInt("BIG_ASTEROIDS_DESTROYED", 0);

        PlayerPrefs.SetInt("GOO_AMMO", 50);

        PlayerPrefs.SetInt("METEOR_AMMO", 30);

        PlayerPrefs.SetInt("LASER_BOT_KILLED", 0);

        PlayerPrefs.SetInt("FLY_BOT_KILLED", 0);

        PlayerPrefs.SetInt("EYE_BOT_KILLED", 0);

        PlayerPrefs.SetInt("MULTI_BOT_KILLED", 0);

        PlayerPrefs.SetInt("JELLY_BOT_KILLED", 0);


    }

    public void Goo100()
    {
        int OrbCount = PlayerPrefs.GetInt("ORBS");
        if (OrbCount < 100)
        {
            AndroidNative.showDialog("Failed", "Insufficient Orbs");
        }
        else
        {
            OrbCount -= 100;
            PlayerPrefs.SetInt("ORBS", OrbCount);
            int gooCount = PlayerPrefs.GetInt("GOO_AMMO");
            gooCount += 10;
            PlayerPrefs.SetInt("GOO_AMMO", gooCount);
            //AndroidNative.showDialog("Success", "Ammo Added");
            AndroidNative.showMessage("Success", "Ammo Added");
        }
        
    }

    public void Meteor100()
    {
        int OrbCount = PlayerPrefs.GetInt("ORBS");
        if (OrbCount < 100)
        {
            AndroidNative.showDialog("Failed", "Insufficient Orbs");
        }
        else
        {
            OrbCount -= 100;
            PlayerPrefs.SetInt("ORBS", OrbCount);
            int meteorCount = PlayerPrefs.GetInt("METEOR_AMMO");
            meteorCount += 3;
            PlayerPrefs.SetInt("METEOR_AMMO", meteorCount);
            AndroidNative.showDialog("Success", "Ammo Added");
        }
    }
}
