using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI : MonoBehaviour
{
    public bool IsGameOver = false;
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    private Player player;
    private GameManager gameManager;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject youWin;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip win;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        scoreText.text = "Oxygen level: 100%";
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Oxygen level: " + player.Oxygen + "%";
        if (player.Oxygen == 0)
        {
            GameOver();
        }
        
        if (gameManager.PlayerWins)
        {
            Win();
        }
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        IsGameOver = true;
    }
    public void Win()
    {
        audioSource.PlayOneShot(win);
        youWin.SetActive(true);
    }
}
