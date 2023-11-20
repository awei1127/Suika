using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionHandler : MonoBehaviour
{
    private const float UP_EDGE = 1.14f;
    public event EventHandler ReachUpEdge;
    void Update()
    {
        if (transform.position.y > UP_EDGE && GetComponent<BallState>().ballStage == BallStage.Inbox)
        {
            // �o�ͨ�F�W�t�ƥ�
            OnReachUpEdge();
        }
    }

    // ��F�W�t�ƥ�
    void OnReachUpEdge()
    {
        ReachUpEdge?.Invoke(this, EventArgs.Empty);
    }
}