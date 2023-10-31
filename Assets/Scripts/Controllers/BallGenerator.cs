using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public BallPrefabsData ballPrefabsData;
    public Transform player;
    public Transform nextBallWindow;
    private const int RANDOM_MAX_VALUE = 4;
    private BallCollisionHandler currentBallHandler;
    private GameObject nextBall;
    
    void Start()
    {
        SpawnNextBall();
        SetNextBallToCurrent();
        SpawnNextBall();
    }

    // 創建 Next 球
    void SpawnNextBall()
    {
        // 取得隨機整數
        int index = GetRandomInt();

        // 從 ballPrefabsData 變量中取出準備要創建的 prefab
        GameObject prefabToInstantiate = ballPrefabsData.ballPrefabs[index];

        // 創建球物件
        nextBall = Instantiate(prefabToInstantiate, nextBallWindow);

        // 引用這顆球的 BallState 組件並進行初始化
        bool isCurrentBall = false;
        bool isKinematic = true;
        bool colliderEnabled = false;
        BallState ballState = nextBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, isKinematic, colliderEnabled);
    }

    // 將 Next 球設為玩家手上的球
    void SetNextBallToCurrent()
    {
        // 將 nextBall 的父級設為 player 並重置位置
        nextBall.transform.parent = player;
        nextBall.transform.localPosition = Vector3.zero;

        // 對 nextBall 重新初始化
        bool isCurrentBall = true;
        bool isKinematic = true;
        bool colliderEnabled = false;
        BallState ballState = nextBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, isKinematic, colliderEnabled);

        // 引用這顆球的 BallCollisionHandler 組件並訂閱落下碰撞事件、同球碰撞事件
        currentBallHandler = nextBall.GetComponent<BallCollisionHandler>();
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
        bool isKinematic = false;
        bool colliderEnabled = true;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, isKinematic, colliderEnabled);

        // 引用這顆球的 BallCollisionHandler 組件並訂閱同球碰撞事件
        BallCollisionHandler mergedBallHandler = newBall.GetComponent<BallCollisionHandler>();
        mergedBallHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // 落下碰撞事件處理器 (落下就生成球)
    void FallCollidedHandler(object sender, EventArgs e)
    {
        // 取消訂閱
        if (currentBallHandler != null)
        {
            currentBallHandler.FallCollided -= FallCollidedHandler;
        }
        // 把 Next 球物件設為當前 並創建一個新的 Next 球物件
        SetNextBallToCurrent();
        SpawnNextBall();
    }

    // 同球碰撞事件處理器 (同球碰撞就生成球)
    void SameBallCollidedHandler(object sender, SameBallCollidedEventArgs e)
    {
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
