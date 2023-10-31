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
        // �p�G���l����
        if (transform.childCount > 0)
        {
            // ���l���� ���O �I�����ͮ� ���Ŭ���(���_���l���Y)
            bool isCurrentBall = true;
            bool isKinematic = false;
            bool colliderEnabled = true;
            transform.GetChild(0).GetComponent<BallState>().Initialize(isCurrentBall, isKinematic, colliderEnabled);
            transform.GetChild(0).transform.parent = null;
        }
    }
}
