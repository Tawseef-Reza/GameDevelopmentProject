using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateScript : MonoBehaviour
{
    public int tutorialCheckpointsAvailable = 2;
    private enum GameState
    {
        Playing,
        TutorialWaitCheck1,
        Settings
    }
    private GameState _currentGame = GameState.Playing;
    private PlayerController player;
    private Transform playerLocation;
    private Transform Checkpoint1;
    private bool firstCheckReached;


    public GameObject firstCheckButton;
    //public TextMeshProUGUI firstText;
    private GameObject firstText;
    private GameObject firstOK;

    private TutorialGuy tutorialGuy;
    private GameObject firstTutorialGuyObject;


    //private CameraFollow _camera;
    // Start is called before the first frame update
    void Start()
    {
        Checkpoint1 = GameObject.Find("Checkpoint1").GetComponent<Transform>();
        firstCheckReached = false;
        player = GameObject.Find("PlayerMan").GetComponent<PlayerController>();
        playerLocation = GameObject.Find("PlayerMan").GetComponent<Transform>();

        firstCheckButton = GameObject.Find("FirstCheckButton");
        firstText = GameObject.Find("FirstInstruction");
        firstOK = GameObject.Find("FirstOK");

        tutorialGuy = GameObject.Find("TutorialGuy").GetComponent<TutorialGuy>();
        firstTutorialGuyObject = GameObject.Find("TutorialGuy");

        firstCheckButton.SetActive(false);
        firstText.SetActive(false);
        firstOK.SetActive(false);
        //_camera = GameObject.Find("MainCamera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentGame)
        {
            case GameState.Playing:
                print("Playing");
                Playing();
                break;
            case GameState.TutorialWaitCheck1:
                print("tutorialWait1");
                TutorialWait("check1");
                break;
            case GameState.Settings:
                print("Settings");
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
        
        
    }
    private void CheckCheckpoint()
    {
        
        if (playerLocation.position.x >= Checkpoint1.position.x && (firstCheckReached == false))
        {
            firstCheckButton.SetActive(true);
        }
        else
        {
            firstCheckButton.SetActive(false);
        }
    }
    //==TutorialWait Functions==//
    private void TutorialWait(string checknumber)
    {
        if (checknumber == "check1")
        {
            player.BecomeIdle();
            firstCheckButton.SetActive(false);
            firstText.SetActive(true);
            firstOK.SetActive(true);

        }
    }
    public void FirstButtonClick()
    {
        player._rigidbody2d.velocity = new Vector2(0, 0);
        _currentGame = GameState.TutorialWaitCheck1;
    }
    public void FirstButtonOK()
    {
        firstText.SetActive(false);
        firstOK.SetActive(false);
        firstCheckReached = true;
        StartCoroutine(KillTutorialGuy(firstTutorialGuyObject));
        tutorialGuy.Disappear();

        
        _currentGame = GameState.Playing;
        
    }

    private IEnumerator KillTutorialGuy(GameObject person)
    {
        yield return new WaitForSeconds(1);
        Destroy(person);
    }
    //==Settings Functions==//
    private void Settings()
    {

    }
    
}
