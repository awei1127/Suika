using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public float moveSpeed;
    private const float LEFT_EDGE = -1.95f;
    private const float RIGHT_EDGE = 1.95f;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            float newX = transform.position.x + moveSpeed;
            newX = Mathf.Min(newX, RIGHT_EDGE);
            transform.position = new Vector2(newX, transform.position.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float newX = transform.position.x - moveSpeed;
            newX = Mathf.Max(newX, LEFT_EDGE);
            transform.position = new Vector2(newX, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Release();
        }
    }

    void Release()
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
