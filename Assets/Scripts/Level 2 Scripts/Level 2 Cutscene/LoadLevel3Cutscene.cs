using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel3Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Level3Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Level3Cutscene()
    {
        yield return new WaitForSeconds(53);
        SceneManager.LoadScene("Level3_Victory");
    }
}
