using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float bubbleTimer = 0f;
    [SerializeField] private GameObject[] enemies = new GameObject[5];
    //private Player player;
    private int[] spawnX = {-6,6};
    private bool damageSlowDown = false;
    public bool DamageSlowDown
    {
        get {return damageSlowDown;}
    }
    [SerializeField] private GameObject airBubble;
    [SerializeField] private GameObject bkg;
    private Vector2 lastSpawnPosition;
    private float downSpeed;
    public float DownSpeed
    {
        get {return downSpeed;}
    }
    private UI ui;

    private bool playerWins = false;
    public bool PlayerWins 
    {
        get {return playerWins;}
    }


    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UI>();
        spawnNewEnemy();
        downSpeed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        downSpeed = damageSlowDown ? 0.05f : 0.1f;
        if (!ui.IsGameOver && bkg.GetComponent<Rigidbody2D>().position.y >= -130)
        {
            bkg.GetComponent<Rigidbody2D>().position += new Vector2(0, -downSpeed);
            if (Vector2.Distance(bkg.GetComponent<Rigidbody2D>().position, lastSpawnPosition) >= 6f)
            {
                spawnNewEnemy();
            }
            // Bubble spawn timer
            bubbleTimer += Time.deltaTime;
            if (bubbleTimer >= 1.5f)
            {
                spawnAirBubble();
                bubbleTimer = 0f;
            }
        }
        else if (bkg.GetComponent<Rigidbody2D>().position.y < -130 && bkg.GetComponent<Rigidbody2D>().position.y >= -135.5)
        {
            playerWins = true;
            bkg.GetComponent<Rigidbody2D>().position += new Vector2(0, -downSpeed);
        }

        
    }

    //trigger the slowdown effect for 1 second
    public void TriggerSlowDown()
    {
        StartCoroutine(SlowDownCoroutine());
    }

    private IEnumerator SlowDownCoroutine()
    {
        damageSlowDown = true;
        yield return new WaitForSeconds(1.5f);
        damageSlowDown = false;
    }

    private void spawnNewEnemy()
    {
        GameObject newObject = Instantiate(enemies[Random.Range(0, 5)], new Vector2(Random.Range(-5,6), 6), Quaternion.identity);
        lastSpawnPosition = bkg.GetComponent<Rigidbody2D>().position;
    }

    private void spawnAirBubble()
    {
        GameObject bubble = Instantiate(airBubble, new Vector2(Random.Range(-2,3),6), Quaternion.identity);
    }
}
