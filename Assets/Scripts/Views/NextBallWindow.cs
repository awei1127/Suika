using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBallWindow : MonoBehaviour
{
    void Start()
    {
        // ���oUI�� NextPanel ���ù��y��
        Vector3 nextPanelPosition = MenuView.Instance.NextPanel.transform.position;

        // ���� NextPanel ���e�� �H�Φۭq�����רӺ�X����ܲy����m
        float nextPanelWidth = MenuView.Instance.NextPanel.GetComponent<RectTransform>().rect.width;
        float yPadding = -200f;
        Vector3 nextBallScreenPosition = new Vector3(nextPanelPosition.x + nextPanelWidth / 2, nextPanelPosition.y + yPadding, nextPanelPosition.z);

        // �N�ù��y���ഫ���@�ɮy��
        Vector3 nextBallworldPosition = Camera.main.ScreenToWorldPoint(nextBallScreenPosition);

        // ��y�ФϬM�b NextBallWindow ����W
        transform.position = new Vector2(nextBallworldPosition.x, nextBallworldPosition.y);
    }
}
