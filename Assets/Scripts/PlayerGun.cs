using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerGun : MonoBehaviour
{

    public GameObject Bullet;
    public Transform GunTransform;
    public float bulletSpeed = 500;
    public int bulletBounce = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnBullet();
        if (Input.GetMouseButtonDown(0)) 
        {
            SpawnBullet();
            Debug.Log("Shot fired");
        }
    }

    void SpawnBullet()
    {
        GameObject newObject = Instantiate(Bullet, GunTransform.transform.position, GunTransform.transform.rotation);
        newObject.GetComponent<ProjectileMovements>().bulletSpeed = bulletSpeed;
        newObject.GetComponent<ProjectileMovements>().bouncingInt = bulletBounce;
    }
}
