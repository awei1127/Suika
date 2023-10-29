using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public float moveSpeed;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(transform.position.x - moveSpeed, transform.position.y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Release();
        }
    }

    void Release()
    {
        // 如果有子物件
        if (transform.childCount > 0)
        {
            // 讓子物件受重力影響 並將其父級設為空(切斷父子關係)
            transform.GetChild(0).GetComponent<Rigidbody2D>().isKinematic = false;
            transform.GetChild(0).transform.parent = null;
        }
    }
}
