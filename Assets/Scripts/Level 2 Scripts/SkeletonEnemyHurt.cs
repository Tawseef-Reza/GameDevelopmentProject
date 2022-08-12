using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyHurt : MonoBehaviour
{
    private bool hurtingDone = true;
    private int numberOfLives = 3;
    private Animator _animator;
    private Transform Parent;
    private GameObject Parentobj;

    private GameObject playerObj;
    private Transform playerLocationBandit;

    public Transform[] path = new Transform[2];
    private int index;
    public float banditSpeed = 3;

    private enum banditState
    {
        followingPath,
        attacking,
        beingHurt
    }
    private banditState _currentBanditState;
    private float deathAnimSpeed = 0.5f;
    private float hurtAnimSpeed = 0.333f;
    // Start is called before the first frame update
    void Start()
    {
        Parentobj = transform.parent.gameObject;
        _animator = Parentobj.GetComponent<Animator>();
        Parent = Parentobj.GetComponent<Transform>();
        index = 0;
        _currentBanditState = banditState.followingPath;

        playerObj = GameObject.FindWithTag("RealPlayer");
        playerLocationBandit = playerObj.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(playerObj.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());

        if (playerLocationBandit.position.x > path[0].position.x && playerLocationBandit.position.x < path[1].position.x && hurtingDone == true)
        {
            if (playerLocationBandit.position.y > Parent.position.y + 1 || playerLocationBandit.position.y < Parent.position.y - 1)
            {
                if (playerObj.GetComponent<Rigidbody2D>().velocity.x == 0 && playerObj.GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    _currentBanditState = banditState.followingPath;
                }
                else
                {
                    _currentBanditState = banditState.attacking;
                }
            }
            else
            {
                _currentBanditState = banditState.attacking;
            }

        }
        else if (hurtingDone == true)
        {
            _currentBanditState = banditState.followingPath;
        }
        else if (hurtingDone == false)
        {
            _currentBanditState = banditState.beingHurt;
        }
        //Vector3(0.185363531,-0.0082502868,0)
        switch (_currentBanditState)
        {
            case banditState.followingPath:
                if ((index == 0 && Parent.localScale.x > 0) || (index == 1 && Parent.localScale.x < 0))
                {
                    Parent.localScale = new Vector3(-Parent.localScale.x, Parent.localScale.y, Parent.localScale.z);
                }
                _animator.SetBool("isAttacking", false);
                Parent.position = Vector3.MoveTowards(Parent.position, path[index].position, banditSpeed * Time.deltaTime);
                if (Parent.position == path[index].position)
                {
                    if ((index == 0 && Parent.localScale.x < 0) || (index == 1 && Parent.localScale.x > 0))
                    {
                        Parent.localScale = new Vector3(-Parent.localScale.x, Parent.localScale.y, Parent.localScale.z);
                    }
                    if (index == path.Length - 1)
                    {
                        index = 0;
                    }
                    else
                    {
                        index += 1;
                    }
                }
                break;
            case banditState.attacking:
                if ((playerLocationBandit.position.x < Parent.position.x && Parent.localScale.x > 0) || (playerLocationBandit.position.x > Parent.position.x && Parent.localScale.x < 0))
                {
                    Parent.localScale = new Vector3(-Parent.localScale.x, Parent.localScale.y, Parent.localScale.z);

                }
                if (Vector3.Distance(playerLocationBandit.position, Parent.position) <= 2.3f)
                {
                    /*if ((playerLocationBandit.position.x < Parent.position.x && Parent.localScale.x > 0) || (playerLocationBandit.position.x > Parent.position.x && Parent.localScale.x < 0))
                    {
                        Parent.localScale = new Vector3(-Parent.localScale.x, Parent.localScale.y, Parent.localScale.z);
                        _animator.SetBool("isAttacking", true);
                    }
                    else
                    {
                        _animator.SetBool("isAttacking", true);
                    }*/
                    _animator.SetBool("isAttacking", true);

                }
                else
                {
                    Parent.position = Vector3.MoveTowards(Parent.position, new Vector3(playerLocationBandit.position.x, Parent.position.y, Parent.position.z), (banditSpeed) * Time.deltaTime);
                    _animator.SetBool("isAttacking", false);
                }


                break;
            case banditState.beingHurt:
                break;

        }
        if (numberOfLives == 0)
        {
            _animator.SetBool("isDead", true);
            StartCoroutine(finishDeath());
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (hurtingDone == true && collision.CompareTag("lightSlash"))
        {
            numberOfLives -= 1;
            if (numberOfLives <= 0)
            {
                _animator.SetBool("isDead", true);
                StartCoroutine(finishDeath());
            }
            else
            {

                _animator.SetBool("enemyHurt", true);
                StartCoroutine(finishHurt());
            }
        }
        else if (collision.CompareTag("heavySlash") && hurtingDone == true)
        {
            numberOfLives -= 3;
            if (numberOfLives <= 0)
            {


                _animator.SetBool("isDead", true);
                StartCoroutine(finishDeath());

            }
            else
            {

                _animator.SetBool("enemyHurt", true);
                StartCoroutine(finishHurt());

            }
        }
        else if (hurtingDone == false)
        {

        }

    }
    private IEnumerator finishDeath()
    {
        yield return new WaitForSeconds(deathAnimSpeed);

        Destroy(Parentobj);
    }
    private IEnumerator finishHurt()
    {
        hurtingDone = false;
        yield return new WaitForSeconds(hurtAnimSpeed);
        hurtingDone = true;
        _animator.SetBool("enemyHurt", false);

    }
}


