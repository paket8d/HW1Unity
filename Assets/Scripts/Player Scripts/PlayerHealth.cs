using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private bool isInvulnerable = false;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float respawnTime = 0.5f;
    [Header("Animation Settings")]
    [SerializeField] private PlayerVFXAnimator vfx;
    public float CurrentHealth { get => currentHealth; private set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    private Coroutine _invulnerabilityCoroutine;
    private float _invulnerabilityCoroutineTime;

    void Start()
    {
        CurrentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        if (!isInvulnerable)
        {
            CurrentHealth -= damage;
            vfx.PlayDamage();
            if (CurrentHealth <= 0f)
            {
                Invoke(nameof(Die), respawnTime);
            }
        }
    }
    
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        if (!isInvulnerable)
        {
            vfx.PlayHeal();
        }
        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
    }
    
    public void MakeInvulnerable(float duration)
    {
        _invulnerabilityCoroutineTime = duration;
        if (_invulnerabilityCoroutine == null)
        {
            _invulnerabilityCoroutine = StartCoroutine(InvulnerabilityCoroutine());
        }
        
    }
    
    private IEnumerator InvulnerabilityCoroutine()
    {
        vfx.SetInvuln(true);
        isInvulnerable = true;
        while (_invulnerabilityCoroutineTime > 0f)
        {
            _invulnerabilityCoroutineTime -= Time.deltaTime;
            yield return null;
        }
        
        isInvulnerable = false;
        vfx.SetInvuln(false);
        _invulnerabilityCoroutine = null;
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
