using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private MenuController _menu;
    
    private bool isGameOnPause = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isGameOnPause)
            {
                _menu.Resume();
                isGameOnPause = false;
            }
            else
            {
                _menu.PauseGame();
                isGameOnPause = true;
            }
        }
    }
}
