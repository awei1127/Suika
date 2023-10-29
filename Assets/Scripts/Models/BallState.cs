using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    public bool IsDestroyed { get; set; } = false;
    public bool IsCurrentBall { get; set; } = true;
    public BallNumber ballNumber;

    public void Initialize(bool isCurrentBall, BallNumber ballNumber)
    {
        this.ballNumber = ballNumber;

        // 如果不是創建當前的球 則一開始就受重力影響
        if (!isCurrentBall)
        {
            IsCurrentBall = isCurrentBall;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
