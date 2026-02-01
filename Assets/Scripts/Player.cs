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
    private Oxygen mask;

    [SerializeField] private Sprite dead;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip[] hurt;
    [SerializeField] private AudioClip die;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        mask = FindObjectOfType<Oxygen>();
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
        float drainRate = 3f; // oxygen per second
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
            //Debug.Log("Trigger");
            mask.CrackMask();
            mask.ShakeMask();
            Enemy enemy = other.GetComponent<Enemy>();
            if (!ui.IsGameOver && oxygen - enemy.OxygenDepletion <= 0)
            {
                //Debug.Log("Dead");
                oxygen = 0f;
                animator.SetTrigger("Hurt");
                animator.SetTrigger("Dead");
                audioSource.Stop();
                audioSource.PlayOneShot(die);
                GetComponent<SpriteRenderer>().sprite = dead;
            }
            else if (!ui.IsGameOver)
            {
                gameManager.TriggerSlowDown();
                oxygen -= enemy.OxygenDepletion;
                audioSource.PlayOneShot(hurt[Random.Range(0, 2)]);
                animator.SetTrigger("Hurt");
            }
            
        }
        if (other.gameObject.CompareTag("AirBubble"))
        {
            audioSource.PlayOneShot(pop);
            if (!ui.IsGameOver)
            {
                if (oxygen + 10 >= 100)
                {
                    oxygen = 100f;
                }
                else
                {
                    oxygen += 5f;
                }
            }
            Destroy(other.gameObject);
        }
    }

}
