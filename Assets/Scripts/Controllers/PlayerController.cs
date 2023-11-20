using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float LEFT_BOX_EDGE = -1.95f;
    private const float RIGHT_BOX_EDGE = 1.95f;
    private const float DISTANCE_FROM_CAMERA = 10f;

    public void MoveByScreenPosition(Vector2 inputPosition)
    {
        if (GameDirector.Instance.CurrentGameState == GameState.InGame)
        {
            // 將傳入的螢幕座標轉換為世界座標
            Vector3 screenPosition = new Vector3(inputPosition.x, inputPosition.y, DISTANCE_FROM_CAMERA);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // 把座標限制在一定範圍內
            float newX = Mathf.Clamp(worldPosition.x, LEFT_BOX_EDGE, RIGHT_BOX_EDGE);

            // 把座標反映在player上
            transform.position = new Vector2(newX, transform.position.y);
        }
    }

    public void Release()
    {
        if (GameDirector.Instance.CurrentGameState == GameState.InGame)
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
