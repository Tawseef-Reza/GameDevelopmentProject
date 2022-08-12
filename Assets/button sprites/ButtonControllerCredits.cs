using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControllerCredits : MonoBehaviour
{
    private LevelDataScript _levelData;
    // Start is called before the first frame update
    void Start()
    {
        _levelData = GameObject.Find("LevelData").GetComponent<LevelDataScript>();
        _levelData.LevelNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoBackNow()
    {
        SceneManager.LoadScene("Other Main Menu");
    }
}
