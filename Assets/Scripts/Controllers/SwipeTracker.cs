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
        // 調用 playerController 的移動方法
        playerController.MoveByScreenPosition(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 調用 playerController 的移動方法
        playerController.MoveByScreenPosition(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 調用 playerController 的放開球方法
        playerController.Release();
    }
}
