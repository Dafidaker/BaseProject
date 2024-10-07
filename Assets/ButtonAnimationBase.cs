using DG.Tweening;
using UnityEngine;
using Utility;

public abstract class ButtonAnimationBase : ScriptableObject
{
    public Ease typeOfEase = Ease.InOutSine;
    public bool resetAnimations = true;
    

    private void StopAndResetTween(ButtonController buttonController)
    {
        if (!resetAnimations) return;
        
        var transform = buttonController.gameObject.transform;
        transform.DOKill();
        TransformUtil.CopyTransformData(buttonController.InitialTransform,transform);
    }
    protected abstract void Animation(ButtonController buttonController);

    public void CallAnimation(ButtonController buttonController)
    {
        StopAndResetTween(buttonController);
        Animation(buttonController);
    }
}
