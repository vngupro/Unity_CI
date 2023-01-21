using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

    private void Start()
    {
        _menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
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
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}
