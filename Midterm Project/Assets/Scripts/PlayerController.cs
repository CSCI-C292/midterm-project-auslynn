using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    Collider2D groundCollider;
    GameObject player;
    public GameObject attack;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer attackRenderer;
    public Animator animator;
    public GameObject barrier;
    //public Canvas endCanvas;
    //public Canvas startCanvas;
    public Canvas canvas;
    public float _moveSpeed = 3f;
    public float _jumpHeight = 3f;
    public float _glideTimeRemaining = 3f;
    public float _glideRate = 5f;
    private bool isGrounded = true;
    private Vector3 startingLocation;
    private int coinCount = 0;
    private int isRunning;
    private bool flipVar;
    private bool isWinScreenOn;
    private bool isStartScreenOn;
    private Vector2 attackPosition;
    private Transform t;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        player = GetComponent<GameObject>();
        

        

        startingLocation = transform.position;
        if(SceneManager.GetActiveScene().name == "Scene1")
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
        isStartScreenOn = true;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
       {
           Jump();
       }

       if(Input.GetKeyDown(KeyCode.Mouse1) && isGrounded == false)//Q
       {
           attack.SetActive(true);
           attackRenderer.gameObject.SetActive(true);
       }
       
       if(Input.GetKey(KeyCode.Z))
       {
           Glide();
       }

       GlideReset();

       if(SceneManager.GetActiveScene().name == "Scene2" || SceneManager.GetActiveScene().name == "Scene3" || SceneManager.GetActiveScene().name == "Scene4")
       {
           if(coinCount == 3)
           {
               barrier.SetActive(false);
               coinCount = 0;
           }
       }

        if (canvas.gameObject.activeInHierarchy == true && SceneManager.GetActiveScene().name == "Scene4")
        {
            if(Input.GetButtonDown("Cancel")) //Esc
            {
                 canvas.gameObject.SetActive(false);
                 //Application.Quit();
                  UnityEditor.EditorApplication.isPlaying = false;
            }
            if(Input.GetButtonDown("Fire1")) //E
            {
                 canvas.gameObject.SetActive(false);
                 SceneManager.LoadScene("Scene1");
            }
        }

        if(canvas.gameObject.activeInHierarchy == true && SceneManager.GetActiveScene().name == "Scene1")
        {
            if(Input.GetButtonDown("Fire1")) //E
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.D))
       {
           rb.velocity = new Vector2(_moveSpeed, rb.velocity.y);
       }

       if(Input.GetKey(KeyCode.A))
       {
           rb.velocity = new Vector2(-_moveSpeed, rb.velocity.y);
       }

        if(rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
            flipVar = false;

            attackRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
            flipVar = true;

            attackRenderer.flipX = true;
            t.RotateAround(player.transform.position, new Vector3(0, 0, 1), 180);
        }
        else
        {
            spriteRenderer.flipX = flipVar;
        }

        if(rb.velocity.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        SceneChanger(other);

        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            attack.SetActive(false);
            attackRenderer.gameObject.SetActive(false);
        }

        if(other.gameObject.tag == "Enemy" && attack.activeInHierarchy == false)
        {
            //this.gameObject.SetActive(false);
            transform.position = startingLocation;
        }

        if(other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            coinCount++;
        }

        if(other.gameObject.name == "WinningCoin")
        {
            isWinScreenOn = true;
            canvas.gameObject.SetActive(true);
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

    void SceneChanger(Collider2D other)
    {
        if(other.name == "SceneChange1")
        {
            SceneManager.LoadScene("Scene2");
            startingLocation = transform.position;
        }

        if(other.name == "SceneChange2")
        {
            SceneManager.LoadScene("Scene3");
            startingLocation = transform.position;
        }

        if(other.name == "SceneChange3")
        {
            SceneManager.LoadScene("Scene4");
            startingLocation = transform.position;
        }
    }
    

   /* void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, _jumpHeight), ForceMode2D.Impulse);
    }
    */

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
