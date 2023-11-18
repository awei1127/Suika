using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    public bool IsDestroyed { get; set; } = false;
    public BallNumber ballNumber;   // 預設為0 需在編輯器手動設置
    public BallStage ballStage;

    // 初始化球的方法 設定是否為 當前球 受重力影響 啟用碰撞器
    public void Initialize(BallStage stage, bool isKinematic, bool colliderEnabled)
    {
        ballStage = stage;
        GetComponent<Rigidbody2D>().isKinematic = isKinematic;
        GetComponent<Collider2D>().enabled = colliderEnabled;
    }
}
