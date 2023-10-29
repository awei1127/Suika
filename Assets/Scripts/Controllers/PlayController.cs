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
            // ���l��������O�v�T �ñN����ų]����(���_���l���Y)
            transform.GetChild(0).GetComponent<Rigidbody2D>().isKinematic = false;
            transform.GetChild(0).transform.parent = null;
        }
    }
}
