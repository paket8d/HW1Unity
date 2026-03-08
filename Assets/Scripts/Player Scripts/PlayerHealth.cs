using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float respawnTime = 0.5f;
    public float Health { get => currentHealth; private set => currentHealth = value; }

    void Start()
    {
        Health = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0f)
        {
            Invoke(nameof(Die), respawnTime);
        }
    }

    private void Die()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
