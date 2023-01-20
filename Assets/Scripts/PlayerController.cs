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

    //[SerializeField]
    //private List<GameObject> AllGuns = new List<GameObject>();

    [SerializeField]
    private int amountOfGuns = 1;

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


        // Try to make this so that only the capsule colider can trigger this
        // Because right now the bigge "Sphere" Colider triggers it             :/

        if (other.CompareTag("AddedShot"))
        {
            amountOfGuns += 1;
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
        //if (amountOfGuns == 2) 

        //if (gun.name == "MiddleGun");


        //Basically here i will cycle through all the guns.
        //Depending on how many times the power up has been picked up
        //It will change activate or unactivate some guns

        // 2 guns = left and right guns active, middle gun not active
        // 3 guns = all Active

    }
    
    //public void ClearChildren()
    //{
    //    //Debug.Log(transform.childCount);
    //    int i = 0;

    //    //Array to hold all child obj
    //    GameObject[] allChildren = new GameObject[transform.childCount];

    //    //Find all child obj and store to that array
    //    foreach (Transform child in transform)
    //    {
    //        allChildren[i] = child.gameObject;
    //        i += 1;
    //    }

    //    //Now destroy them
    //    foreach (GameObject child in allChildren)
    //    {
    //        DestroyImmediate(child.gameObject);
    //    }

    //    Debug.Log(transform.childCount);
    //}
}
