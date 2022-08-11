using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemyHurt : MonoBehaviour
{
    private bool hurtingDone = true;
    private int numberOfLives = 3;
    private Animator _animator;
    private Transform Parent;
    private GameObject Parentobj;

    private GameObject playerObj;
    private Transform playerLocationMushroom;

    public Transform[] path = new Transform[2];
    private int index;
    public float mushroomSpeed = 3;

    private enum mushroomState
    {
        followingPath,
        attacking,
        beingHurt
    }
    private mushroomState _currentMushroomState;
    // Start is called before the first frame update
    void Start()
    {
        Parentobj = GameObject.Find("MushroomParent1");
        _animator = Parentobj.GetComponent<Animator>();
        Parent = Parentobj.GetComponent<Transform>();
        index = 0;
        _currentMushroomState = mushroomState.followingPath;

        playerObj = GameObject.FindWithTag("RealPlayer");
        playerLocationMushroom = playerObj.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(playerObj.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
        
        if (playerLocationMushroom.position.x > path[0].position.x && playerLocationMushroom.position.x < path[1].position.x && hurtingDone == true)
        {
            if (playerLocationMushroom.position.y > Parent.position.y + 1 || playerLocationMushroom.position.y < Parent.position.y - 1)
            {
                if (playerObj.GetComponent<Rigidbody2D>().velocity.x == 0 && playerObj.GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    _currentMushroomState = mushroomState.followingPath;
                }
                else
                {
                    _currentMushroomState = mushroomState.attacking;
                }
            }
            else 
            { 
                _currentMushroomState = mushroomState.attacking; 
            }
            
        }
        else if (hurtingDone == true)
        {
            _currentMushroomState = mushroomState.followingPath;
        }
        else if (hurtingDone == false)
        {
            _currentMushroomState = mushroomState.beingHurt;
        }
        //Vector3(0.185363531,-0.0082502868,0)
        switch (_currentMushroomState) {
            case mushroomState.followingPath:
                if ((index == 0 && Parent.localScale.x > 0) || (index == 1 && Parent.localScale.x < 0))
                {
                    Parent.localScale = new Vector3(-Parent.localScale.x, Parent.localScale.y, Parent.localScale.z);
                }
                _animator.SetBool("isAttacking", false);
                Parent.position = Vector3.MoveTowards(Parent.position, path[index].position, mushroomSpeed * Time.deltaTime);
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
            case mushroomState.attacking:
                if ((playerLocationMushroom.position.x < Parent.position.x && Parent.localScale.x > 0) || (playerLocationMushroom.position.x > Parent.position.x && Parent.localScale.x < 0))
                {
                    Parent.localScale = new Vector3(-Parent.localScale.x, Parent.localScale.y, Parent.localScale.z);
                    
                }
                if (Vector3.Distance(playerLocationMushroom.position, Parent.position) <= 2.3f)
                {
                    /*if ((playerLocationMushroom.position.x < Parent.position.x && Parent.localScale.x > 0) || (playerLocationMushroom.position.x > Parent.position.x && Parent.localScale.x < 0))
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
                    Parent.position = Vector3.MoveTowards(Parent.position, new Vector3 (playerLocationMushroom.position.x, Parent.position.y, Parent.position.z), (mushroomSpeed) * Time.deltaTime);
                    _animator.SetBool("isAttacking", false);
                }
                
                
                break;
            case mushroomState.beingHurt:
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
                print("this dying code is running with " + numberOfLives + " lives");
                
                _animator.SetBool("isDead", true);
                StartCoroutine(finishDeath());
                
            }
            else 
            {
                print("this hurting code is running " + numberOfLives + " lives");
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
        yield return new WaitForSeconds(1.166f);
        print("parentdestroyed");
        Destroy(Parentobj);
    }
    private IEnumerator finishHurt()
    {
        hurtingDone = false;
        yield return new WaitForSeconds(0.333f);
        hurtingDone = true;
        _animator.SetBool("enemyHurt", false);

    }
}
