using UnityEngine;

public class PlayerVFXAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    private static readonly int TakeHealHash = Animator.StringToHash("Heal");
    private static readonly int IsInvuln = Animator.StringToHash("IsInvulnerable");

    public void PlayDamage() => animator.SetTrigger(TakeDamage);
    public void PlayHeal() => animator.SetTrigger(TakeHealHash);
    public void SetInvuln(bool state) => animator.SetBool(IsInvuln, state);
}

