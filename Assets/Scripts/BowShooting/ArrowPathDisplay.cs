using UnityEngine;

public class ArrowPathDisplay : MonoBehaviour
{
    public GameObject pathPointPrefab; // ������ ����� ����
    public Transform pathParent;       // ������������ ������ ��� ����� ����

    // �������� ����
    // �������� ����
    public void ClearPath()
    {
        foreach (Transform child in pathParent)
        {
            Destroy(child.gameObject);
        }
    }

    // ������� ����� ����
    public void CreatePath(Vector2 startPosition, Vector2 direction, float power, int pointCount)
    {
        ClearPath(); // �������� ������������ ����� ����

        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)(pointCount - 1);
            Vector2 pointPosition = startPosition + direction * power * t;

            // ������� ������ ����� ����
            GameObject pathPoint = Instantiate(pathPointPrefab, pointPosition, Quaternion.identity, pathParent);
        }
    }

}
