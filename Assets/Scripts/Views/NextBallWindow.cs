using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBallWindow : MonoBehaviour
{
    void Start()
    {
        // 取得UI中 NextPanel 的螢幕座標
        Vector3 nextPanelPosition = MenuView.Instance.NextPanel.transform.position;

        // 按照 NextPanel 的寬度 以及自訂的高度來算出欲顯示球的位置
        float nextPanelWidth = MenuView.Instance.NextPanel.GetComponent<RectTransform>().rect.width;
        float yPadding = -200f;
        Vector3 nextBallScreenPosition = new Vector3(nextPanelPosition.x + nextPanelWidth / 2, nextPanelPosition.y + yPadding, nextPanelPosition.z);

        // 將螢幕座標轉換為世界座標
        Vector3 nextBallworldPosition = Camera.main.ScreenToWorldPoint(nextBallScreenPosition);

        // 把座標反映在 NextBallWindow 物件上
        transform.position = new Vector2(nextBallworldPosition.x, nextBallworldPosition.y);
    }
}
