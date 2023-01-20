using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] 
    private int health = 10;
    [SerializeField]
    private int invincibilityFrameLoop = 5;
    
    private bool invincibility = false;
    private Renderer rend;
    
    private GameObject healthBar;
    private Slider healthBarSlider;
    
    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<Renderer>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        
        healthBarSlider =  healthBar.GetComponent<Slider>();
        healthBarSlider.maxValue = health;
    }

    // Update is called once per frame
    private void Update()
    {

    }
    
    public void TakeDamage(int damage)
    {
        if (invincibility) return;
        
        health -= damage;
        healthBarSlider.value = health;

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
