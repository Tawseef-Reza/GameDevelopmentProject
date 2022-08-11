using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinEnemyHurt : MonoBehaviour
{
    private bool hurtingDone = true;
    private int numberOfLives = 3;
    private Animator _animator;
    private Transform Parent;
    private GameObject Parentobj;

    private GameObject playerObj;
    private Transform playerLocationGoblin;

    public Transform[] path = new Transform[2];
    private int index;
    public float goblinSpeed = 3;

    private enum goblinState
    {
        followingPath,
        attacking,
        beingHurt
    }
    private goblinState _currentGoblinState;
    private float deathAnimSpeed = 0.80f;
    private float hurtAnimSpeed = 0.333f;
    // Start is called before the first frame update
    void Start()
    {
        Parentobj = transform.parent.gameObject;
        _animator = Parentobj.GetComponent<Animator>();
        Parent = Parentobj.GetComponent<Transform>();
        index = 0;
        _currentGoblinState = goblinState.followingPath;

        playerObj = GameObject.FindWithTag("RealPlayer");
        playerLocationGoblin = playerObj.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(playerObj.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
        
        if (playerLocationGoblin.position.x > path[0].position.x && playerLocationGoblin.position.x < path[1].position.x && hurtingDone == true)
        {
            if (playerLocationGoblin.position.y > Parent.position.y + 1 || playerLocationGoblin.position.y < Parent.position.y - 1)
            {
                if (playerObj.GetComponent<Rigidbody2D>().velocity.x == 0 && playerObj.GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    _currentGoblinState = goblinState.followingPath;
                }
                else
                {
                    _currentGoblinState = goblinState.attacking;
                }
            }
            else 
            { 
                _currentGoblinState = goblinState.attacking; 
            }
            
        }
        else if (hurtingDone == true)
        {
            _currentGoblinState = goblinState.followingPath;
        }
        else if (hurtingDone == false)
        {
            _currentGoblinState = goblinState.beingHurt;
        }
        //Vector3(0.185363531,-0.0082502868,0)
        switch (_currentGoblinState) {
            case goblinState.followingPath:
                if ((index == 0 && Parent.localScale.x > 0) || (index == 1 && Parent.localScale.x < 0))
                {
                    Parent.localScale = new Vector3(-Parent.localScale.x, Parent.localScale.y, Parent.localScale.z);
                }
                _animator.SetBool("isAttacking", false);
                Parent.position = Vector3.MoveTowards(Parent.position, path[index].position, goblinSpeed * Time.deltaTime);
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
            case goblinState.attacking:
                if ((playerLocationGoblin.position.x < Parent.position.x && Parent.localScale.x > 0) || (playerLocationGoblin.position.x > Parent.position.x && Parent.localScale.x < 0))
                {
                    Parent.localScale = new Vector3(-Parent.localScale.x, Parent.localScale.y, Parent.localScale.z);
                    
                }
                if (Vector3.Distance(playerLocationGoblin.position, Parent.position) <= 2.3f)
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
                    Parent.position = Vector3.MoveTowards(Parent.position, new Vector3 (playerLocationGoblin.position.x, Parent.position.y, Parent.position.z), (goblinSpeed) * Time.deltaTime);
                    _animator.SetBool("isAttacking", false);
                }
                
                
                break;
            case goblinState.beingHurt:
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
