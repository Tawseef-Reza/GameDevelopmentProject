using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour
{
    private LevelDataScript levelData;
    public enum GameState
    {
        Playing,
        TutorialWait,
        Killed,
        Settings
    }
    public GameState _currentGame = GameState.Playing;

    public GameObject SettingsButton;
    public GameObject MenuSettings;

    public Transform[] tutorialRespawns = new Transform[3];
    
    public GameObject playerPhysical;
    public PlayerController player;
    public Transform playerLocation;
    private Transform Checkpoint1;
    private Transform Checkpoint2;
    private Transform Checkpoint3;
    private Transform Checkpoint4;
    private bool firstCheckReached;
    private bool secondCheckReached;
    private bool thirdCheckReached;
    private bool fourthCheckReached;

    public GameObject restartButton;
    public GameObject firstCheckButton;
    //public TextMeshProUGUI firstText;
    private GameObject firstText;
    private GameObject firstOK;

    public GameObject secondCheckButton;
    private GameObject secondText;
    private GameObject secondOK;
    private GameObject secondTwoText;
    private GameObject secondTwoOK;

    public GameObject thirdCheckButton;
    private GameObject thirdText;
    private GameObject thirdOK;
    private GameObject thirdTwoText;
    private GameObject thirdTwoOK;


    public GameObject fourthCheckButton;
    public GameObject fourthText;
    public GameObject fourthOK;

    private TutorialGuy tutorialGuy;
    private GameObject firstTutorialGuyObject;

    // private CameraFollow _cameraObj;
    /*public GameObject lightSlashy;
    public GameObject heavySlashy;*/

    public GameObject panel;

    
    //private CameraFollow _camera;
    // Start is called before the first frame update
    void Start()
    {
        levelData = GameObject.FindWithTag("LevelData").GetComponent<LevelDataScript>();
        levelData.LevelNumber = 1;
        SettingsButton = GameObject.FindWithTag("Settings");
        MenuSettings = GameObject.Find("MenuSettings");

        Checkpoint1 = GameObject.Find("Checkpoint1").GetComponent<Transform>();
        Checkpoint2 = GameObject.Find("Checkpoint2").GetComponent<Transform>();
        Checkpoint3 = GameObject.Find("Checkpoint3").GetComponent<Transform>();
        Checkpoint4 = GameObject.Find("Checkpoint4").GetComponent<Transform>();
        firstCheckReached = false;
        secondCheckReached = false;
        thirdCheckReached = false;
        fourthCheckReached = false;
        playerPhysical = GameObject.FindWithTag("RealPlayer");
        player = playerPhysical.GetComponent<PlayerController>();
        playerLocation = playerPhysical.GetComponent<Transform>();

        restartButton = GameObject.Find("Restart");
        firstCheckButton = GameObject.Find("FirstCheckButton");
        firstText = GameObject.Find("FirstInstruction");
        firstOK = GameObject.Find("FirstOK");

        secondCheckButton = GameObject.Find("SecondCheckButton");
        secondText = GameObject.Find("SecondInstruction");
        secondOK = GameObject.Find("SecondOK");
        secondTwoText = GameObject.Find("SecondTwoInstruction");
        secondTwoOK = GameObject.Find("SecondTwoOK");

        thirdCheckButton = GameObject.Find("ThirdCheckButton");
        thirdText = GameObject.Find("ThirdInstruction");
        thirdOK = GameObject.Find("ThirdOK");
        thirdTwoText = GameObject.Find("ThirdTwoInstruction");
        thirdTwoOK = GameObject.Find("ThirdTwoOK");


        fourthCheckButton = GameObject.Find("FourthCheckButton");
        fourthText = GameObject.Find("FourthInstruction");
        fourthOK = GameObject.Find("FourthOK");

        firstTutorialGuyObject = GameObject.Find("TutorialGuyParent");
        tutorialGuy = firstTutorialGuyObject.GetComponent<TutorialGuy>();
        

        
        
        //_cameraObj = GameObject.Find("MainCamera").GetComponent<CameraFollow>();

        
        

        panel = GameObject.Find("Panel");

        

        
        firstCheckButton.SetActive(false);
        firstText.SetActive(false);
        firstOK.SetActive(false);

        secondCheckButton.SetActive(false);
        secondText.SetActive(false);
        secondOK.SetActive(false);
        secondTwoText.SetActive(false);
        secondTwoOK.SetActive(false);

        thirdCheckButton.SetActive(false);
        thirdText.SetActive(false);
        thirdOK.SetActive(false);
        thirdTwoText.SetActive(false);
        thirdTwoOK.SetActive(false);

        fourthCheckButton.SetActive(false);
        fourthText.SetActive(false);
        fourthOK.SetActive(false);
        restartButton.SetActive(false);
        panel.SetActive(false);

        MenuSettings.SetActive(false);

        
        //_camera = GameObject.Find("MainCamera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentGame)
        {
            case GameState.Playing:
                //print("Playing");
                Playing();
                break;
            case GameState.TutorialWait:
                //print("tutorialWait1");
                TutorialWait("idk");
                break;
            case GameState.Killed:
                Killed();
                break;
            case GameState.Settings:
                //print("Settings");
                Settings();
                break;
        }
        print(_currentGame);
    }
    //==Playing Functions==//
    private void Playing()
    {
        CheckCheckpoint();
        player.PlayerUpdate();
        /*heavySlashy.GetComponent<heavySlashScript>().heavyUpdate();
        lightSlashy.GetComponent<lightSlashScript>().lightUpdate();*/

    }
    private void CheckCheckpoint()
    {
        
        if (playerLocation.position.x >= Checkpoint1.position.x &&  playerLocation.position.x <= Checkpoint1.position.x + 3.5 && playerLocation.position.y <= Checkpoint1.position.y + 1 && (firstCheckReached == false))
        {
            firstCheckButton.SetActive(true);
        }
        else if (playerLocation.position.x >= Checkpoint2.position.x && playerLocation.position.x <= Checkpoint2.position.x + 1.75f && playerLocation.position.y <= Checkpoint1.position.y + 1 && (secondCheckReached == false) && (firstCheckReached == true))
        {
            secondCheckButton.SetActive(true);
        }
        else if (playerLocation.position.x >= Checkpoint3.position.x && playerLocation.position.x <= Checkpoint3.position.x + 2 && playerLocation.position.y <= Checkpoint3.position.y + 1 && (thirdCheckReached == false) && (secondCheckReached == true) && (firstCheckReached == true))
        {
            thirdCheckButton.SetActive(true);
            
        }
        else if (playerLocation.position.x >= Checkpoint4.position.x && playerLocation.position.x <= Checkpoint4.position.x + 2 && playerLocation.position.y <= Checkpoint4.position.y + 1 && (fourthCheckReached == false) && (thirdCheckReached == true) && (secondCheckReached == true) && (firstCheckReached == true))
        {
            fourthCheckButton.SetActive(true);
            
        }
        else
        {
            firstCheckButton.SetActive(false);
            secondCheckButton.SetActive(false);
            thirdCheckButton.SetActive(false);
            fourthCheckButton.SetActive(false);
        }
    }
    //==TutorialWait Functions==//
    private void TutorialWait(string checknumber)
    {
        
    }
    public void FirstButtonClick()
    {
        player._rigidbody2d.velocity = new Vector2(0, 0);
        player.BecomeIdle();
        firstCheckButton.SetActive(false);
        firstText.SetActive(true);
        firstOK.SetActive(true);
        _currentGame = GameState.TutorialWait;
    }
    public void FirstButtonOK()
    {
        firstText.SetActive(false);
        firstOK.SetActive(false);
        firstCheckReached = true;
        StartCoroutine(KillTutorialGuy("first"));
        
        player.GoBack();
        
        _currentGame = GameState.Playing;
        
    }

    public void SecondButtonClick()
    {
        player._rigidbody2d.velocity = new Vector2(0, 0);
        player.BecomeIdle();
        secondCheckButton.SetActive(false);
        secondText.SetActive(true);
        secondOK.SetActive(true);
        _currentGame = GameState.TutorialWait;
    }

    public void SecondButtonOK()
    { 
        
        secondText.SetActive(false);
        secondOK.SetActive(false);
        secondTwoText.SetActive(true);
        secondTwoOK.SetActive(true);
        
    }

    public void Second2ButtonOK()
    {
        secondTwoText.SetActive(false);
        secondTwoOK.SetActive(false);
        secondCheckReached = true;
        StartCoroutine(KillTutorialGuy("second"));

        player.GoBack();

        _currentGame = GameState.Playing;
    }

    public void ThirdButtonClick()
    {
        player._rigidbody2d.velocity = new Vector2(0, 0);
        player.BecomeIdle();
        thirdCheckButton.SetActive(false);
        thirdText.SetActive(true);
        thirdOK.SetActive(true);
        _currentGame = GameState.TutorialWait;
    }
    public void ThirdButtonOK()
    {
        thirdText.SetActive(false);
        thirdOK.SetActive(false);
        thirdTwoText.SetActive(true);
        thirdTwoOK.SetActive(true);
    }
    public void Third2ButtonOK()
    {
        thirdTwoText.SetActive(false);
        thirdTwoOK.SetActive(false);
        thirdCheckReached = true;
        StartCoroutine(KillTutorialGuy("third"));

        player.GoBack();

        _currentGame = GameState.Playing;

    }
    public void FourthButtonClick()
    {
        player._rigidbody2d.velocity = new Vector2(0, 0);
        player.BecomeIdle();
        fourthCheckButton.SetActive(false);
        fourthText.SetActive(true);
        fourthOK.SetActive(true);
        _currentGame = GameState.TutorialWait;
    }
    public void FourthButtonOK()
    {
        fourthText.SetActive(false);
        fourthOK.SetActive(false);
        fourthCheckReached = true;
        StartCoroutine(KillTutorialGuy("fourth"));

        player.GoBack();

        _currentGame = GameState.Playing;
    }

    
    private IEnumerator KillTutorialGuy(string stage)
    {
        switch (stage) {
            case "first":
                tutorialGuy.Disappear();
                yield return new WaitForSeconds(1);
                firstTutorialGuyObject.transform.position = tutorialRespawns[0].position;
                tutorialGuy.Reappear();
                yield return new WaitForSeconds(1.166f);
                break;
            case "second":
                tutorialGuy.Disappear();
                yield return new WaitForSeconds(1);
                firstTutorialGuyObject.transform.position = tutorialRespawns[1].position;
                tutorialGuy.Reappear();
                yield return new WaitForSeconds(1.166f);
                break;
            case "third":
                tutorialGuy.Disappear();
                yield return new WaitForSeconds(1);
                firstTutorialGuyObject.transform.position = tutorialRespawns[2].position;
                tutorialGuy.Reappear();
                yield return new WaitForSeconds(1.166f);
                break;
            case "fourth":
                tutorialGuy.Disappear();
                yield return new WaitForSeconds(1);
                firstTutorialGuyObject.transform.position = new Vector3(23.68f, -12.84f, 0);
                tutorialGuy.Reappear();
                yield return new WaitForSeconds(1.166f);
                break;
        }
        
        
        

    }
    //==Killed Functions==//

    private void Killed()
    {
        
    }
    /*private IEnumerator FinishDeath()
    {
        yield return new WaitForSeconds(1.081f);
        player._animator.SetBool("isDead", false);
        player._animator.SetBool("isGone", true);
        restartButton.SetActive(true);
        
        
    }*/
    public void restartLevel()
    {
        /*restartButton.SetActive(false);
        panel.SetActive(false);
        lightSlashy.SetActive(true);
        heavySlashy.SetActive(true);
        var newPlayer = Instantiate(playerPhysical, Checkpoint1.position, Quaternion.identity);
        Destroy(playerPhysical);
        playerPhysical = newPlayer;
        //_cameraObj.playerheading = newPlayer.GetComponent<Transform>().GetChild(1);
        player = newPlayer.GetComponent<PlayerController>();
        playerLocation = newPlayer.GetComponent<Transform>();*/
        SceneManager.LoadScene("Level 1");
        
        
        
    }
    //==Settings Functions==//
    private void Settings()
    {

    }
    public void SettingsButtonClick()
    {
        MenuSettings.SetActive(true);
        _currentGame = GameState.Settings;
        playerPhysical.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Other Main Menu");
    }
    public void ReturnToGame()
    {
        MenuSettings.SetActive(false);
        playerPhysical.SetActive(true);
        _currentGame = GameState.Playing;
    }
}
