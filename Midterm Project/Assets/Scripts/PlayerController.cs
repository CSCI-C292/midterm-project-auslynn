using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    Collider2D groundCollider;
    public float _moveSpeed = 3f;
    public float _jumpHeight = 3f;
    public float _glideTimeRemaining = 3f;
    public float _glideRate = 1.50f;
    private bool isGrounded = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
       {
           Jump();
       }

       if(Input.GetKey(KeyCode.D))
       {
           rb.velocity = new Vector2(_moveSpeed, rb.velocity.y);
       }

       if(Input.GetKey(KeyCode.A))
       {
           rb.velocity = new Vector2(-_moveSpeed, rb.velocity.y);
       }

       if(Input.GetKey(KeyCode.Z))
       {
           Glide();
       }

       GlideReset();
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");

       if(Input.GetKeyDown(KeyCode.Space))
       {
           Jump();
       }

       if(Input.GetKey(KeyCode.D))
       {
           rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
       }

       if(Input.GetKey(KeyCode.A))
       {
           rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
       }

       if(Input.GetKey(KeyCode.Z))
       {
           Glide();
       }
        
    }
    */

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void GlideReset()
    {
        if(isGrounded == true)
        {
            _glideTimeRemaining = 3f;
        }
    }
    
    void Jump()
    {
        if(isGrounded == true)
        {
             rb.AddForce(new Vector2(rb.velocity.x, _jumpHeight), ForceMode2D.Impulse);
        }
    }

    void Glide()
    {
        if (_glideTimeRemaining > 0)
        {
                _glideTimeRemaining -= Time.deltaTime;
                //rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y + glideRate));
                rb.velocity = new Vector2(rb.velocity.x, _glideRate);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

}
