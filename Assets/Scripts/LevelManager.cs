using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    [SerializeField]
    GameObject[] Levels;
    private int EnemyLeft;

    GameObject CurrentLevel;

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
        Player = GameObject.FindGameObjectWithTag("Player");
        LoadRandomLevel();
    }

    public void RemoveEnemyCount()
    {
        EnemyLeft--;
        if(EnemyLeft <= 0)
        {
            GameObject.Find("Porte").SetActive(false);
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
        EnemyLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

}
