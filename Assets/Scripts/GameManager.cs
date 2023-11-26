using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject titleScreen;
    private int score = 0;
    private float spawnRate = 1.0f;
    public bool isGameActive;
    public TextMeshProUGUI livesText;
    private int lives;
    public GameObject pauseScreen;
    private bool paused;

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);
        titleScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePaused();
        }
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;

        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
