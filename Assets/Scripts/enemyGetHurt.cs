using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGetHurt : MonoBehaviour
{
    private bool hurtingDone = true;
    private int numberOfLives = 3;
    private Animator _animation;
    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (numberOfLives <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hurtingDone == true && collision.CompareTag("lightSlash"))
        {
            numberOfLives -= 1;
            _animation.SetBool("enemyHurt", true);
            StartCoroutine(finishHurt());
            
        }
        else if (collision.CompareTag("heavySlash") && hurtingDone == true)
        {
            numberOfLives -= 2;
            _animation.SetBool("enemyHurt", true);
            StartCoroutine(finishHurt());
            
        }
        else if (hurtingDone == false)
        {

        }
        
        
    }
    private IEnumerator finishHurt()
    {
        hurtingDone = false;
        yield return new WaitForSeconds(0.66f);
        hurtingDone = true;
        _animation.SetBool("enemyHurt", false);

    }
}
