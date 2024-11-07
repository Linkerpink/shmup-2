using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offset;

    private Camera secondCam;

    private void Awake()
    {
        secondCam = GameObject.Find("Second Cam").GetComponent<Camera>();
    }

    void Update()
    {
        this.transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (this.transform.position.y + offset < secondCam.ScreenToWorldPoint(Vector2.right * (float)Screen.width).y)
        {
            this.transform.position = Vector2.up * 20;
        }
    }
}
