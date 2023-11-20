using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float LEFT_BOX_EDGE = -1.95f;
    private const float RIGHT_BOX_EDGE = 1.95f;
    private const float DISTANCE_FROM_CAMERA = 10f;

    public void MoveByScreenPosition(Vector2 inputPosition)
    {
        if (GameDirector.Instance.CurrentGameState == GameState.InGame)
        {
            // �N�ǤJ���ù��y���ഫ���@�ɮy��
            Vector3 screenPosition = new Vector3(inputPosition.x, inputPosition.y, DISTANCE_FROM_CAMERA);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // ��y�Э���b�@�w�d��
            float newX = Mathf.Clamp(worldPosition.x, LEFT_BOX_EDGE, RIGHT_BOX_EDGE);

            // ��y�ФϬM�bplayer�W
            transform.position = new Vector2(newX, transform.position.y);
        }
    }

    public void Release()
    {
        if (GameDirector.Instance.CurrentGameState == GameState.InGame)
        {
            // �� tag �� Ball ���l����
            string tag = "Ball";
            GameObject holdingBall = FindChildWithTag(this.gameObject, tag);

            // �p�G��W���y
            if (holdingBall)
            {
                // ���y�� ���O �I�����ͮ� ���Ŭ���(���_���l���Y)
                bool isKinematic = false;
                bool colliderEnabled = true;
                holdingBall.GetComponent<BallState>().Initialize(BallStage.Current, isKinematic, colliderEnabled);
                holdingBall.transform.parent = null;
            }
        }
        
    }

    // ���o�ŦX tag ���Ĥ@�Ӥl����
    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        // �p�G�S���l����
        if (parent.transform.childCount == 0)
        {
            return null;
        }

        // �M���l���� �^�ǲĤ@�ӲŦX tag �� GameObject
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
