using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    private LevelDataScript _levelData;
    // Start is called before the first frame update
    void Start()
    {
        _levelData = GameObject.Find("LevelData").GetComponent<LevelDataScript>();
        _levelData.LevelNumber = 0;
        StartCoroutine(GoBackFinalMain());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator GoBackFinalMain()
    {
        yield return new WaitForSeconds(31);
        SceneManager.LoadScene("Other Main Menu");
    }
}
