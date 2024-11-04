using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private float moveSpeed = 1f;
    
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    [SerializeField] private float rotationSpeed = 0.01f;
    [SerializeField] private float rotationMultiplier = 10;

    private Controls controls;

    public bool isGamepad;

    private bool canMove = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        controls = new Controls();

        controls.Player.Movement.performed += context => Movement(context.ReadValue<Vector2>());
        controls.Player.Movement.canceled += context => Movement(context.ReadValue<Vector2>());

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        if (canMove) 
        {
            rb.velocity = moveDirection * moveSpeed;
        }else
        {
            rb.velocity = Vector2.zero;
        }

        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, moveDirection.x * -rotationMultiplier), rotationSpeed);

        /*
        switch (moveDirection.x)
        {
            case -1:
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, rotationMultiplier), rotationSpeed);
                break;
            case 0:
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 0), rotationSpeed);
                break;
            case 1:
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, -rotationMultiplier), rotationSpeed);
                break;
        }
        */

        //Debug.Log(Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, moveDirection.x * -rotationMultiplier), rotationSpeed));
    }

    private void Movement(Vector2 _direction)
    {
        moveDirection = new Vector2(_direction.x, 0f);
    }

    public IEnumerator Freeze(float _freezeTime)
    { 
        canMove = false;
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(_freezeTime);
        spriteRenderer.color = Color.white;
        canMove = true;
    }

    //Input
    public void OnDeviceChange(PlayerInput _input)
    {
        isGamepad = _input.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}