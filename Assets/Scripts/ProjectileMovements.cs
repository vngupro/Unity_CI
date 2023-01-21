using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovements : MonoBehaviour
{
    public int bouncingInt = 0;
    private Rigidbody rb;
    private Vector3 velocity_Last_Frame;
    public float bulletSpeed = 500;
    public float LifeTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(rb.transform.forward * bulletSpeed);
    }

    private void LateUpdate()
    {
        velocity_Last_Frame = rb.velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(bouncingInt > 0)
        {
            Vector3 normal = collision.GetContact(0).normal;
            Vector3 dir = Vector3.Reflect(velocity_Last_Frame, normal);
            rb.velocity = dir;
            Debug.DrawLine(transform.position, transform.position + (dir * 5),Color.red,999999);
            bouncingInt--;
        }
        else
        {
            Destroy(gameObject);
        }
        Destroy(gameObject, LifeTime);
    }
}
