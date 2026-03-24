using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleData obstacleData;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(obstacleData.Damage);
        }
        Destroy(gameObject);
    }
}
