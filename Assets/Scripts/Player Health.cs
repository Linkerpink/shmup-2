using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private List<GameObject> healthIcons = new List<GameObject>();

    [SerializeField] private int hp = 3;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Bullet")
        {
            hp--;
        }
    }

    private void Die()
    {
        Debug.Log("you died!");
    }
}