using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private LevelDataScript levelData;
    // Start is called before the first frame update
    void Start()
    {
        levelData = GameObject.FindWithTag("LevelData").GetComponent<LevelDataScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ContinueGame()
    {
        if (levelData.LevelNumber == 0)
        {

        }
        else
        {
            SceneManager.LoadScene($"Level {levelData.LevelNumber}");
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credit Screen");
    }
    
}
