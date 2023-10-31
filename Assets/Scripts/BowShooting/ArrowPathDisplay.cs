using UnityEngine;

public class ArrowPathDisplay : MonoBehaviour
{
    public GameObject pathPointPrefab; // Префаб точки пути
    public Transform pathParent;       // Родительский объект для точек пути

    // Очистить путь
    // Очистить путь
    public void ClearPath()
    {
        foreach (Transform child in pathParent)
        {
            Destroy(child.gameObject);
        }
    }

    // Создать точки пути
    public void CreatePath(Vector2 startPosition, Vector2 direction, float power, int pointCount)
    {
        ClearPath(); // Очистить существующие точки пути

        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)(pointCount - 1);
            Vector2 pointPosition = startPosition + direction * power * t;

            // Создать объект точки пути
            GameObject pathPoint = Instantiate(pathPointPrefab, pointPosition, Quaternion.identity, pathParent);
        }
    }

}
