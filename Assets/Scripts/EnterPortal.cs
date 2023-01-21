using UnityEngine;

public class EnterPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.instance.LoadRandomLevel();
        }
    }
}
