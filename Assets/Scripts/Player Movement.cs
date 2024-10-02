using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    [SerializeField] private float rotationSpeed = 0.01f;
    [SerializeField] private float rotationMultiplier = 10;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();

        controls.Player.Movement.performed += context => Movement(context.ReadValue<Vector2>());
        controls.Player.Movement.canceled += context => Movement(context.ReadValue<Vector2>());

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;

        //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, moveDirection.x * rotationMultiplier, 0), rotationSpeed);
        //transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(0,0,moveDirection.x * rotationMultiplier), rotationSpeed);
    }

    private void Movement(Vector2 _direction)
    {
        moveDirection = new Vector2(_direction.x, 0f);
    }
}