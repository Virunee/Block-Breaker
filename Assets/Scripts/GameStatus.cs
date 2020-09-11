using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // config params
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 10;
    [SerializeField] TextMeshProUGUI scoreText;

    // state variables
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        updateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

    }

    public void addPointsToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        updateScoreText();
    }

    public void updateScoreText()
    {
        scoreText.SetText(currentScore.ToString());
    }

    public void resetScore()
    {
        currentScore = 0;
        updateScoreText();
    }
}
