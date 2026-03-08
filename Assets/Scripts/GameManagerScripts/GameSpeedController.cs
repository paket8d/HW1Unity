using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    [Header("Game Speed Settings")]
    [SerializeField] private float currentGameSpeed = 1f;
    [SerializeField] private float speedIncreaseRate = 0.05f;
    [SerializeField] private float maxGameSpeed = 3f;
    
    public float CurrentGameSpeed { get => currentGameSpeed; private set => currentGameSpeed = value; }

    // Update is called once per frame
    void Update()
    {
        CurrentGameSpeed = Mathf.Min(maxGameSpeed, CurrentGameSpeed + speedIncreaseRate * Time.deltaTime);
    }
}
