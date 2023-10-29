using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    private BallState ballState;
    public event EventHandler FallCollided;
    public event EventHandler<SameBallCollidedEventArgs> SameBallCollided;

    void Start()
    {
        ballState = GetComponent<BallState>();
    }

    // �I���� ���������ƥ�o��
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �Y�O��e���y �h�u�o�ͤ@�����U�I���ƥ�
        if (ballState.IsCurrentBall)
        {
            OnFallCollided();
            ballState.IsCurrentBall = false;
        }

        // �YTag�ۦP �h�o�ͦP�y�I���ƥ�
        if (collision.gameObject.CompareTag("Ball"))
        {
            OnSameBallCollided(collision);
        }
    }

    // ���U�I���ƥ�
    void OnFallCollided()
    {
        FallCollided?.Invoke(this, EventArgs.Empty);
    }

    // �P�y�I���ƥ�
    void OnSameBallCollided(Collision2D collision)
    {
        // ���o�I������ BallState ��/�ե�
        BallState collidedBallState = collision.gameObject.GetComponent<BallState>();
        
        // �T�{�ۤv�P�I���������Q�B�z�A�B�z �H�קK�b�P�@�V���Ʀh���B�z
        if (!ballState.IsDestroyed && !collidedBallState.IsDestroyed)
        {
            ballState.IsDestroyed = true;
            collidedBallState.IsDestroyed = true;

            Destroy(this.gameObject);
            Destroy(collision.gameObject);

            // �ǳƨƥ�Ѽ� ��J �I����m �y��
            SameBallCollidedEventArgs e = new SameBallCollidedEventArgs();
            e.midpoint = (transform.position + collision.transform.position) / 2;
            e.ballNumber = ballState.ballNumber;

            // �I�s�ƥ� �ǤJ�ƥ�Ѽ� (�I����m �y��)
            SameBallCollided?.Invoke(this, e);
        }
    }
}
