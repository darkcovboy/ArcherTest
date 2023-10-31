using Spine;
using Spine.Unity;
using UnityEngine;

public class LookRotateBone : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SpineBone(dataField: "skeletonAnimation")]
    [SerializeField] private string _boneNameGun;
    [SerializeField] private Camera _mainCamera;
    
    private Bone _bone;

    private void Start()
    {
        _bone = _skeletonAnimation.Skeleton.FindBone(_boneNameGun);

        Debug.Log(_bone != null);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _mainCamera.transform.position.z;
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        // Переведите позицию мыши в координаты локального пространства скелета
        Vector3 skeletonSpacePoint = _skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);

        // Вычислите угол вращения, чтобы кость смотрела в сторону мыши
        float rotationAngle = Mathf.Atan2(skeletonSpacePoint.y, skeletonSpacePoint.x) * Mathf.Rad2Deg;

        // Установите угол вращения кости
        _bone.Rotation = (rotationAngle + 180);

        // Обновите скелет для применения изменений
        _skeletonAnimation.Skeleton.UpdateWorldTransform();
    }
}
