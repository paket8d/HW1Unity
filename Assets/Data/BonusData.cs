using UnityEngine;

[CreateAssetMenu(fileName = "BonusData", menuName = "Scriptable Objects/BonusData")]
public class BonusData : ScriptableObject
{
    [Header("Bonus Type")]
    [SerializeField] private BonusType bonusType = BonusType.Heal;
    [Header("Bonus Settings")]
    [SerializeField] private float amount = 20f;
    [SerializeField] private float duration = 5f;
    
    public BonusType BonusType => bonusType;
    public float Amount => amount;
    public float Duration => duration;
}
