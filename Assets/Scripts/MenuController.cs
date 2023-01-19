using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

    private void Start()
    {
        _menu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _menu.SetActive(true);
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        _menu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
