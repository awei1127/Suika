using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform player;
    private const int RANDOM_MAX_VALUE = 4;
    private BallCollisionHandler currentBallHandler;
    
    void Start()
    {
        SpawnCurrentBall();
    }

    // 創建球
    void SpawnCurrentBall()
    {
        // 取得隨機枚舉值
        BallNumber currentBallNumber = GetRandomBallNumber();

        // 創建球物件
        GameObject newBall = Instantiate(ballPrefab, player);

        // 引用這顆球的 BallState 組件並進行初始化
        bool isCurrentBall = true;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, currentBallNumber);

        // 引用這顆球的 BallCollisionHandler 組件並訂閱落下碰撞事件、同球碰撞事件
        currentBallHandler = newBall.GetComponent<BallCollisionHandler>();
        currentBallHandler.FallCollided += FallCollidedHandler;
        currentBallHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // 創建球 傳入 球種 位置
    void SpawnMergedBall(Vector3 position, BallNumber number)
    {
        // 創建球物件
        GameObject newBall = Instantiate(ballPrefab, position, new Quaternion());

        // 引用這顆球的 BallState 組件並進行初始化
        bool isCurrentBall = false;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, number);

        // 引用這顆球的 BallCollisionHandler 組件並訂閱同球碰撞事件
        BallCollisionHandler mergedBallHandler = newBall.GetComponent<BallCollisionHandler>();
        mergedBallHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // 落下碰撞事件處理器 (落下就生成球)
    void FallCollidedHandler(object sender, EventArgs e)
    {
        Debug.Log("執行落下碰撞處理器！");
        // 取消訂閱
        if (currentBallHandler != null)
        {
            currentBallHandler.FallCollided -= FallCollidedHandler;
        }
        // 創建球物件
        SpawnCurrentBall();
    }

    // 同球碰撞事件處理器 (同球碰撞就生成球)
    void SameBallCollidedHandler(object sender, SameBallCollidedEventArgs e)
    {
        Debug.Log("執行同球碰撞處理器！");
        // 球種升級 (將枚舉值+1)
        BallNumber upgradedBallNumber = (BallNumber)(((int)e.ballNumber) + 1);
        // 生成球
        SpawnMergedBall(e.midpoint, upgradedBallNumber);
    }

    // 取得隨機枚舉值方法
    BallNumber GetRandomBallNumber()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, RANDOM_MAX_VALUE + 1);
        return (BallNumber)randomNumber;
    }
}
