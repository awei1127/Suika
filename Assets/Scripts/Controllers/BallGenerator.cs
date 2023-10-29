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

    // �Ыزy
    void SpawnCurrentBall()
    {
        // ���o�H���T�|��
        BallNumber currentBallNumber = GetRandomBallNumber();

        // �Ыزy����
        GameObject newBall = Instantiate(ballPrefab, player);

        // �ޥγo���y�� BallState �ե�öi���l��
        bool isCurrentBall = true;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, currentBallNumber);

        // �ޥγo���y�� BallCollisionHandler �ե�íq�\���U�I���ƥ�B�P�y�I���ƥ�
        currentBallHandler = newBall.GetComponent<BallCollisionHandler>();
        currentBallHandler.FallCollided += FallCollidedHandler;
        currentBallHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // �Ыزy �ǤJ �y�� ��m
    void SpawnMergedBall(Vector3 position, BallNumber number)
    {
        // �Ыزy����
        GameObject newBall = Instantiate(ballPrefab, position, new Quaternion());

        // �ޥγo���y�� BallState �ե�öi���l��
        bool isCurrentBall = false;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, number);

        // �ޥγo���y�� BallCollisionHandler �ե�íq�\�P�y�I���ƥ�
        BallCollisionHandler mergedBallHandler = newBall.GetComponent<BallCollisionHandler>();
        mergedBallHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // ���U�I���ƥ�B�z�� (���U�N�ͦ��y)
    void FallCollidedHandler(object sender, EventArgs e)
    {
        Debug.Log("���渨�U�I���B�z���I");
        // �����q�\
        if (currentBallHandler != null)
        {
            currentBallHandler.FallCollided -= FallCollidedHandler;
        }
        // �Ыزy����
        SpawnCurrentBall();
    }

    // �P�y�I���ƥ�B�z�� (�P�y�I���N�ͦ��y)
    void SameBallCollidedHandler(object sender, SameBallCollidedEventArgs e)
    {
        Debug.Log("����P�y�I���B�z���I");
        // �y�ؤɯ� (�N�T�|��+1)
        BallNumber upgradedBallNumber = (BallNumber)(((int)e.ballNumber) + 1);
        // �ͦ��y
        SpawnMergedBall(e.midpoint, upgradedBallNumber);
    }

    // ���o�H���T�|�Ȥ�k
    BallNumber GetRandomBallNumber()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, RANDOM_MAX_VALUE + 1);
        return (BallNumber)randomNumber;
    }
}
