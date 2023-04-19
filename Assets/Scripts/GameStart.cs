using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart:MonoBehaviour
{
    public GameObject startPanel;
    public GameObject exitPanel;
    
    void Start()
    {
        startPanel.SetActive(true);
        exitPanel.SetActive(false);
    }

    public void ExitGame()
    {
        startPanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void No()
    {
        startPanel.SetActive(true);
        exitPanel.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }
    
    void Update()
    {
    }
}
