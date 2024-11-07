using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement movement;

    [SerializeField] private List<GameObject> healthIcons = new List<GameObject>();

    [SerializeField] private int hp = 3;

    private bool invincible = false;
    private float spriteRendererTimer = 1f;
    private bool enableSpriteRenderer = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        foreach (GameObject healthIcon in healthIcons)
        {
            healthIcon.SetActive(false);

            for (int i = 0; i < hp; i++)
            {
                healthIcons[i].SetActive(true);
            }
        }

        if (hp <= 0)
        {
            Die();
        }

        //Player flashing
        if (invincible)
        {
            spriteRendererTimer -= 1f * Time.deltaTime * 10;

            if (spriteRendererTimer <= 0)
            {
                enableSpriteRenderer = !enableSpriteRenderer;
                spriteRendererTimer = 1f;
            }

            if (enableSpriteRenderer == true)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "Enemy Bullet")
            {
                hp--;
                StartCoroutine(Invincible(1f));
            }

            if (collision.gameObject.tag == "Freeze Bullet")
            {
                hp--;
                StartCoroutine(movement.Freeze(1));
                StartCoroutine(Invincible(1f));
            }

            if (collision.gameObject.tag == "Health Pickup")
            {
                if (hp < 3)
                {
                    hp++;
                }

                Destroy(collision.gameObject);
            }
        }
    }

    public IEnumerator Invincible(float _iSeconds)
    {
        invincible = true;
        yield return new WaitForSeconds(1f);
        invincible = false;
    }

    private void Die()
    {
        SceneManager.LoadScene("Death Screen");
    }
}