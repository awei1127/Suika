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

    // 碰撞時 視條件讓事件發生
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 若是當前的球 則只發生一次落下碰撞事件 加上條件: 且碰到的東西不是牆
        if (ballState.ballStage == BallStage.Current)
        {
            OnFallCollided();
            ballState.ballStage = BallStage.Inbox;
        }

        // 取得 BallNumber 枚舉的最後一個值
        Array values = Enum.GetValues(typeof(BallNumber));
        BallNumber lastValues = (BallNumber)values.GetValue(values.Length - 1);

        // 若不是最後的球種
        if (ballState.ballNumber != lastValues)
        {
            // 取得碰撞物物件
            GameObject collidedBall = collision.gameObject;

            // 若碰撞物Tag為Ball 若球種相同 則發生同球碰撞事件
            if (collidedBall.CompareTag("Ball"))
            {
                if (ballState.ballNumber == collidedBall.GetComponent<BallState>().ballNumber)
                {
                    OnSameBallCollided(collision);
                }
            }
        }
    }

    // 落下碰撞事件
    void OnFallCollided()
    {
        FallCollided?.Invoke(this, EventArgs.Empty);
    }

    // 同球碰撞事件
    void OnSameBallCollided(Collision2D collision)
    {
        // 取得碰撞物的 BallState 類/組件
        BallState collidedBallState = collision.gameObject.GetComponent<BallState>();
        
        // 確認自己與碰撞物都未被處理再處理 以避免在同一幀重複多次處理
        if (!ballState.IsDestroyed && !collidedBallState.IsDestroyed)
        {
            ballState.IsDestroyed = true;
            collidedBallState.IsDestroyed = true;

            Destroy(this.gameObject);
            Destroy(collision.gameObject);

            // 準備事件參數 放入 碰撞位置 球種
            SameBallCollidedEventArgs e = new SameBallCollidedEventArgs();
            e.midpoint = (transform.position + collision.transform.position) / 2;
            e.ballNumber = ballState.ballNumber;

            // 呼叫事件 傳入事件參數 (碰撞位置 球種)
            SameBallCollided?.Invoke(this, e);
        }
    }
}
