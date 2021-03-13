using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text scoreText;
    public Text winText;
    public Text livesText;

    private int scoreValue = 0;
    
    private int score;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = scoreValue.ToString();

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

        
    }

    void Update()
    {
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
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Number of Collectibles Collected: " + score.ToString();
        
        if (score >= 5)
        {
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
        }
        
    }
}
