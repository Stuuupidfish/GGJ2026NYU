using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int oxygenDepletion;
    public int OxygenDepletion
    {
        get {return oxygenDepletion;}
    }
    [SerializeField] private string enemyType;
    [SerializeField] private float speed; 
    private GameManager gameManager;

    private Rigidbody2D rb;
    private bool moveRight = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb.position.x < 0)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        float moveInput = moveRight ? 1f : -1f;
        float moveSpeed = speed;
        float downSpeed = gameManager.DownSpeed;
        Vector2 pos = rb.position;
        pos.x += moveInput * moveSpeed * Time.fixedDeltaTime;
        pos.y -= downSpeed;
        rb.MovePosition(pos);
    }
}
