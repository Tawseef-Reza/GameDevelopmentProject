using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateScript1 : MonoBehaviour
{
    private LevelDataScript levelData;



    public enum GameState
    {
        Playing,
        Killed,
        Settings
    }
    public GameState _currentGame = GameState.Playing;

    public GameObject SettingsButton;
    public GameObject MenuSettings;

    
    
    public GameObject playerPhysical;
    public PlayerController1 player;
    public Transform playerLocation;

    public GameObject restartButton;
    public GameObject panel;

    
    
    // Start is called before the first frame update
    void Start()
    {
        levelData = GameObject.FindWithTag("LevelData").GetComponent<LevelDataScript>();
        levelData.LevelNumber = 2;





        playerPhysical = GameObject.FindWithTag("RealPlayer");
        player = playerPhysical.GetComponent<PlayerController1>();
        playerLocation = playerPhysical.GetComponent<Transform>();
        SettingsButton = GameObject.FindWithTag("Settings");
        MenuSettings = GameObject.Find("MenuSettings");
        restartButton = GameObject.FindWithTag("Restart");
        panel = GameObject.Find("Panel");
        
        /*SettingsButton = GameObject.FindWithTag("Settings");
        MenuSettings = GameObject.Find("MenuSettings");

        playerPhysical = GameObject.FindWithTag("RealPlayer");
        player = playerPhysical.GetComponent<PlayerController1>();
        playerLocation = playerPhysical.GetComponent<Transform>();

        restartButton = GameObject.FindWithTag("Restart");
        

        panel = GameObject.Find("Panel");
        
        */

        
        
        restartButton.SetActive(false);
        panel.SetActive(false);

        MenuSettings.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
        switch (_currentGame)
        {
            case GameState.Playing:
                
                Playing();
                break;
            
            case GameState.Killed:
                Killed();
                break;
            case GameState.Settings:
                
                Settings();
                break;
        }
        
    }
    //==Playing Functions==//
    private void Playing()
    {
        
        player.PlayerUpdate();
        

    }
    
    private void Killed()
    {
        
    }
    
    public void restartLevel()
    {
        
        SceneManager.LoadScene("Level 2");
        
        
        
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
