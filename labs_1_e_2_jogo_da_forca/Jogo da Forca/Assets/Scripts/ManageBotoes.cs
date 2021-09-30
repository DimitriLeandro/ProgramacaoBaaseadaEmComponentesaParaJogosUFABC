using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageBotoes : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("score", 0);
    }
    void Update()
    {
        
    }

    public void StartMundoGame()
    {
        SceneManager.LoadScene("Lab1");
    }
}
