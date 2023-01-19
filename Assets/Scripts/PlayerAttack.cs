using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Range(0.1f,10f)]
    public float AttackSpeed;

    private float TimeBtwAttacks;

    private float Timer;

    [SerializeField]
    private GameObject Projectile;
    // Start is called before the first frame update
    void Start()
    {
        TimeBtwAttacks = 1 / AttackSpeed;
        Timer = TimeBtwAttacks;
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
            Timer = TimeBtwAttacks;
            Attack();
        }
    }

    void Attack()
    {
        //To link with annemy detection
        //Shoot the nearest ennemy
        Debug.Log("shoot");
    }
}
