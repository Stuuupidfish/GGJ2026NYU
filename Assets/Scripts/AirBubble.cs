using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubble : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        float downSpeed = gameManager.DownSpeed;
        Vector2 pos = rb.position;
        pos.y -= downSpeed;
        rb.MovePosition(pos);
    }
    
}
