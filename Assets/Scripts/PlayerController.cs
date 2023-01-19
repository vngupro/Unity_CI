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
    private SphereCollider playerCollider;
    
    [SerializeField]
    private List<GameObject> enemiesInRange = new List<GameObject>();
    
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
        if (!nearestEnemy)
        {
            return;
        }
        
        Vector3 direction = nearestEnemy.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Remove(other.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
