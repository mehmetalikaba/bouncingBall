using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class theBall : MonoBehaviour
{

    public float bounceForce = 200f;
    public float thePoint, highScore, rotationSpeed, rotationForce;

    public GameObject highScoreTextObject;
    public GameObject thePointTextObject;
    public GameObject yourPointTextEndObject;
    public GameObject thePointTextEndObject;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI thePointText;
    public TextMeshProUGUI yourPointTextEnd;
    public TextMeshProUGUI thePointTextEnd;

    Rigidbody2D rb;

    public float bounceTimer;
    public bool isGround, gameOver, gameStarted;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        highScore = PlayerPrefs.GetFloat("highScore", 0f);
        highScoreText.text = highScore.ToString();
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (isGround)
        {
            if (gameStarted)
            {
                thePoint += 1;
                thePointText.text = thePoint.ToString();

                bounceTimer += Time.deltaTime;
                if (bounceTimer > 0.5f)
                {
                    if (isGround)
                    {
                        bouncer();
                    }
                    bounceTimer = 0;
                }
            }
            else if (!gameStarted)
            {
                if (isGround)
                    bouncer();
            }
        }

        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

        if (transform.position.y >= 4.65f || transform.position.y <= -4.65f)
        {
            if (!gameOver)
            {
                gameOver = true;
                rotationSpeed = 0;
                bounceForce = 0;
                rotationForce = 0;
                rb.gravityScale = 0;
                thePointTextObject.SetActive(false);
                thePointTextEndObject.SetActive(true);
                yourPointTextEndObject.SetActive(true);
                thePointTextEnd.text = thePoint.ToString();
                if (thePoint > highScore)
                {
                    highScore = thePoint;
                    PlayerPrefs.SetFloat("highScore", highScore);
                    highScoreText.text = highScore.ToString();
                }

            }
        }
    }


    void FixedUpdate()
    {
        rb.AddTorque(rotationForce * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("groundda");
            isGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("groundda degil");
            isGround = false;
        }
    }

    void bouncer()
    {
        Debug.Log("zipladi");
        rb.AddForce(Vector2.up * bounceForce);
        isGround = false;
    }
}