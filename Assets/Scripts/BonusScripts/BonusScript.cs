using UnityEngine;

public class BonusScript : MonoBehaviour
{
    [SerializeField] private BonusData BonusData;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (BonusData.BonusType == BonusType.Heal && playerHealth != null)
        {
            playerHealth.Heal(BonusData.Amount);
        }
        else if (BonusData.BonusType == BonusType.Invulnerability && playerHealth != null)
        {
            playerHealth.MakeInvulnerable(BonusData.Duration);
        }
        Destroy(gameObject);
    }
}
