using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public static event Action<BallGenerator> BallGeneratorCreated; // �w�q�@���R�A�ƥ�A�o�Өƥ�|�b�y�ͦ�����ҤƮɳQĲ�o�C

    public BallPrefabsData ballPrefabsData;
    public Transform player;
    public Transform nextBallWindow;
    private const int RANDOM_MAX_VALUE = 4;
    private BallCollisionHandler currentBallCollisionHandler;
    private GameObject nextBall;
    public event EventHandler<SpawnBallEventArgs> SpawnBall;

    void Awake()
    {
        // ��ҤƮɵo��"�Ыزy���;��ƥ�"�A�H�Ѿɺt����q�\"�Ыزy�ƥ�"
        BallGeneratorCreated?.Invoke(this);
    }

    void OnDestroy()
    {
        // ��ҳQ�R���ɨ����Ҧ����q�\
        BallGeneratorCreated = null;
    }

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
        bool isKinematic = true;
        bool colliderEnabled = false;
        BallState ballState = nextBall.GetComponent<BallState>();
        ballState.Initialize(BallStage.Next, isKinematic, colliderEnabled);

        // �ޥγo���y�� BallCollisionHandler �ե�íq�\���U�I���ƥ�B�P�y�I���ƥ�
        currentBallCollisionHandler = nextBall.GetComponent<BallCollisionHandler>();
        currentBallCollisionHandler.FallCollided += FallCollidedHandler;
        currentBallCollisionHandler.SameBallCollided += SameBallCollidedHandler;

        // �o�ͳЫزy�ƥ�
        SpawnBallEventArgs e = new SpawnBallEventArgs();
        e.spawnedBallCollisionHandler = currentBallCollisionHandler;
        e.spawnedBallPositionHandler = nextBall.GetComponent<BallPositionHandler>();
        OnSpawnBall(e);
    }

    // �N Next �y�]�����a��W���y
    void SetNextBallToCurrent()
    {
        // �N nextBall �����ų]�� player �í��m��m
        nextBall.transform.parent = player;
        nextBall.transform.localPosition = Vector3.zero;

        // �� nextBall ���s��l��
        bool isKinematic = true;
        bool colliderEnabled = false;
        BallState ballState = nextBall.GetComponent<BallState>();
        ballState.Initialize(BallStage.Current, isKinematic, colliderEnabled);
    }

    // �ھڶǤJ����m�H�έ�l�y�� �ӳЫؤɯū᪺�y
    void SpawnUpgradedBall(Vector3 position, BallNumber originNumber)
    {
        // ����y�ɯŭ���
        SoundManager.Instance.PlayBallUpgradeSound();
        
        // ���o�ɯū᪺��� (�N�T�|��+1)
        int upgradedIndex = ((int)originNumber) + 1;

        // �q ballPrefabsData �ܶq�����X�ǳƭn�Ыت� prefab
        GameObject prefabToInstantiate = ballPrefabsData.ballPrefabs[upgradedIndex];

        // �Ыزy����
        GameObject newBall = Instantiate(prefabToInstantiate, position, new Quaternion());   // �ݧ令�H����V

        // �ޥγo���y�� BallState �ե�öi���l��
        bool isKinematic = false;
        bool colliderEnabled = true;
        BallState ballState = newBall.GetComponent<BallState>();
        ballState.Initialize(BallStage.Inbox, isKinematic, colliderEnabled);

        // �ޥγo���y�� BallCollisionHandler �ե�íq�\�P�y�I���ƥ�
        BallCollisionHandler mergedBallCollisionHandler = newBall.GetComponent<BallCollisionHandler>();
        mergedBallCollisionHandler.SameBallCollided += SameBallCollidedHandler;

        // �o�ͳЫزy�ƥ�
        SpawnBallEventArgs e = new SpawnBallEventArgs();
        e.spawnedBallCollisionHandler = mergedBallCollisionHandler;
        e.spawnedBallPositionHandler = newBall.GetComponent<BallPositionHandler>();
        OnSpawnBall(e);
    }

    // ���U�I���ƥ�B�z�� (���U�N�ͦ��y)
    void FallCollidedHandler(object sender, EventArgs e)
    {
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

    // ���y�ƥ�
    void OnSpawnBall(SpawnBallEventArgs e)
    {
        SpawnBall?.Invoke(this, e);
    }
}
