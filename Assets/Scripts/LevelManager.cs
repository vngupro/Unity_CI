using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] Levels;

    GameObject CurrentLevel;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    Transform SpawnPoint;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        LoadRandomLevel();
    }

    void LoadRandomLevel()
    {

        if(CurrentLevel != null)
        {
            Destroy(CurrentLevel);
        }

        int rand = Random.Range(0, Levels.Length);
        
        CurrentLevel = Instantiate(Levels[rand], Vector3.zero, Quaternion.identity);

        Player.transform.position = SpawnPoint.position;
    }

}
