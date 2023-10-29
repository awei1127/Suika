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

    // 碰撞時 視條件讓事件發生
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 若是當前的球 則只發生一次落下碰撞事件
        if (ballState.IsCurrentBall)
        {
            OnFallCollided();
            ballState.IsCurrentBall = false;
        }

        // 若Tag相同 則發生同球碰撞事件
        if (collision.gameObject.CompareTag("Ball"))
        {
            OnSameBallCollided(collision);
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
