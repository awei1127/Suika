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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Release();
        }
    }

    void Release()
    {
        // �� tag �� Ball ���l����
        string tag = "Ball";
        GameObject holdingBall = FindChildWithTag(this.gameObject, tag);

        // �p�G��W���y
        if (holdingBall)
        {
            // ���y�� ���O �I�����ͮ� ���Ŭ���(���_���l���Y)
            bool isCurrentBall = true;
            bool isKinematic = false;
            bool colliderEnabled = true;
            holdingBall.GetComponent<BallState>().Initialize(isCurrentBall, isKinematic, colliderEnabled);
            holdingBall.transform.parent = null;
        }
    }

    // ���o�ŦX tag ���Ĥ@�Ӥl����
    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        // �p�G�S���l����
        if (parent.transform.childCount == 0)
        {
            return null;
        }

        // �M���l���� �^�ǲĤ@�ӲŦX tag �� GameObject
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
