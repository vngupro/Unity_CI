using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 20.0f;
    [SerializeField]
    private float radius = 5;  
    
    [SerializeField]
    private float lookSpeed = 1;

    private Rigidbody rb;
    private SphereCollider playerRadiusDetection;
    
    [SerializeField]
    private List<GameObject> enemiesInRange = new List<GameObject>();

    [SerializeField]
    private List<GameObject> AllGuns = new List<GameObject>();

    [SerializeField]
    private int amountOfGuns = 1;
    
    [SerializeField]
    private int bulletSpeed = 500;

    [SerializeField]
    private int bulletBounce = 0;

    [SerializeField]
    private GameObject nearestEnemy;

    private void Awake()
    {
        LevelManager.instance.SubscribePlayer(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRadiusDetection = GetComponentInChildren<SphereCollider>();
        playerRadiusDetection.radius = radius;
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;

        rb.velocity = direction * speed * Time.fixedDeltaTime;

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z - 10f);
        // Make trembling collision do not use Translate
        //transform.Translate(direction * (speed * Time.deltaTime));
    }

    // Update is called once per frame
    private void Update()
    {
        //removing enemies from the list when they are destroyed:
        DetectEnemyInZone();
    }

    private void DetectEnemyInZone()
    {
        FindNearestEnemy();
        FocusNearestEnemy();
    }
    
    private void FindNearestEnemy()
    {
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (!enemiesInRange[i])
                enemiesInRange.RemoveAt(i);
        }

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

        Vector3 direction = Vector3.zero;
        if (nearestEnemy)
        {
            direction = nearestEnemy.transform.position - transform.position;
            nearestEnemy.GetComponent<EnemyFeedback>().SetFocus(true);
        }
        
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed).eulerAngles;
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

    private void OnCollisionEnter(Collision other)
    {
        // POWER UP 

        // Added shot
        if (other.transform.CompareTag("AddedShot"))
        {
            Debug.Log("PW picked up:  " + amountOfGuns);

            if (amountOfGuns >= 3)
            {
                amountOfGuns = 3;
            }
            else if (amountOfGuns >= 1)
            {
                amountOfGuns += 1;
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
        else if (other.transform.CompareTag("ShotSpeed"))
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
        else if (other.transform.CompareTag("AddedBounce"))
        {
            bulletBounce += 1;
            for (int i = 0; i < AllGuns.Count; i++)
            {
                AllGuns[i].GetComponent<PlayerGun>().bulletBounce = bulletBounce;
            }

            Debug.Log("PW picked up" + bulletBounce);
            Destroy(other.gameObject);
        }

        //PlayerSpeed
        else if (other.transform.CompareTag("PlayerSpeed"))
        {
            speed += 2.5f;
            Debug.Log("PW picked up");
            Destroy(other.gameObject);
        }

        //BiggerRange 
        else if (other.transform.CompareTag("BiggerRange"))
        {
            radius += 2.5f;
            playerRadiusDetection.radius = radius;

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

}
