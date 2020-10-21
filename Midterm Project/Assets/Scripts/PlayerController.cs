using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 1f;
    public float jumpHeight = 10f;
    public float glideTimeRemaining = 3f;
    public float glideRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

       // Vector2 movement = new Vector2(moveHorizontal, moveVertical) * moveSpeed * Time.deltaTime;
       // movement.Normalize();

       // rb.AddForce(movement);

       if(Input.GetKeyDown(KeyCode.Space))
       {
           Jump();
       }

       if(Input.GetKey(KeyCode.D))
       {
           rb.velocity = new Vector2(moveSpeed, 0);
       }

       if(Input.GetKey(KeyCode.A))
       {
           rb.velocity = new Vector2(-moveSpeed, 0);
       }

       if(Input.GetKey(KeyCode.Z))
       {
           Glide();
       }
    }
    
    void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpHeight), ForceMode2D.Impulse);
    }

    void Glide()
    {
        if (glideTimeRemaining > 0)
        {
                glideTimeRemaining -= Time.deltaTime;
                //rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y + glideRate));
                rb.velocity = new Vector2(rb.velocity.x, glideRate);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
