using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    private void Start()
    {
        LevelManager.instance.SubscribeEnemy(gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
            Debug.Log("Hit: " + health);
        }

        if (health <= 0)
        {
            LevelManager.instance.RemoveEnemyCount(gameObject);
        }
    }
}
