using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 20.0f;
    [SerializeField]
    private float radius = 5;  
    
    [SerializeField]
    private float lookSpeed = 1;
    
    [SerializeField]
    private SphereCollider playerCollider;
    
    [SerializeField]
    private List<GameObject> enemiesInRange = new List<GameObject>();

    [SerializeField]
    private List<GameObject> AllGuns = new List<GameObject>();

    [SerializeField]
    private int amountOfGuns = 1;
    
    [SerializeField]
    private int bulletSpeed = 500;

    [SerializeField]
    private int bulletBounce = 1;


    [SerializeField]
    private GameObject nearestEnemy;

    // Start is called before the first frame update
    private void Start()
    {
        playerCollider = GetComponent<SphereCollider>();
        
        playerCollider.radius = radius;
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        DetectEnemyInZone();
    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);
    }

    private void DetectEnemyInZone()
    {
        FindNearestEnemy();
        FocusNearestEnemy();
        PowerUps();
    }
    
    private void FindNearestEnemy()
    {
        if (enemiesInRange.Count == 0)
        {
            nearestEnemy = null;
            return;
        }

        foreach (GameObject enemy in enemiesInRange)
        {
            if (!nearestEnemy)
            {
                nearestEnemy = enemy;
            }
            else
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <
                    Vector3.Distance(transform.position, nearestEnemy.transform.position))
                {
                    nearestEnemy = enemy;
                }
            }
        }

        //removing enemies from the list when they are destroyed:
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (!enemiesInRange[i])
                enemiesInRange.RemoveAt(i);
        }
    }

    private void FocusNearestEnemy()
    {
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy)
                enemy.GetComponent<EnemyFeedback>().SetFocus(false);
        }
        
        if (!nearestEnemy) return;
        
        Vector3 direction = nearestEnemy.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
        nearestEnemy.GetComponent<EnemyFeedback>().SetFocus(true);
    }
    
    public GameObject GetNearestEnemy()
    {
        return nearestEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
            enemiesInRange.Add(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        // POWER UP 

        // Added shot
        if (other.transform.CompareTag("AddedShot"))
        {
            Debug.Log("PW picked up");

            if (amountOfGuns >= 1)
            {
                amountOfGuns += 1;
            }
            if (amountOfGuns >= 3)
            {
                amountOfGuns = 3;
            }

        
            switch (amountOfGuns)
            {
                case 1:
                    AllGuns[0].SetActive(true);
                    break;
                case 2:
                    AllGuns[0].SetActive(false);
                    AllGuns[1].SetActive(true);
                    AllGuns[2].SetActive(true);
                    break;
                case 3:
                    AllGuns[0].SetActive(true);
                    AllGuns[1].SetActive(true);
                    AllGuns[2].SetActive(true);
                    break;
            }

            
            Destroy(other.gameObject);
        }

        //Shot Speed
        if (other.transform.CompareTag("ShotSpeed"))
        {
            Debug.Log("PW picked up");

            bulletSpeed += 500;
            
            for (int i = 0; i < AllGuns.Count; i++)
            {
                AllGuns[i].GetComponent<PlayerGun>().bulletSpeed = bulletSpeed;
            }

            Destroy(other.gameObject);
        }

        //AddedBounce
        if (other.transform.CompareTag("AddedBounce"))
        {
            for (int i = 0; i < AllGuns.Count; i++)
            {
                AllGuns[i].GetComponent<PlayerGun>().bulletBounce = bulletBounce;
            }

            Debug.Log("PW picked up");
            Destroy(other.gameObject);
        }

        //PlayerSpeed
        if (other.transform.CompareTag("PlayerSpeed"))
        {
            speed += 10;
            Debug.Log("PW picked up");
            Destroy(other.gameObject);
        }

        //BiggerRange 
        if (other.transform.CompareTag("BiggerRange"))
        {
            radius += 2.5f;
            Debug.Log("PW picked up");
            Destroy(other.gameObject);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyFeedback>().SetFocus(false);
            enemiesInRange.Remove(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void PowerUps() 
    {

        //Basically here i will cycle through all the guns.
        //Depending on how many times the power up has been picked up
        //It will change activate or unactivate some guns

        // 2 guns = left and right guns active, middle gun not active
        // 3 guns = all Active




        //Increase bulletSpeed



    }
}
