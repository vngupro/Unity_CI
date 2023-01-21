using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
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
    private PlayerController _playerController;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Time_Btw_Attacks = 1 / Attack_Speed;
        Timer = Time_Btw_Attacks;
    }

    // Update is called once per frame
    private void Update()
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

    private void Attack()
    { 
        //Shoot the nearest enemy
        Transform target = _playerController.GetNearestEnemy().transform;
        Vector3 vTargetPlayer = target.position - transform.position;
        Vector3 direction = vTargetPlayer.normalized;

        GameObject newProjectile = Instantiate(Projectile, transform.position + direction * 2, Quaternion.identity);
        Rigidbody rbProjectile = newProjectile.GetComponent<Rigidbody>();
        rbProjectile.AddForce(direction * BulletSpeed * 100);
    }
}
