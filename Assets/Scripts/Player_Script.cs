using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    public float jumpForce;

    public Text scoreText;
    public Text winText;
    public Text livesText;

    private int scoreValue = 0;
    
    private int score;
    private int lives;

    public AudioClip musicBackground;

    public AudioClip musicWin;

    public AudioSource musicSource;

    Animator anim;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = scoreValue.ToString();

        anim = GetComponent<Animator>();

        score = 0;
        lives = 3;

        SetScoreText();

        SetLivesText();

        winText.text = " ";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
          anim.SetInteger("State", 2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
          
          anim.SetInteger("State", 0);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            score = score + 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);
        }

        else if (collision.collider.tag == "Enemy")
        {
          lives = lives - 1; 
          SetLivesText();
          Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground"  && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Number of Collectibles Collected: " + score.ToString();
        
        if (score == 5)
        {
            transform.position = new Vector2(59.06f, 6.78f);
            lives = 3;
            SetLivesText();
        }

        else if (score >= 11)
        {
            musicSource.Stop();
            musicSource.clip = musicWin;
            musicSource.Play();
            winText.text = "You gotten them all! Game made by Gage Schroeder";
        }
        
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        
        if (lives <= 0)
        {
            Destroy(gameObject);
            winText.text = "You lost the game... Game made by Gage Schroeder";
            musicSource.Stop();
        }

    }

    void Flip()
    {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
    }
}
