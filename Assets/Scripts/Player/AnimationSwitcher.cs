using Spine;
using Spine.Unity;
using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private LookRotateBone _boneLookRotate;


    private string _idleAnimation = "idle";
    private string _attackTargetAnimation = "attack_target";
    private string _attackAnimation = "attack_finish";
    private string _startAttackAnimation = "attack_start";

    private void Start()
    {
        // Устанавливаем начальную анимацию
        _boneLookRotate.enabled = false;
        SetAnimation(_idleAnimation);
    }

    public void ChangeToIdleAnimation()
    {
        SetAnimation(_idleAnimation);
    }

    public void ChangeToAttackTargetAnimation()
    {
        _boneLookRotate.enabled=true;
        _skeletonAnimation.AnimationState.SetAnimation(0, _startAttackAnimation, false);
        _skeletonAnimation.AnimationState.AddAnimation(0, _attackTargetAnimation, true, 0f);
        //SetAnimation(_attackTargetAnimation);
    }

    public void ChangeToAttackAnimation()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, _attackAnimation, false);
        Debug.Log(_skeletonAnimation.AnimationName);
        _skeletonAnimation.AnimationState.AddAnimation(0, _idleAnimation, true, 0f);
        _boneLookRotate.enabled = false;
    }

    // Метод для установки анимации
    private void SetAnimation(string animationName)
    {
        // Проверяем, что анимация существует
        if (_skeletonAnimation.SkeletonDataAsset.GetSkeletonData(true).FindAnimation(animationName) != null)
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);
        }
        else
        {
            Debug.LogWarning("Animation " + animationName + " does not exist.");
        }
    }

    // Пример вызова переключения анимаций
    

}
