using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField]
    GameObject[] Levels;
    [SerializeField] private List<GameObject> enemies;

    GameObject CurrentLevel;
    private GameObject exitDoor;
    [SerializeField]
    GameObject Player;


    [SerializeField]
    Transform SpawnPoint;
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }

    private void Start()
    {
        LoadRandomLevel();
    }

    public void RemoveEnemyCount(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
        Debug.Log(enemies.Count);

        if (enemies.Count <= 0)
        {
            exitDoor.SetActive(false);
        }
    }

    public void LoadRandomLevel()
    {
        if(CurrentLevel != null)
        {
            Destroy(CurrentLevel);
        }

        int rand = Random.Range(0, Levels.Length);
        CurrentLevel = Instantiate(Levels[rand], Vector3.zero, Quaternion.identity);
        Player.transform.position = SpawnPoint.position;
    }

    public void SubscribeDoor(GameObject door)
    {
        exitDoor = door;
    }

    public void SubscribePlayer(GameObject player)
    {
        Player = player;
    }

    public void SubscribeEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
}
