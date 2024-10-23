using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private Camera mainCamera;
    private Transform transform;
    [SerializeField] private float margin;

    private void Start()
    {
        mainCamera = Camera.main;
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector2 screenPos = mainCamera.WorldToScreenPoint(transform.position);

        if (screenPos.x > Screen.width + margin)
        {
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(0 - margin, screenPos.y, 10));
        }
        else if (screenPos.x < margin)
        {
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width + margin, screenPos.y, 10));
        }
    }
}