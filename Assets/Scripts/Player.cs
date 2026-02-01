using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    private GameManager gameManager;
    private Animator animator;
    private float oxygen;
    public int Oxygen
    {
        get { return Mathf.RoundToInt(oxygen); }
    }

    private UI ui;



    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        ui = FindObjectOfType<UI>();
        animator = gameObject.GetComponent<Animator>();
        rb.position = new Vector2(0, -3);
        oxygen = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        // Continuous oxygen drain
        float drainRate = 2f; // oxygen per second
        if (!gameManager.PlayerWins && !ui.IsGameOver)
        {
            if (oxygen > 0)
            {
                oxygen -= drainRate * Time.deltaTime;
                if (oxygen < 0)
                    oxygen = 0;
            }
        }
        if (oxygen > 100)
        {
            oxygen = 100;
        }
    

    }

    // Use FixedUpdate for physics-based movement
    void FixedUpdate()
    {
        float moveInput = 0f;
        if (oxygen != 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveInput = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveInput = 1f;
            }
        }
    
        float moveSpeed = 20f; 
        //new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.AddForce(new Vector2(moveInput * moveSpeed, rb.velocity.y));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Trigger");
            gameManager.TriggerSlowDown();
            Enemy enemy = other.GetComponent<Enemy>();
            if (oxygen - enemy.OxygenDepletion <= 0)
            {
                Debug.Log("Dead");
                oxygen = 0f;
                if (animator != null)
                {
                    animator.SetTrigger("Dead");
                }
            }
            else
            {
                oxygen -= enemy.OxygenDepletion;
                animator.SetTrigger("Hurt");
            }
        }
        if (other.gameObject.CompareTag("AirBubble"))
        {
            if (oxygen + 10 >= 100)
            {
                oxygen = 100f;
            }
            else
            {
                oxygen += 10f;
            }
            Destroy(other.gameObject);
        }
    }
}
