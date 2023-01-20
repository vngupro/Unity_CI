using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField]
    private int health = 5;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) 
        {
            //gameObject.tag = "Untagged";
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Bullet")
        {
            health -= 1;
            print("Hit: " + health);

        }
    }
}
