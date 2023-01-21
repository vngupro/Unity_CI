using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject Bullet;
    public Transform GunTransform;
    public float bulletSpeed = 500;
    public int bulletBounce = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        GameObject newBullet = Instantiate(Bullet, GunTransform.transform.position, GunTransform.transform.rotation);
        newBullet.GetComponent<ProjectileMovements>().bulletSpeed = bulletSpeed;
        newBullet.GetComponent<ProjectileMovements>().bouncingInt = bulletBounce;
    }
}
