using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
        // �Y�O��e���y �h�u�o�ͤ@�����U�I���ƥ� �[�W����: �B�I�쪺�F�褣�O��
        if (ballState.ballStage == BallStage.Current)
        {
            OnFallCollided();
            ballState.ballStage = BallStage.Inbox;
        }

        // ���o BallNumber �T�|���̫�@�ӭ�
        Array values = Enum.GetValues(typeof(BallNumber));
        BallNumber lastValues = (BallNumber)values.GetValue(values.Length - 1);

        // �Y���O�̫᪺�y��
        if (ballState.ballNumber != lastValues)
        {
            // ���o�I��������
            GameObject collidedBall = collision.gameObject;

            // �Y�I����Tag��Ball �Y�y�جۦP �h�o�ͦP�y�I���ƥ�
            if (collidedBall.CompareTag("Ball"))
            {
                if (ballState.ballNumber == collidedBall.GetComponent<BallState>().ballNumber)
                {
                    OnSameBallCollided(collision);
                }
            }
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
