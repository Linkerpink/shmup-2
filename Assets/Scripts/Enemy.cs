using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CameraManager cameraManager;
    private GameManager gameManager;

    private Rigidbody2D rb;
    private Vector3 randomPosition = Vector3.zero;
    [SerializeField] private float lerpSpeed = 0.025f;
    [SerializeField] private float moveSpeed = 1.0f;

    private bool initiated = false;

    [SerializeField] private bool movingRight = true;

    [SerializeField] private float hp = 2;

    [SerializeField] private GameObject explotion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraManager = GameObject.Find("Camera Manager").GetComponent<CameraManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent <GameManager>();
    }

    private void Update()
    {
        if (initiated)
        {
            MoveToScreenEdges();
        }
        else
        {
            InitiateEnemy();
        }

        if (hp <= 0)
        {
            cameraManager.ScreenShake(7.5f, 7.5f, 0.25f);
            gameManager.ControllerRumble(0.5f, 0.5f, 0.5f);

            GameObject _explotion = Instantiate(explotion, transform.position, Quaternion.identity);
            Destroy(_explotion, 5f);

            Destroy(gameObject);
        }
    }

    private void InitiateEnemy()
    {
        if (randomPosition == Vector3.zero)
        {
            randomPosition = new Vector3(Random.Range(-9.5f, 9.5f), transform.position.y, 0f);
        }

        transform.position = Vector3.Lerp(transform.position, randomPosition, lerpSpeed);


        if (transform.position == randomPosition)
        {
            initiated = true;
        }
    }

    private void MoveToScreenEdges()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (movingRight) 
        {
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime, 0f);
        }else if (!movingRight)
        {
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0f);
        }

        if (screenPos.x >= Screen.width)
        {
            movingRight = false;
        }

        if (screenPos.x <= 0)
        {
            movingRight = true;
        }
    }

    private IEnumerator Move()
    {

        yield return new WaitForSeconds(moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {   
            Destroy(collision.gameObject);
            hp -= 1;
        }
    }
}
