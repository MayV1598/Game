using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public float timeStart = 10;
    public Text timerText;
    private Board board;
    public GameObject exitPanel;
    private ScoreManager scoreManager;
    
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        board = FindObjectOfType<Board>();
        timerText.text = timeStart.ToString();
    }

    public void LoseGame()
    {
        exitPanel.SetActive(true);
        PlayerPrefs.SetInt("records",scoreManager.score);
        PlayerPrefs.Save();
    }
    
    void Update()
    {
        if(board.currentState == GameState.wait)
        {
            timeStart = 10;
        }

        if(board.currentState != GameState.pause)
        {
            if(timeStart > 0)
            {
                timeStart -= Time.deltaTime;
                timerText.text = Mathf.Round(timeStart).ToString();
            }
            else
            {
                board.currentState = GameState.pause;
                LoseGame();
            }
        }
    }
}
