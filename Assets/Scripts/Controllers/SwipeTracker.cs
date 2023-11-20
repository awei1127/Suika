using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeTracker : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private PlayerController playerController;
    
    public void OnDrag(PointerEventData eventData)
    {
        // �ե� playerController �����ʤ�k
        playerController.MoveByScreenPosition(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // �ե� playerController �����ʤ�k
        playerController.MoveByScreenPosition(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // �ե� playerController ����}�y��k
        playerController.Release();
    }
}
