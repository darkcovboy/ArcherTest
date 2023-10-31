using Spine;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

public class BowShooting : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SpineBone(dataField: "skeletonAnimation")]
    [SerializeField] private string _boneName;
    [SerializeField] private AnimationSwitcher _animationSwitcher;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Arrow _arrowPrefab; 
    [SerializeField] private PointPath _pointPath; 
    [SerializeField] private float _maxPower = 10f;    

    private Vector2 _initialPosition; // Начальная позиция
    private bool _isBowActive = false;  // Натянута ли титева
    private float _currentPower = 0f;    // Текущая сила выстрела
    private Bone _bone;

    private void Start()
    {
        _bone = _skeletonAnimation.Skeleton.FindBone(_boneName);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (_isBowActive && Input.GetMouseButton(0))
        {
            ContinueDrawing();
        }
        else if (_isBowActive && Input.GetMouseButtonUp(0))
        {
            FireArrow();
        }
    }

    // Начало натяжения лука
    private void StartDrawing()
    {
        _animationSwitcher.ChangeToAttackTargetAnimation();
        _initialPosition = _bone.GetSkeletonSpacePosition();
        _isBowActive = true;
        _pointPath.ShowPath();
    }

    // Продолжение натяжения лука
    private void ContinueDrawing()
    {
        _initialPosition = _bone.GetSkeletonSpacePosition();
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _currentPower = Mathf.Min((mousePosition - _initialPosition).magnitude, _maxPower);
        _pointPath.Create(_initialPosition, _currentPower);
    }

    // Выстрел стрелы
    private void FireArrow()
    {
        _isBowActive = false;
        _pointPath.ClosePath();

        Rigidbody2D arrow = Instantiate(_arrowPrefab, _initialPosition, Quaternion.identity).GetComponent<Rigidbody2D>();

        // Рассчитать направление и силу выстрела
        Vector2 shootDirection = -(_initialPosition + (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition)).normalized;
        arrow.velocity = shootDirection * _currentPower;

        _animationSwitcher.ChangeToAttackAnimation();

        // Сбросить текущую силу
        _currentPower = 0f;
    }
}
