using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    private PlayerHealth player;
    private float time;
    private bool gameIsOver;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        gameIsOver = false;
        time = maxTime;
        Time.timeScale = 1f;
        player = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        TimeUpdate();

        if (time <= 1f && !gameIsOver)
            GameOver();
    }

    private void TimeUpdate()
    {
        time -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        gameIsOver = true;

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        if (player.CurrentHealth > 0)
            winText.SetActive(true);
        else
            loseText.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
