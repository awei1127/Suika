using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    public bool IsDestroyed { get; set; } = false;
    public bool IsCurrentBall { get; set; } = true;
    public BallNumber ballNumber;   // �w�]��0 �ݦb�s�边��ʳ]�m

    // ��l�Ʋy����k �]�w�O�_�� ��e�y �����O�v�T �ҥθI����
    public void Initialize(bool isCurrentBall, bool isKinematic, bool colliderEnabled)
    {
        IsCurrentBall = isCurrentBall;
        GetComponent<Rigidbody2D>().isKinematic = isKinematic;
        GetComponent<Collider2D>().enabled = colliderEnabled;
    }
}
