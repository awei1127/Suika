using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    public bool IsDestroyed { get; set; } = false;
    public BallNumber ballNumber;   // �w�]��0 �ݦb�s�边��ʳ]�m
    public BallStage ballStage;

    // ��l�Ʋy����k �]�w�O�_�� ��e�y �����O�v�T �ҥθI����
    public void Initialize(BallStage stage, bool isKinematic, bool colliderEnabled)
    {
        ballStage = stage;
        GetComponent<Rigidbody2D>().isKinematic = isKinematic;
        GetComponent<Collider2D>().enabled = colliderEnabled;
    }
}
