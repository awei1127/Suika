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

        // �p�G���O�Ыط�e���y �h�@�}�l�N�����O�v�T
        if (!isCurrentBall)
        {
            IsCurrentBall = isCurrentBall;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
