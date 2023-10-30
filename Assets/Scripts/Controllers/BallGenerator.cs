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

    // �Ыزy
    void SpawnCurrentBall()
    {
        // ���o�H�����
        int index = GetRandomInt();

        // �q ballPrefabsData �ܶq�����X�ǳƭn�Ыت� prefab
        GameObject prefabToInstantiate = ballPrefabsData.ballPrefabs[index];

        // �Ыزy����
        GameObject newBall = Instantiate(prefabToInstantiate, player);

        // �ޥγo���y�� BallState �ե�öi���l��
        bool isCurrentBall = true;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall);

        // �ޥγo���y�� BallCollisionHandler �ե�íq�\���U�I���ƥ�B�P�y�I���ƥ�
        currentBallHandler = newBall.GetComponent<BallCollisionHandler>();
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
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(isCurrentBall);

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
