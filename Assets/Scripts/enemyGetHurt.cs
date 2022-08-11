using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGetHurt : MonoBehaviour
{
    private GameObject firstHeal;
    private bool hurtingDone = true;
    private int numberOfLives = 1;
    private Animator _animation;
    // Start is called before the first frame update
    void Start()
    {
        firstHeal = GameObject.Find("HealCollect1");
        _animation = GameObject.Find("CircleParent").GetComponent<Animator>();

        firstHeal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if (numberOfLives <= 0)
        {
            firstHeal.SetActive(true);
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
            numberOfLives -= 3;
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
