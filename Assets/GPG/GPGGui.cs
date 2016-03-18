using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.SocialPlatforms;
	
public class GPGGui : MonoBehaviour {
	
	private enum GPLoginState {loggedout, loggedin};
	private GPLoginState m_loginState = GPLoginState.loggedout;
	bool needFullSignin = false;
	private string dataToSave = "Hello World";

    private string testLeaderBoard = "CgkIgp2d3a8JEAIQBg";
    private string testAchievement = "< Unlock Achievement ID >";
    private string testIncAchievement = "< Incremental Achievement ID >";
    private int testIncACTotalSteps = 20;

    double currACPercent = -1;
    double onReportACPercent = 0;
    private int fingerID;
    private int fingerIDTouch;

	// Use this for initialization
	void Start () {
        Social.Active = new UnityEngine.SocialPlatforms.GPGSocial();
        Social.localUser.Authenticate(OnAuthCB);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(PlayerPrefs.HasKey("FIRSTPLAY"))
        {
            Social.ReportProgress("CgkIgp2d3a8JEAIQAQ", 0.0, OnUnlockAC);
        }
        if(!GameScript.PLAY && GameScript.SUBMITSCORE)
        {
            Social.ReportScore(GameScript.SCOREINT, testLeaderBoard, OnSubmitScore);
            GameScript.SUBMITSCORE = false;
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
                            if (touchHit.collider.gameObject.tag == "Leaderboards")
                            {
                                Debug.Log("leaderboard");
                                if (Social.localUser.authenticated)
                                    ShowLeaderBoard();
                                else
                                    Social.localUser.Authenticate(OnAuthCB);
                            }
                            if (touchHit.collider.gameObject.tag == "Achievements")
                            {
                                if (Social.localUser.authenticated)
                                    ShowAchievement();
                                else
                                    Social.localUser.Authenticate(OnAuthCB);
                            }
                        }
                        break;
                }
            }
        }
	}

    void OnAuthCB(bool result)
    {
        Debug.Log("GPGUI: Got Login Response: " + result);
    }

    public void OnLoadAC(IAchievement[] achievements)
    {
        Debug.Log("GPGUI: Loaded Achievements: " + achievements.Length);
    }

    public void OnLoadACDesc(IAchievementDescription[] acDesc)
    {
        Debug.Log("GPGUI: Loaded Achievement Description: " + acDesc.Length);
    }

    public void OnSubmitScore(bool result)
    {
        Debug.Log("GPGUI: OnSubmitScore: " + result);
    }

    public void OnSubmitAC(bool result)
    {
        Debug.Log("GPGUI: OnSubmitAchievement " + result);
        if (result == true) {
            NerdGPG.Instance().haveLoadedAc = false;
            Social.LoadAchievements(OnLoadAC);
        }
    }

    public void OnUnlockAC(bool result)
    {
        Debug.Log("GPGUI: OnUnlockAC " + result);
    }

	public void GPGAuthResult(string result)
	{
		// success/failed
		if(result == "success") {
			m_loginState = GPLoginState.loggedin;
		} else 
			m_loginState = GPLoginState.loggedout;
	}
	
	public void OnGPGCloudLoadResult(string result)
	{
		// result is in the format result;keyNum;length
		// where result is either success/conflict/error
		// keyNum is the key for which this result is 0-3 range as per GPG
		// length is the length of data received from GPG Cloud. Important for binary data handling
		// NOTE: In this code we are only saving/loading STRING data. but it should be fine to use it for any binary data
		Debug.Log("OnGPGCloudLoadResult "+result);
		string[] resArr = result.Split(';');
		if(resArr.Length<3)
		{
			Debug.LogError("Length of array after split is less than 3");	
			return; // weird stuff
		}
		int keyNum = System.Convert.ToInt16(resArr[1]);
		if(resArr[0]=="success") {
			// lets see what our data holds.
			byte[] data = NerdGPG.Instance().getKeyLoadedData(keyNum);
			string str = System.Text.Encoding.Unicode.GetString(data);
			Debug.Log("Data read for key "+ resArr[1] + " is " + str + " with len "+ resArr[2] + " and converted string length is "+ str.Length);
			dataToSave = str;
		}
	}
	
	public void OnGPGCloudSaveResult(string result)
	{
		// result is in the format result;keyNum
		// where result is either success/conflict/error
		// keyNum is the key for which this result is 0-3 range as per GPG
		
		Debug.Log("GPG CloudSaveResult "+result);
		string[] resArr = result.Split(';');
		if(resArr.Length<3)
		{
			Debug.LogError("Length of array after split is less than 3");	
			return; // weird stuff
		}

	}


    void ShowLeaderBoard()
    {
        NerdGPG.Instance().showLeaderBoards(testLeaderBoard);
    }

    void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }
}
