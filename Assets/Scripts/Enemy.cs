using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CameraManager cameraManager;

    private Rigidbody2D rb;
    private Vector3 randomPosition = Vector3.zero;
    [SerializeField] private float lerpSpeed = 0.025f;
    private float moveSpeed = 1.0f;

    private bool initiated = false;

    private float hp = 5;

    [SerializeField] private GameObject explotion;

    private enum EnemyTypes
    {
        Moving,
        Shooting,
    }

    [SerializeField] private EnemyTypes enemyType;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraManager = GameObject.Find("Camera Manager").GetComponent<CameraManager>();
    }

    private void Update()
    {
        if (initiated)
        {
            switch (enemyType)
            {
                case EnemyTypes.Moving:

                    break;

                case EnemyTypes.Shooting:

                    break;

                default:

                    break;
            }
        }
        else
        {
            InitiateEnemy();
        }

        if (hp <= 0)
        {
            cameraManager.ScreenShake(7.5f, 7.5f, 0.5f);

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

    private IEnumerator Move()
    {

        yield return new WaitForSeconds(moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (hp > 1)
            {
                cameraManager.ScreenShake(3f, 7.5f, 0.15f);
            }
            
            Destroy(collision.gameObject);
            hp -= 1;
        }
    }
}
