using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateScript : MonoBehaviour
{
    public int tutorialCheckpointsAvailable = 2;
    public enum GameState
    {
        Playing,
        TutorialWaitCheck1,
        Killed,
        Settings
    }
    public GameState _currentGame = GameState.Playing;


    public GameObject playerPhysical;
    public PlayerController player;
    public Transform playerLocation;
    private Transform Checkpoint1;
    private bool firstCheckReached;

    public GameObject restartButton;
    public GameObject firstCheckButton;
    //public TextMeshProUGUI firstText;
    private GameObject firstText;
    private GameObject firstOK;

    private TutorialGuy tutorialGuy;
    private GameObject firstTutorialGuyObject;

    private CameraFollow _cameraObj;
    
    
    private GameObject panel;

    private Transform[] Checkpoints;
    //private CameraFollow _camera;
    // Start is called before the first frame update
    void Start()
    {
        Checkpoint1 = GameObject.Find("Checkpoint1").GetComponent<Transform>();
        firstCheckReached = false;
        playerPhysical = GameObject.Find("PlayerMan");
        player = playerPhysical.GetComponent<PlayerController>();
        playerLocation = playerPhysical.GetComponent<Transform>();

        restartButton = GameObject.Find("Restart");
        firstCheckButton = GameObject.Find("FirstCheckButton");
        firstText = GameObject.Find("FirstInstruction");
        firstOK = GameObject.Find("FirstOK");

        tutorialGuy = GameObject.Find("TutorialGuy").GetComponent<TutorialGuy>();
        firstTutorialGuyObject = GameObject.Find("TutorialGuy");

        _cameraObj = GameObject.Find("MainCamera").GetComponent<CameraFollow>();

        
        

        panel = GameObject.Find("Panel");

        Checkpoints = new Transform[] { Checkpoint1 };

        
        firstCheckButton.SetActive(false);
        firstText.SetActive(false);
        firstOK.SetActive(false);
        restartButton.SetActive(false);
        panel.SetActive(false);

        
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
            case GameState.TutorialWaitCheck1:
                //print("tutorialWait1");
                TutorialWait("check1");
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
        
        
    }
    private void CheckCheckpoint()
    {
        
        if (playerLocation.position.x >= Checkpoint1.position.x &&  playerLocation.position.x <= Checkpoint1.position.x + 3.5 && playerLocation.position.y <= Checkpoint1.position.y + 1 && (firstCheckReached == false))
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
    //==Killed Functions==//

    private void Killed()
    {
        restartButton.SetActive(true);
        panel.SetActive(true);
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
        restartButton.SetActive(false);
        panel.SetActive(false);
        var newPlayer = Instantiate(playerPhysical, Checkpoint1.position, Quaternion.identity);
        Destroy(playerPhysical);
        playerPhysical = newPlayer;
        _cameraObj.playerheading = newPlayer.GetComponent<Transform>().GetChild(1);
        player = newPlayer.GetComponent<PlayerController>();
        playerLocation = newPlayer.GetComponent<Transform>();
        
        _currentGame = GameState.Playing;
        
    }
    //==Settings Functions==//
    private void Settings()
    {

    }
    
}
