using UnityEngine;
using UnityEngine.SceneManagement;

public class theBall : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float bounceForce = 200f;
    public float rotationForce = 50f;

    Rigidbody2D rb;

    public float bounceTimer;
    public bool isGround, gameOver;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

        if (transform.position.y >= 4.6f || transform.position.y <= -4.6f)
        {
            gameOver = true;
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