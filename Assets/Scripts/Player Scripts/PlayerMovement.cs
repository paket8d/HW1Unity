using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameManagerScript gameSpeedController;
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 1f;
    [SerializeField] private float sideSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [Header("Jump")]
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private int _jumpsUsed;
    
    private Rigidbody _rigidbody;
    private Vector2 _movementInput;

    private void Awake()
    {        
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        gameObject.transform.position = new Vector3(0f, 2f, -20f);
    }
    
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * (forwardSpeed * gameSpeedController.CurrentGameSpeed * Time.deltaTime));
        SideMove(_movementInput);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (_jumpsUsed < maxJumps)
        {
            _jumpsUsed++;
            Jump();
        }
    }

    private void SideMove(Vector2 movementInput)
    {
        gameObject.transform.Translate(
            new Vector3(movementInput.x, 0f, 0f) * (sideSpeed * Time.deltaTime)
            );
    }

    private void Jump()
    {
        var v = _rigidbody.linearVelocity;
        v.y = 0f;
        _rigidbody.linearVelocity = v;
        
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _jumpsUsed = 0;
        }
    }
}
