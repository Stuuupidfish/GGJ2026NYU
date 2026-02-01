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
    [SerializeField] private GameObject gameOver;
    void Start()
    {
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
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        IsGameOver = true;
    }
}
