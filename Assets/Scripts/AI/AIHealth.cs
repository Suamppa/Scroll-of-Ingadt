using UnityEngine;

public class AIHealth : MonoBehaviour
{

    public int Health;
    // Max health of the target
    public int maxHealth = 5;

    void Start()
    {
        Health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
