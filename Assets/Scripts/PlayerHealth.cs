using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] 
    private int health = 10;
    [SerializeField]
    private int invincibilityFrameLoop = 5;
    
    private bool invincibility = false;
    private Renderer rend;
    
    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (invincibility) return;
        
        health -= damage;
            
        if (health <= 0) 
            Die();
        else 
            StartCoroutine(Invincibility());
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator Invincibility()
    {
        invincibility = true;

        for (int i = 0; i < invincibilityFrameLoop; i++)
        {
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            rend.material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        
        invincibility = false;
    }
}
