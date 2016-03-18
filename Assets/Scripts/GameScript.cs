using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour
{

    public static float HEALTH;
    public float smallAsteroidVelocity;
    public float bigAsteroidVelocity;
    public float smallAsteroidFrequency;
    public float bigAsteroidFrequency;
    public float pickUpFrequency;
    public static float SCORE;
    public static int SCOREINT;
    public static bool PLAY;
    public static bool PAUSE;
    public static float FUEL;
    public static bool SOUND;
    public static bool GAMEOVER;
    public Material normal;
    public GameObject HeathDeathSound;
    public GameObject blackHoleDeathSound;
    private bool start;
    private bool soundDone;
    private string backKey;

    private bool noAd;
    public GameObject HUDMENU;
    public GameObject MAINMENU;
    public GameObject PAUSEMENU;
    public GameObject GAMEOVERMENU;
    public GameObject HELP;
    public static bool SUBMITSCORE;

    //For GUI
    private GameObject SpaceShip;
    private Vector3 lastPosition;

    public UILabel scoreLabel;
    public UILabel highscoreLabel;
    public static int HELPCOUNTER;

    public static int OrbCounter;

    void Start()
    {
        HEALTH = 100;
        FUEL = 100;
        PLAY = false;
        PAUSE = false;
        OrbCounter = 0;
        AsteroidsMovement.VELOCITY_SmallAsteroid = smallAsteroidVelocity;
        BigAsteroid.VELOCITY_BigAsteroids = bigAsteroidVelocity;
        Creator.smallAsteroidFrequency = smallAsteroidFrequency;
        Creator.bigAsteroidFrequency = bigAsteroidFrequency;
        Creator.pickUpFrequency = pickUpFrequency;
        SOUND = true;
        //For GUI
        SpaceShip = GameObject.FindGameObjectWithTag("SpaceShip");
        start = false;

        MAINMENU.gameObject.SetActive(false);
        HUDMENU.gameObject.SetActive(false);
        PAUSEMENU.gameObject.SetActive(false);
        GAMEOVERMENU.gameObject.SetActive(false);

        Camera.main.transform.position = new Vector3(19, 82.5f, -10);
        backKey = "MainMenu";
        MAINMENU.gameObject.SetActive(true);
      

                
      
        if (PlayerPrefs.HasKey("Highscore"))
        {
  
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
        PlayerPrefs.SetInt("TUTORIALDONE", 1);
    }

    void Update() 
    {
        if(Time.time > 2 && !start)
        {
            start = true;
           
        }
        if (HEALTH <= 0)
        {
            Follow.FOLLOW = false;
            GAMEOVER = true;
            if (SOUND && !soundDone)
            {
                soundDone = true;
                GameObject sound = Instantiate(HeathDeathSound, Camera.main.transform.position, this.transform.rotation) as GameObject;
                Debug.Log("Played");
            }
            ShowScore();
            FinalScore.SHOWSCORE = true;
            SpaceShip.gameObject.GetComponent<Renderer>().enabled = false;
            Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(50, Screen.width/2, 18));
            SpaceShip.gameObject.transform.position = startPosition;
          
           
        }
        if (PLAY && !PAUSE && PlayerPrefs.HasKey("TUTORIALDONE"))
        {
            HEALTH -= (Time.deltaTime);
            if (AsteroidsMovement.VELOCITY_SmallAsteroid < 18)
                AsteroidsMovement.VELOCITY_SmallAsteroid += (Time.deltaTime / 200);
            if (BigAsteroid.VELOCITY_BigAsteroids < 8  )
                BigAsteroid.VELOCITY_BigAsteroids += (Time.deltaTime / 200);
            if (Creator.smallAsteroidFrequency > 1)
                Creator.smallAsteroidFrequency -= (Time.deltaTime / 500);
            if (Creator.bigAsteroidFrequency > 8)
                Creator.bigAsteroidFrequency -= (Time.deltaTime/8);
            SCORE += Time.deltaTime * 10;
            SCOREINT = (int)SCORE;
        }
        if(!GetComponent<AudioSource>().isPlaying && PLAY && SOUND)
        {
            GetComponent<AudioSource>().Play();
        }else
        {
            if(!PLAY || !SOUND)
            {
                GetComponent<AudioSource>().Stop();
            }
        }

        if (PLAY && !PlayerPrefs.HasKey("TUTORIALDONE"))
        {
            Camera.main.transform.position = new Vector3(19, 132.5f, -10);
            //TUTORIALHELP.gameObject.SetActive(true);
            if (HELPCOUNTER == 5)
            {
                //DONEBUTTON.gameObject.SetActive(true);
            }
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                    return;
            }
        }
    }

    void ShowScore()
    {
        PLAY = false;
        Camera.main.transform.position = new Vector3(19, 132.5f, -10);
        HELP.gameObject.SetActive(false);
        MAINMENU.gameObject.SetActive(false);
        HUDMENU.gameObject.SetActive(false);
        PAUSEMENU.gameObject.SetActive(false);
        GAMEOVERMENU.gameObject.SetActive(true);
        ClearScreen();
        
    }
    IEnumerator waitForSound()
    {
        yield return new WaitForSeconds(3.0f);
        Camera.main.transform.position = new Vector3(19, 82.5f, -10);
    }

    void ResetStats()
    {
        HEALTH = 100;
        FUEL = 100;
        PLAY = false;
        SCORE = 0;
        OrbCounter = 0;
        AsteroidsMovement.VELOCITY_SmallAsteroid = smallAsteroidVelocity;
        BigAsteroid.VELOCITY_BigAsteroids = bigAsteroidVelocity;
        Creator.smallAsteroidFrequency = smallAsteroidFrequency;
        Creator.bigAsteroidFrequency = bigAsteroidFrequency;
        Creator.pickUpFrequency = pickUpFrequency;
        soundDone = false;
    }

    void StartGame()
    {
        //Set Camera Postion
        //Set Space Ship Position
        //ResetStats;
        Camera.main.transform.position = new Vector3(19, 8, -10);
        SpaceShip.GetComponent<Renderer>().enabled = true;
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(50, Screen.height / 2, 18));
        SpaceShip.gameObject.transform.position = startPosition;
        GAMEOVER = false;
        ResetStats();
        ClearScreen();
        PLAY = true;
        PAUSE = false;
        SUBMITSCORE = true;
        noAd = true;
    }

    void ClearScreen()
    {

        GameObject[] SmallAsteroids = GameObject.FindGameObjectsWithTag("SmallAsteroids");
        GameObject[] BigAsteroids = GameObject.FindGameObjectsWithTag("BigAsteroids");
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemies");
        
        foreach (var asteroid in SmallAsteroids)
        {
            Destroy(asteroid);
        }
        foreach (var enemies in Enemies)
        {
            Destroy(enemies);
        }
        foreach (var asteroid in BigAsteroids)
        {
            Destroy(asteroid);
        }
    }

    void SoundChange()
    {
        SOUND = !SOUND;
    }

    public void GUIButtons(string button)
    {
        switch (button)
        {
            case "Play":
                PlayerPrefs.SetInt("FIRSTPLAY", 1);
                Follow.FOLLOW = true;
                MAINMENU.gameObject.SetActive(false);
                StartGame();
                if(PlayerPrefs.HasKey("TUTORIALDONE"))
                    HUDMENU.gameObject.SetActive(true);
                break;
            case "Sound":
                SoundChange();
                break;
            case "MainMenu":
                backKey = "MainMenu";
                MAINMENU.gameObject.SetActive(true);
                PAUSEMENU.gameObject.SetActive(false);
                HUDMENU.gameObject.SetActive(false);
                //CREDITS.gameObject.SetActive(false);
                HELP.gameObject.SetActive(false);
                GAMEOVERMENU.gameObject.SetActive(false);
                GAMEOVER = false;
                PAUSE = false;
                ResetStats();
                ClearScreen();
                
                Camera.main.transform.position = new Vector3(19, 82.5f, -10);
                break;
            case "Restart":
                Follow.FOLLOW = true;
                HUDMENU.gameObject.SetActive(true);
                PAUSEMENU.gameObject.SetActive(false);
                MAINMENU.gameObject.SetActive(false);
                //CREDITS.gameObject.SetActive(false);
                HELP.gameObject.SetActive(false);
                GAMEOVERMENU.gameObject.SetActive(false);
                GAMEOVER = false;
                PAUSE = false;
                ClearScreen();
                StartGame();
                break;
            case "Pause":
                backKey = "Pause";
                Follow.FOLLOW = false;
                PAUSEMENU.gameObject.SetActive(true);
                MAINMENU.gameObject.SetActive(false);
                HUDMENU.gameObject.SetActive(false);
                //CREDITS.gameObject.SetActive(false);
                HELP.gameObject.SetActive(false);
                GAMEOVERMENU.gameObject.SetActive(false);
                Debug.Log("pause");
                PAUSE = true;
                Camera.main.transform.position = new Vector3(19, 132.5f, -10);
                break;
            case "Resume":
                Follow.FOLLOW = true;
                Camera.main.transform.position = new Vector3(19, 8, -10);
                HUDMENU.gameObject.SetActive(true);
                PAUSEMENU.gameObject.SetActive(false);
                PAUSE = false;
                break;
            case "Help":
                SpaceShip.gameObject.GetComponent<Renderer>().enabled = false;
                HELP.gameObject.SetActive(true);
                MAINMENU.gameObject.SetActive(false);
                HUDMENU.gameObject.SetActive(false);
                //CREDITS.gameObject.SetActive(false);
                PAUSEMENU.gameObject.SetActive(false);
                GAMEOVERMENU.gameObject.SetActive(false);
                lastPosition = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(19, 8.0f, -10);
                break;
            case "Credits":
               // CREDITS.gameObject.SetActive(true);
                HELP.gameObject.SetActive(false);
                MAINMENU.gameObject.SetActive(false);
                HUDMENU.gameObject.SetActive(false);
                PAUSEMENU.gameObject.SetActive(false);
                GAMEOVERMENU.gameObject.SetActive(false);
                lastPosition = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(19, 132.5f, -10);
                break;
            case "Exit":
                Application.Quit();
                break;
            case "Back":
                if(backKey == "MainMenu")
                {
                    backKey = "MainMenu";
                    MAINMENU.gameObject.SetActive(true);
                    PAUSEMENU.gameObject.SetActive(false);
                    HUDMENU.gameObject.SetActive(false);
                    //CREDITS.gameObject.SetActive(false);
                    HELP.gameObject.SetActive(false);
                    GAMEOVER = false;
                    PAUSE = false;
                    ResetStats();
                    ClearScreen();
                    Camera.main.transform.position = new Vector3(19, 82.5f, -10);
                }else
                {
                    if(backKey == "Pause")
                    {
                        backKey = "Pause";
                        PAUSEMENU.gameObject.SetActive(true);
                        MAINMENU.gameObject.SetActive(false);
                        HUDMENU.gameObject.SetActive(false);
                        //CREDITS.gameObject.SetActive(false);
                        HELP.gameObject.SetActive(false);
                        GAMEOVERMENU.gameObject.SetActive(false);
                        Debug.Log("pause");
                        PAUSE = true;
                        Camera.main.transform.position = new Vector3(19, 132.5f, -10);
                    }
                }
                break;
            case "TutorialCompleted":
                Camera.main.transform.position = new Vector3(19, 82.5f, -10);
                break;
        }
    }

    public void Play()
    {
        PlayerPrefs.SetInt("FIRSTPLAY", 1);
        Follow.FOLLOW = true;
        MAINMENU.gameObject.SetActive(false);
        StartGame();
        if (PlayerPrefs.HasKey("TUTORIALDONE"))
            HUDMENU.gameObject.SetActive(true);
    }
    public void Sound()
    {
        SoundChange();
    }
    public void MainMenu()
    {
        backKey = "MainMenu";
        MAINMENU.gameObject.SetActive(true);
        PAUSEMENU.gameObject.SetActive(false);
        HUDMENU.gameObject.SetActive(false);
        //CREDITS.gameObject.SetActive(false);
        HELP.gameObject.SetActive(false);
        GAMEOVERMENU.gameObject.SetActive(false);
        GAMEOVER = false;
        PAUSE = false;
        ResetStats();
        ClearScreen();

        Camera.main.transform.position = new Vector3(19, 82.5f, -10);
    }
    public void Restart()
    {
        Follow.FOLLOW = true;
        HUDMENU.gameObject.SetActive(true);
        PAUSEMENU.gameObject.SetActive(false);
        MAINMENU.gameObject.SetActive(false);
        //CREDITS.gameObject.SetActive(false);
        HELP.gameObject.SetActive(false);
        GAMEOVERMENU.gameObject.SetActive(false);
        GAMEOVER = false;
        PAUSE = false;
        ClearScreen();
        StartGame();
    }
    public void Resume()
    {
        Follow.FOLLOW = true;
        Camera.main.transform.position = new Vector3(19, 8, -10);
        HUDMENU.gameObject.SetActive(true);
        PAUSEMENU.gameObject.SetActive(false);
        PAUSE = false;
    }
    public void Pause()
    {
        backKey = "Pause";
        Follow.FOLLOW = false;
        PAUSEMENU.gameObject.SetActive(true);
        MAINMENU.gameObject.SetActive(false);
        HUDMENU.gameObject.SetActive(false);
        //CREDITS.gameObject.SetActive(false);
        HELP.gameObject.SetActive(false);
        GAMEOVERMENU.gameObject.SetActive(false);
        Debug.Log("pause");
        PAUSE = true;
        Camera.main.transform.position = new Vector3(19, 132.5f, -10);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Help()
    {
        SpaceShip.gameObject.GetComponent<Renderer>().enabled = false;
        HELP.gameObject.SetActive(true);
        MAINMENU.gameObject.SetActive(false);
        HUDMENU.gameObject.SetActive(false);
        //CREDITS.gameObject.SetActive(false);
        PAUSEMENU.gameObject.SetActive(false);
        GAMEOVERMENU.gameObject.SetActive(false);
        lastPosition = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(19, 8.0f, -10);
    }
    public void Back()
    {
        if (backKey == "MainMenu")
        {
            backKey = "MainMenu";
            MAINMENU.gameObject.SetActive(true);
            PAUSEMENU.gameObject.SetActive(false);
            HUDMENU.gameObject.SetActive(false);
            //CREDITS.gameObject.SetActive(false);
            HELP.gameObject.SetActive(false);
            GAMEOVER = false;
            PAUSE = false;
            ResetStats();
            ClearScreen();
            Camera.main.transform.position = new Vector3(19, 82.5f, -10);
        }
        else
        {
            if (backKey == "Pause")
            {
                backKey = "Pause";
                PAUSEMENU.gameObject.SetActive(true);
                MAINMENU.gameObject.SetActive(false);
                HUDMENU.gameObject.SetActive(false);
                //CREDITS.gameObject.SetActive(false);
                HELP.gameObject.SetActive(false);
                GAMEOVERMENU.gameObject.SetActive(false);
                Debug.Log("pause");
                PAUSE = true;
                Camera.main.transform.position = new Vector3(19, 132.5f, -10);
            }
        }
    }

    public void ShowMainMenu()
    {
        MAINMENU.gameObject.SetActive(true);
    }
    public void HideMainMenu()
    {
        MAINMENU.gameObject.SetActive(false);
    }
}