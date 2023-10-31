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
            // 讓子物件的 重力 碰撞器生效 父級為空(切斷父子關係)
            bool isCurrentBall = true;
            bool isKinematic = false;
            bool colliderEnabled = true;
            transform.GetChild(0).GetComponent<BallState>().Initialize(isCurrentBall, isKinematic, colliderEnabled);
            transform.GetChild(0).transform.parent = null;
        }
    }
}
