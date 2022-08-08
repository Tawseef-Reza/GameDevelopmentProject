using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float power = 2f;
    public float jumpPower = 10f;
    public Rigidbody2D _rigidbody2d;
    private Animator _animator;
    private SpriteRenderer _spriteRendy;
    private float direction;
    

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private bool hSlashingDone = true;
    private bool lSlashingDone = true; 
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRendy = GetComponent<SpriteRenderer>();
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
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpPower);
            //StartCoroutine(allowJump());
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
        else if (hSlashingDone == true && !Input.GetKeyDown(KeyCode.Mouse0))
        {
            _animator.SetBool("isHeavySlashing", false);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _animator.SetBool("isLightSlashing", true);

        }
        else if (lSlashingDone == true && !Input.GetKey(KeyCode.Mouse0))
        {
            _animator.SetBool("isLightSlashing", false);
        }



    }
    public void BecomeIdle()
    {
        _animator.SetBool("isWaitSpecial", true);
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isJumping", false);
        _animator.SetBool("isHeavySlashing", false);
        _animator.SetBool("isLightSlashing", false);
    }
    private IEnumerator allowLightSlash()
    {
        lSlashingDone = false;
        yield return new WaitForSeconds(0.5f);
        lSlashingDone = true;
    }
    private IEnumerator allowHeavySlash()
    {
        hSlashingDone = false;
        yield return new WaitForSeconds(0.75f);
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
