using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletDelay = 0.1f;
    [SerializeField] private float bulletDestroyTime = 2.5f;

    private Controls controls;

    private IEnumerator _Shooting;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        controls = new Controls();

        controls.Player.Shoot.performed += context => Shoot();
        controls.Player.Shoot.canceled += context => StopShoot();
        _Shooting = Shooting();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if (gameManager.paused)
        {
            StopShoot();
        }
    }

    private void Shoot()
    {
        if (!gameManager.paused)
        {
            StartCoroutine(_Shooting);
        }
    }

    private void StopShoot()
    {
        StopCoroutine(_Shooting);
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            GameObject _bullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(_bullet, bulletDestroyTime);
            yield return new WaitForSeconds(bulletDelay);
        }
    }
}
