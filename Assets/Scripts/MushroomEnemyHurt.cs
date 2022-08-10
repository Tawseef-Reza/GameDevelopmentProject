using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemyHurt : MonoBehaviour
{
    private bool hurtingDone = true;
    private int numberOfLives = 3;
    private Animator _animation;
    private Transform Parent;

    public Transform[] path = new Transform[2];
    // Start is called before the first frame update
    void Start()
    {
        _animation = GameObject.Find("MushroomParent").GetComponent<Animator>();
        Parent = GameObject.Find("MushroomParent").GetComponent<Transform>();
        

    }

    // Update is called once per frame
    void Update()
    {
        //Parent.position = Vector2.MoveTowards(Parent.position)
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
        yield return new WaitForSeconds(0.333f);
        hurtingDone = true;
        _animation.SetBool("enemyHurt", false);

    }
}
