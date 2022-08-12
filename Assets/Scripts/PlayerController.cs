using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private GameStateScript gameyControl;

    private AudioSource _walkingAudio;
    private int levelToGoTo = 2;
    public Image[] heartArray = new Image[5];
    public int numberOfLives = 5;
    private int numberOfExtraJumps = 1;
    public float power = 2f;
    public float jumpPower = 10f;
    public Rigidbody2D _rigidbody2d;
    public Animator _animator;
    public SpriteRenderer _spriteRendy;
    public float direction;
    
    
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    

    private bool hSlashingDone = true;
    //private bool lSlashingDone = true;
    private bool dashingDone = true;
    private bool hurtingDone = true;

    private int activeHeart;
    /*public GameObject lightSlash;
    public Transform lightSlashTransform;
    public GameObject heavySlash;
    public Transform heavySlashTransform;*/

    //private CheckPointData1 checkpointStuff;

    // Start is called before the first frame update
    void Start()
    {
        _walkingAudio = transform.GetChild(5).GetComponent<AudioSource>();
        // transform.position = checkpointStuff;
        gameyControl = GameObject.Find("GameStateController").GetComponent<GameStateScript>();
        
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRendy = GetComponent<SpriteRenderer>();

        numberOfLives = 5;

        _walkingAudio.enabled = false;
        _spriteRendy.color = Color.white;
    }

    // Update is called once per frame
    public void PlayerUpdate()
    {
        if ((_rigidbody2d.velocity.x > 0.9f || _rigidbody2d.velocity.x < -0.9f) && (_rigidbody2d.velocity.y < 1 && _rigidbody2d.velocity.y > -1))
        {
            _walkingAudio.enabled = true;
        }
        else
        {
            _walkingAudio.enabled = false;
        }
        switch (numberOfLives)
        {
            case 5:
                heartArray[0].color = new Color32(255, 255, 255, 255);
                heartArray[1].color = new Color32(255, 255, 255, 255);
                heartArray[2].color = new Color32(255, 255, 255, 255);
                heartArray[3].color = new Color32(255, 255, 255, 255);
                heartArray[4].color = new Color32(255, 255, 255, 255);
                break;
            case 4:
                heartArray[0].color = new Color32(255, 255, 255, 255);
                heartArray[1].color = new Color32(255, 255, 255, 255);
                heartArray[2].color = new Color32(255, 255, 255, 255);
                heartArray[3].color = new Color32(255, 255, 255, 255);
                heartArray[4].color = new Color32(0, 0, 0, 255);
                break;
            case 3:
                heartArray[0].color = new Color32(255, 255, 255, 255);
                heartArray[1].color = new Color32(255, 255, 255, 255);
                heartArray[2].color = new Color32(255, 255, 255, 255);
                heartArray[3].color = new Color32(0, 0, 0, 255);
                heartArray[4].color = new Color32(0, 0, 0, 255);
                break;
            case 2:
                heartArray[0].color = new Color32(255, 255, 255, 255);
                heartArray[1].color = new Color32(255, 255, 255, 255);
                heartArray[2].color = new Color32(0, 0, 0, 255);
                heartArray[3].color = new Color32(0, 0, 0, 255);
                heartArray[4].color = new Color32(0, 0, 0, 255);
                break;
            case 1:
                heartArray[0].color = new Color32(255, 255, 255, 255);
                heartArray[1].color = new Color32(0, 0, 0, 255);
                heartArray[2].color = new Color32(0, 0, 0, 255);
                heartArray[3].color = new Color32(0, 0, 0, 255);
                heartArray[4].color = new Color32(0, 0, 0, 255);
                break;
        }
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
        if (direction < 0f)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            _animator.SetBool("isRunning", true);
            _rigidbody2d.velocity = new Vector2(Input.GetAxis("Horizontal") * power, _rigidbody2d.velocity.y);
        }
        else if (direction > 0f)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
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
            
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpPower);
            //StartCoroutine(allowJump());
            print("jumpdefault");
        }
        else if (!Input.GetButtonDown("Jump") && isTouchingGround)
        {
            numberOfExtraJumps = 1;
            print("restored jump");
        }
        else if (Input.GetButtonDown("Jump") && !isTouchingGround && numberOfExtraJumps > 0)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpPower);
            numberOfExtraJumps -= 1;
            print("jump in air");
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
            
            /*lightSlash.SetActive(true);*/
            /*Invoke("SetLightFalse", 0.25f);*/



        }
        else if (/*lSlashingDone == true && */!Input.GetKey(KeyCode.Mouse0))
        {
            _animator.SetBool("isLightSlashing", false);

            /*lightSlash.SetActive(false);*/
        }

        if (Input.GetKeyDown(KeyCode.C) && dashingDone == true)
        {
            _animator.SetBool("isDashing", true);
            StartCoroutine(finishDash());
        }
        else if (Input.GetKeyDown(KeyCode.C) && dashingDone == false)
        {

        }
        else if (dashingDone == true && !Input.GetKeyDown(KeyCode.C))
        {
            _animator.SetBool("isDashing", false);
        }
        if (hurtingDone == true)
        {
            _animator.SetBool("isTakingDamage", false);
        }
        
        

    }
    /*private void SetLightFalse()
    {
        lightSlash.SetActive(false);
        print("invoke done");
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (numberOfLives == 1 && hurtingDone == true)
            {
                numberOfLives -= 1;
                DeadScene();
                
                
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
        else if (collision.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene($"Level {levelToGoTo}");
        }
        else if (collision.CompareTag("Heal"))
        {
            if (numberOfLives >= 5)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                numberOfLives += 1;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("Spike"))
        {
            DeadScene();
        }
    }
    private void DeadScene()
    {
        _rigidbody2d.velocity = new Vector2(0, 0);
        heartArray[0].color = new Color32(0, 0, 0, 255);
        heartArray[1].color = new Color32(0, 0, 0, 255);
        heartArray[2].color = new Color32(0, 0, 0, 255);
        heartArray[3].color = new Color32(0, 0, 0, 255);
        heartArray[4].color = new Color32(0, 0, 0, 255);
        gameyControl._currentGame = GameStateScript.GameState.Killed;
        gameyControl.SettingsButton.SetActive(false);
        gameyControl.restartButton.SetActive(true);
        gameyControl.panel.SetActive(true);
        _animator.SetBool("isDead", true);
        StartCoroutine(FinishDeath());
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
        _animator.SetBool("isLightSlashing", false);
        _animator.SetBool("isFalling", false);
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isAscending", false);
    }
    public void GoBack()
    {
        _animator.SetBool("isWaitSpecial", false);
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
        if (transform.localScale.x < 0) {
            yield return new WaitForSeconds(0.083f);
            transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y);
            yield return new WaitForSeconds(0.083f);
            transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y);
            yield return new WaitForSeconds(0.083f);
            dashingDone = true;
        }
        else
        {
            yield return new WaitForSeconds(0.083f);
            transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y);
            yield return new WaitForSeconds(0.083f);
            transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y);
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
        /*heavySlash.SetActive(true);*/
        yield return new WaitForSeconds(0.333f);
        /*heavySlash.SetActive(false);*/
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
