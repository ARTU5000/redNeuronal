using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiempo : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 10f;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 2f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 3f;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 4f;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale = 5f;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 6f;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Time.timeScale = 7f;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 8f;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 9f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Time.timeScale = 10f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale = 20f;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Time.timeScale = 30f;
        }
        
    }
}
