using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPortal : MonoBehaviour
{
    public GameObject door;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !door.activeInHierarchy)
        {
            LevelManager.instance.LoadRandomLevel();
            
        }
    }
}
