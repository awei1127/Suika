using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public float moveSpeed;
    private const float LEFT_EDGE = -1.95f;
    private const float RIGHT_EDGE = 1.95f;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            float newX = transform.position.x + moveSpeed;
            newX = Mathf.Min(newX, RIGHT_EDGE);
            transform.position = new Vector2(newX, transform.position.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float newX = transform.position.x - moveSpeed;
            newX = Mathf.Max(newX, LEFT_EDGE);
            transform.position = new Vector2(newX, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Release();
        }
    }

    void Release()
    {
        // 找 tag 為 Ball 的子物件
        string tag = "Ball";
        GameObject holdingBall = FindChildWithTag(this.gameObject, tag);

        // 如果手上有球
        if (holdingBall)
        {
            // 讓球的 重力 碰撞器生效 父級為空(切斷父子關係)
            bool isKinematic = false;
            bool colliderEnabled = true;
            holdingBall.GetComponent<BallState>().Initialize(BallStage.Current, isKinematic, colliderEnabled);
            holdingBall.transform.parent = null;
        }
    }

    // 取得符合 tag 的第一個子物件
    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        // 如果沒有子物件
        if (parent.transform.childCount == 0)
        {
            return null;
        }

        // 遍歷子物件 回傳第一個符合 tag 的 GameObject
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
