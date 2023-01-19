using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Range(0.1f,10f)]
    public float Attack_Speed;

    private float Time_Btw_Attacks;

    private float Timer;

    [SerializeField]
    private GameObject Projectile;

    [Range(0.1f, 10f)]
    public float BulletSpeed;
    private PlayerController PC;
    
    private void Awake()
    {
        PC = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Time_Btw_Attacks = 1 / Attack_Speed;
        Timer = Time_Btw_Attacks;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Timer = Time_Btw_Attacks;
            Attack();
        }
    }

    void Attack()
    {
        //To link with annemy detection
        //Shoot the nearest ennemy

        //waiting for nearestEnemy to become public
        Transform target = PC.GetNearestEnemy().transform;
        Vector3 direction = (target.position - transform.position).normalized;
        //Vector3 direction = new Vector3(0.5f,0, 0.5f).normalized;

        Rigidbody rb = Instantiate(Projectile, transform.position + direction * 2, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(direction* BulletSpeed*100);
        Debug.Log("shoot");
    }
}
