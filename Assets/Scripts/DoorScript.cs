using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.SubscribeDoor(gameObject);
    }
}
