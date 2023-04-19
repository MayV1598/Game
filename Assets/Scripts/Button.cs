using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string toLoad;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void Menu()
    {
        SceneManager.LoadScene(toLoad);
    }
}
