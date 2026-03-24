using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPauseRouter : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed || !uiManager) return;
        uiManager.OnPause(context);
    }
}
