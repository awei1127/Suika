using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public BallPrefabsData ballPrefabsData;
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
        // 取得隨機整數
        int index = GetRandomInt();

        // 從 ballPrefabsData 變量中取出準備要創建的 prefab
        GameObject prefabToInstantiate = ballPrefabsData.ballPrefabs[index];

        // 創建球物件
        GameObject newBall = Instantiate(prefabToInstantiate, player);

        // 引用這顆球的 BallState 組件並進行初始化
        bool isCurrentBall = true;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall);

        // 引用這顆球的 BallCollisionHandler 組件並訂閱落下碰撞事件、同球碰撞事件
        currentBallHandler = newBall.GetComponent<BallCollisionHandler>();
        currentBallHandler.FallCollided += FallCollidedHandler;
        currentBallHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // 根據傳入的位置以及原始球種 來創建升級後的球
    void SpawnUpgradedBall(Vector3 position, BallNumber originNumber)
    {
        // 取得升級後的整數 (將枚舉值+1)
        int upgradedIndex = ((int)originNumber) + 1;

        // 從 ballPrefabsData 變量中取出準備要創建的 prefab
        GameObject prefabToInstantiate = ballPrefabsData.ballPrefabs[upgradedIndex];

        // 創建球物件
        GameObject newBall = Instantiate(prefabToInstantiate, position, new Quaternion());   // 待改成隨機方向

        // 引用這顆球的 BallState 組件並進行初始化
        bool isCurrentBall = false;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall);

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
        // 生成球
        SpawnUpgradedBall(e.midpoint, e.ballNumber);
    }

    // 取得最大值為 RANDOM_MAX_VALUE 的隨機整數
    int GetRandomInt()
    {
        System.Random random = new System.Random();
        return random.Next(0, RANDOM_MAX_VALUE + 1);
    }
}
