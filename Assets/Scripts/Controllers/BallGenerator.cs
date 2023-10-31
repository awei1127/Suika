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

    // �Ы� Next �y
    void SpawnNextBall()
    {
        // ���o�H�����
        int index = GetRandomInt();

        // �q ballPrefabsData �ܶq�����X�ǳƭn�Ыت� prefab
        GameObject prefabToInstantiate = ballPrefabsData.ballPrefabs[index];

        // �Ыزy����
        nextBall = Instantiate(prefabToInstantiate, nextBallWindow);

        // �ޥγo���y�� BallState �ե�öi���l��
        bool isCurrentBall = false;
        bool isKinematic = true;
        bool colliderEnabled = false;
        BallState ballState = nextBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, isKinematic, colliderEnabled);
    }

    // �N Next �y�]�����a��W���y
    void SetNextBallToCurrent()
    {
        // �N nextBall �����ų]�� player �í��m��m
        nextBall.transform.parent = player;
        nextBall.transform.localPosition = Vector3.zero;

        // �� nextBall ���s��l��
        bool isCurrentBall = true;
        bool isKinematic = true;
        bool colliderEnabled = false;
        BallState ballState = nextBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, isKinematic, colliderEnabled);

        // �ޥγo���y�� BallCollisionHandler �ե�íq�\���U�I���ƥ�B�P�y�I���ƥ�
        currentBallHandler = nextBall.GetComponent<BallCollisionHandler>();
        currentBallHandler.FallCollided += FallCollidedHandler;
        currentBallHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // �ھڶǤJ����m�H�έ�l�y�� �ӳЫؤɯū᪺�y
    void SpawnUpgradedBall(Vector3 position, BallNumber originNumber)
    {
        // ���o�ɯū᪺��� (�N�T�|��+1)
        int upgradedIndex = ((int)originNumber) + 1;

        // �q ballPrefabsData �ܶq�����X�ǳƭn�Ыت� prefab
        GameObject prefabToInstantiate = ballPrefabsData.ballPrefabs[upgradedIndex];

        // �Ыزy����
        GameObject newBall = Instantiate(prefabToInstantiate, position, new Quaternion());   // �ݧ令�H����V

        // �ޥγo���y�� BallState �ե�öi���l��
        bool isCurrentBall = false;
        bool isKinematic = false;
        bool colliderEnabled = true;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall, isKinematic, colliderEnabled);

        // �ޥγo���y�� BallCollisionHandler �ե�íq�\�P�y�I���ƥ�
        BallCollisionHandler mergedBallHandler = newBall.GetComponent<BallCollisionHandler>();
        mergedBallHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // ���U�I���ƥ�B�z�� (���U�N�ͦ��y)
    void FallCollidedHandler(object sender, EventArgs e)
    {
        // �����q�\
        if (currentBallHandler != null)
        {
            currentBallHandler.FallCollided -= FallCollidedHandler;
        }
        // �� Next �y����]����e �óЫؤ@�ӷs�� Next �y����
        SetNextBallToCurrent();
        SpawnNextBall();
    }

    // �P�y�I���ƥ�B�z�� (�P�y�I���N�ͦ��y)
    void SameBallCollidedHandler(object sender, SameBallCollidedEventArgs e)
    {
        // �ͦ��y
        SpawnUpgradedBall(e.midpoint, e.ballNumber);
    }

    // ���o�̤j�Ȭ� RANDOM_MAX_VALUE ���H�����
    int GetRandomInt()
    {
        System.Random random = new System.Random();
        return random.Next(0, RANDOM_MAX_VALUE + 1);
    }
}
