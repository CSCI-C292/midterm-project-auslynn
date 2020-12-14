using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public int moveSpeed;
    public float switchInterval;
    public SpriteRenderer spriteRenderer;
    private float tempTime;
    private Rigidbody2D rb;
    private AudioSource hitSound;
    public AudioClip hitClip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tempTime = switchInterval;

        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(switchInterval > tempTime / 2)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
            switchInterval -= Time.deltaTime;
        }
        else if (switchInterval > 0)
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
            switchInterval -= Time.deltaTime;
        }
        else
        {
            switchInterval = tempTime;
        }

        if(rb.velocity.x >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Attack")
        {
           // Vector3 deathPosition = this.transform.position;
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
            this.gameObject.SetActive(false);
        }
    }
}