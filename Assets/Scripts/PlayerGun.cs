using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerGun : MonoBehaviour
{

    public GameObject Bullet;
    public Transform GunTransform;
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
            //Shoot();
            Debug.Log("Shot fired");
        }
    }

    IEnumerator Shoot()
    {
        
        //Bullet.GetComponent<Rigidbody>().velocity = new Vector3(10,0,0);
        yield return new WaitForSeconds(.1f);
    }

    void SpawnBullet()
    {
        GameObject newObject = Instantiate(Bullet, GunTransform.transform.position, GunTransform.transform.rotation);
        //newObject.GetComponent<Rigidbody>().rotation = transform.rotation;
        //newObject.GetComponent<Rigidbody>().velocity = new Vector3(transform.rotation.x, 0, transform.rotation.z) * 10.0f;
    }
}
