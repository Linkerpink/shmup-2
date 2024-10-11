using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offset;

    void Update()
    {
        this.transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (this.transform.position.y + offset < Camera.main.ScreenToWorldPoint(Vector2.right * (float)Screen.width).y)
        {
            this.transform.position = Vector2.up * 20;
        }
    }
}
