using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SocialPlatforms;

public class PlayGameServices : MonoBehaviour 
{

    private string playerLabel = "Player disconnected";

    private int score = 0;



    private const string LEADERBOARD_NAME = "leaderboard_easy";


    private const string LEADERBOARD_ID = "CgkIi76JyM0bEAIQAA";

    public Dictionary<string, GPAchievement> achievements;

    void Start()
    {
        GooglePlayConnection.instance.addEventListener (GooglePlayConnection.PLAYER_CONNECTED, OnPlayerConnected);
		GooglePlayConnection.instance.addEventListener (GooglePlayConnection.PLAYER_DISCONNECTED, OnPlayerDisconnected);

		GooglePlayConnection.instance.addEventListener (GooglePlayConnection.CONNECTION_INITIALIZED, OnConnect);
		
		
		//listen for GooglePlayManager events
		GooglePlayManager.instance.addEventListener (GooglePlayManager.ACHIEVEMENT_UPDATED, OnAchivmentUpdated);
		GooglePlayManager.instance.addEventListener (GooglePlayManager.PLAYER_LOADED, OnPlayerInfoLoaded);
		GooglePlayManager.instance.addEventListener (GooglePlayManager.SCORE_SUBMITED, OnScoreSubmited);



        if (GooglePlayConnection.state == GPConnectionState.STATE_CONNECTED)
        {
            //checking if player already connected
            OnPlayerConnected();
        }

        GooglePlayConnection.instance.start(GooglePlayConnection.CLIENT_GAMES);

        //Load Achievements
        GooglePlayManager.instance.addEventListener(GooglePlayManager.ACHIEVEMENTS_LOADED, OnAchivmentsLoaded);
        GooglePlayManager.instance.loadAchivments();
    }

    private void OnConnect()
    {
        Debug.Log("OnConnect");
        GooglePlayConnection.instance.connect();
    }

    void Update()
    {
        if (PlayerPrefs.HasKey("FIRSTPLAY") && !GooglePlayManager.instance.achievements.ContainsKey("achievement_FirstPlay"))
        {   
            //GooglePlayManager.instance.reportAchievement("achievement_FirstPlay");

            ////Load Achivements again
            //GooglePlayManager.instance.addEventListener(GooglePlayManager.ACHIEVEMENTS_LOADED, OnAchivmentsLoaded);
            //GooglePlayManager.instance.loadAchivments();
        }
        if (!GameScript.PLAY && GameScript.SUBMITSCORE)
        {
            //Submit Score
            GooglePlayManager.instance.submitScore(LEADERBOARD_NAME, GameScript.SCOREINT);
            GameScript.SUBMITSCORE = false;
        }       
    }

    public void LeaderBoardClick()
    {
        if (GooglePlayConnection.state == GPConnectionState.STATE_CONNECTED)
        {
            GooglePlayManager.instance.showLeaderBoard(LEADERBOARD_NAME);
        }
        else
        {
            GooglePlayConnection.instance.connect();
        }
    }
    public void AchievementClick()
    {
        if (GooglePlayConnection.state == GPConnectionState.STATE_CONNECTED)
        {
            GooglePlayManager.instance.showAchivmentsUI();
        }
        else
        {
            GooglePlayConnection.instance.connect();
        }
    }

    private void OnAchivmentsLoaded(CEvent e)
    {
        GooglePlayManager.instance.removeEventListener(GooglePlayManager.ACHIEVEMENTS_LOADED, OnAchivmentsLoaded);
        GooglePlayResult result = e.data as GooglePlayResult;
        if (result.isSuccess)
        {
            AndroidNative.showMessage("OnAchivmentsLoaded", "Total Achivments: " + GooglePlayManager.instance.achievements.Count.ToString());
        }
        else
        {
            AndroidNative.showMessage("OnAchivmentsLoaded error: ", result.message);
        }

    }

    private void OnAchivmentUpdated(CEvent e)
    {
        GooglePlayResult result = e.data as GooglePlayResult;
        //AndroidNative.showMessage("OnAchivmentUpdated ", "Id: " + result.achievementId + "\n status: " + result.message);
    }


    private void OnLeaderBoardsLoaded(CEvent e)
    {
        GooglePlayManager.instance.removeEventListener(GooglePlayManager.LEADERBOARDS_LOEADED, OnLeaderBoardsLoaded);

        GooglePlayResult result = e.data as GooglePlayResult;
        if (result.isSuccess)
        {
            if (GooglePlayManager.instance.GetLeaderBoard(LEADERBOARD_ID) == null)
            {
                AndroidNative.showMessage("Leader boards loaded", LEADERBOARD_ID + " not found in leader boards list");
                return;
            }


            AndroidNative.showMessage(LEADERBOARD_NAME + "  score", GooglePlayManager.instance.GetLeaderBoard(LEADERBOARD_ID).GetScore(GPCollectionType.COLLECTION_PUBLIC, GPBoardTimeSpan.TIME_SPAN_ALL_TIME).ToString());
        }
        else
        {
            AndroidNative.showMessage("OnLeaderBoardsLoaded error: ", result.message);
        }
    }

    private void OnScoreSubmited(CEvent e)
    {
        GooglePlayResult result = e.data as GooglePlayResult;
        AndroidNative.showMessage("OnScoreSubmited", result.message);
    }

    private void OnPlayerInfoLoaded(CEvent e)
    {
        GooglePlayResult result = e.data as GooglePlayResult;

        if (result.isSuccess)
        {
            playerLabel = GooglePlayManager.instance.player.name;
        }
        else
        {
            playerLabel = "error: " + result.message;
        }
    }

    private void OnPlayerDisconnected()
    {
        playerLabel = "Player disconnected";
    }

    private void OnPlayerConnected()
    {
        GooglePlayManager.instance.loadPlayer();
    }


    void OnDestroy()
    {
        if (!GooglePlayConnection.IsDestroyed)
        {
            GooglePlayConnection.instance.removeEventListener(GooglePlayConnection.PLAYER_CONNECTED, OnPlayerConnected);
            GooglePlayConnection.instance.removeEventListener(GooglePlayConnection.PLAYER_DISCONNECTED, OnPlayerDisconnected);

        }

        if (!GooglePlayManager.IsDestroyed)
        {
            GooglePlayManager.instance.removeEventListener(GooglePlayManager.ACHIEVEMENT_UPDATED, OnAchivmentUpdated);
            GooglePlayManager.instance.removeEventListener(GooglePlayManager.PLAYER_LOADED, OnPlayerInfoLoaded);
            GooglePlayManager.instance.removeEventListener(GooglePlayManager.SCORE_SUBMITED, OnScoreSubmited);
        }

    }
}
