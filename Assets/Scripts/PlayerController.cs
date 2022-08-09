using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameStateScript gameyControl;
    

    public int numberOfLives = 3;
    private int numberOfExtraJumps = 1;
    public float power = 2f;
    public float jumpPower = 10f;
    public Rigidbody2D _rigidbody2d;
    public Animator _animator;
    private SpriteRenderer _spriteRendy;
    public float direction;
    
    
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    

    private bool hSlashingDone = true;
    //private bool lSlashingDone = true;
    private bool dashingDone = true;
    private bool hurtingDone = true;

    public GameObject lightSlash;
    public Transform lightSlashTransform;
    public GameObject heavySlash;
    public Transform heavySlashTransform;

    //private CheckPointData1 checkpointStuff;

    // Start is called before the first frame update
    void Start()
    {
       // transform.position = checkpointStuff;
        gameyControl = GameObject.Find("GameStateController").GetComponent<GameStateScript>();
        
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRendy = GetComponent<SpriteRenderer>();
        
        numberOfLives = 3;

        lightSlash = GameObject.FindWithTag("lightSlash");
        heavySlash = GameObject.FindWithTag("heavySlash");
        lightSlashTransform = lightSlash.GetComponent<Transform>();
        heavySlashTransform = heavySlash.GetComponent<Transform>();
        gameyControl.lightSlashy = lightSlash;
        gameyControl.heavySlashy = heavySlash;

        lightSlash.SetActive(false);
        heavySlash.SetActive(false);
        /*lightSlash.SetActive(false);
        heavySlash.SetActive(false);*/
        _spriteRendy.color = Color.white;
    }

    // Update is called once per frame
    public void PlayerUpdate()
    {
        
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
        if (direction < 0f)
        {
            
            _spriteRendy.flipX = true;
            _animator.SetBool("isRunning", true);
            _rigidbody2d.velocity = new Vector2(Input.GetAxis("Horizontal") * power, _rigidbody2d.velocity.y);
        }
        else if (direction > 0f)
        {
            _spriteRendy.flipX = false;
            _animator.SetBool("isRunning", true);
            _rigidbody2d.velocity = new Vector2(Input.GetAxis("Horizontal") * power, _rigidbody2d.velocity.y);
        }
        else
        {
            _animator.SetBool("isRunning", false);
            _rigidbody2d.velocity = new Vector2(0, _rigidbody2d.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            //print("making true code being ran");
            //_animator.SetBool("isAscending", true);
            numberOfExtraJumps = 1;
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpPower);
            //StartCoroutine(allowJump());
        }
        else if (!Input.GetButtonDown("Jump") && isTouchingGround)
        {
            numberOfExtraJumps = 1;
        }
        else if (Input.GetButtonDown("Jump") && !isTouchingGround && numberOfExtraJumps > 0)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpPower);
            numberOfExtraJumps -= 1;
        }
        else
        {
            
        }
        /*else if ((!Input.GetButtonDown("Jump") && isTouchingGround))
        {
            //print("making false code being ran");
            _animator.SetBool("isAscending", false);
        }*/
        if (_rigidbody2d.velocity.y < 0 && !isTouchingGround)
        {
            _animator.SetBool("isFalling", true);
            _animator.SetBool("isAscending", false);
        }
        else if (_rigidbody2d.velocity.y > 0 && !isTouchingGround)
        {
            _animator.SetBool("isAscending", true);
            _animator.SetBool("isFalling", false);
        }
        else
        {
            _animator.SetBool("isAscending", false);
            _animator.SetBool("isFalling", false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _animator.SetBool("isHeavySlashing", true);
            StartCoroutine(allowHeavySlash());
            
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && hSlashingDone == false)
        {

        }
        else if (hSlashingDone == true && !Input.GetKeyDown(KeyCode.Mouse1))
        {
            _animator.SetBool("isHeavySlashing", false);
            
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _animator.SetBool("isLightSlashing", true);
            lightSlash.SetActive(true);
            

        }
        else if (/*lSlashingDone == true && */!Input.GetKey(KeyCode.Mouse0))
        {
            _animator.SetBool("isLightSlashing", false);
            lightSlash.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.T) && dashingDone == true)
        {
            _animator.SetBool("isDashing", true);
            StartCoroutine(finishDash());
        }
        else if (Input.GetKeyDown(KeyCode.T) && dashingDone == false)
        {

        }
        else if (dashingDone == true && !Input.GetKeyDown(KeyCode.T))
        {
            _animator.SetBool("isDashing", false);
        }
        if (hurtingDone == true)
        {
            _animator.SetBool("isTakingDamage", false);
        }
        
        print(numberOfExtraJumps + " is the number of extra jumps");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (numberOfLives == 1 && hurtingDone == true)
            {
                _rigidbody2d.velocity = new Vector2(0, 0);
                gameyControl._currentGame = GameStateScript.GameState.Killed;
                gameyControl.restartButton.SetActive(true);
                gameyControl.panel.SetActive(true);
                _animator.SetBool("isDead", true);
                StartCoroutine(FinishDeath());
                
                
            }
            else if (hurtingDone == true)
            {
                numberOfLives -= 1;
                _animator.SetBool("isTakingDamage", true);
                StartCoroutine(finishHurt());
            }
            else if (hurtingDone == false)
            {

            }

        }
    }
    private IEnumerator FinishDeath()
    {
        yield return new WaitForSeconds(1.081f);
        _animator.SetBool("isDead", false);
        _animator.SetBool("isGone", true);
        
    }
    public void BecomeIdle()
    {
        _animator.SetBool("isWaitSpecial", true);
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isJumping", false);
        _animator.SetBool("isHeavySlashing", false);
        _animator.SetBool("isLightSlashing", false);
    }
    private IEnumerator finishHurt()
    {
        hurtingDone = false;
        yield return new WaitForSeconds(0.583f);
        hurtingDone = true;

    }
    private IEnumerator finishDash()
    {
        dashingDone = false;
        if (_spriteRendy.flipX == true) {
            yield return new WaitForSeconds(0.083f);
            transform.position = new Vector2(transform.position.x - 3, transform.position.y);
            yield return new WaitForSeconds(0.083f);
            transform.position = new Vector2(transform.position.x - 3, transform.position.y);
            yield return new WaitForSeconds(0.083f);
            dashingDone = true;
        }
        else
        {
            yield return new WaitForSeconds(0.083f);
            transform.position = new Vector2(transform.position.x + 3, transform.position.y);
            yield return new WaitForSeconds(0.083f);
            transform.position = new Vector2(transform.position.x + 3, transform.position.y);
            yield return new WaitForSeconds(0.083f);
            dashingDone = true;
        }
    }
    /*private IEnumerator allowLightSlash() // notneeded
    {
       // lSlashingDone = false;
        yield return new WaitForSeconds(0.166f);
        lightSlash.SetActive(true);
        yield return new WaitForSeconds(0.2083f);
        lightSlash.SetActive(false);
        //lSlashingDone = true;
    }*/
    private IEnumerator allowHeavySlash()
    {
        hSlashingDone = false;

        yield return new WaitForSeconds(0.333f);
        heavySlash.SetActive(true);
        yield return new WaitForSeconds(0.333f);
        heavySlash.SetActive(false);
        yield return new WaitForSeconds(0.0833f);
        hSlashingDone = true;
    }
    private IEnumerator allowJump()
    {
        //print("corountine being ran");
        float storeRadius = groundCheckRadius;
        groundCheckRadius = 0;
        yield return new WaitForSeconds(0.7f);
        groundCheckRadius = storeRadius;
        //print("corountine finished");
        
    }
}
