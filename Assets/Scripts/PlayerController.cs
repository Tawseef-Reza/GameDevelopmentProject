using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float power = 2f;
    public float jumpPower = 6f;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    private SpriteRenderer _spriteRendy;
    private float direction;
    

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRendy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if ()
        {

        }
        else
        {

        }*/
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
            _animator.SetBool("isJumping", true);
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpPower);
            StartCoroutine(allowJump());
        }
        else if ((!Input.GetButtonDown("Jump") && isTouchingGround))
        {
            //print("making false code being ran");
            _animator.SetBool("isJumping", false);
        }
        
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
