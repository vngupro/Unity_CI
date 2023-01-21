using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFeedback : MonoBehaviour
{
    private bool isFocused = false;
    
    private Renderer rend;
    
    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.white;
    }

    public void SetFocus(bool focus)
    {
        isFocused = focus;
        rend.material.color = isFocused ? Color.red : Color.white;
    }
}
