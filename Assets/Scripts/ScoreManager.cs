using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text end;
    public int score;

    void Start()
    {
        
    }
    
    void Update()
    {
        scoreText.text = "" + score;
        end.text = scoreText.text;
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
    }
}
