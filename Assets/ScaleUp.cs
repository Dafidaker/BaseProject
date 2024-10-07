using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleUp", menuName = "ButtonAnimations/ScaleUp")]
public class ScaleUp : ButtonAnimationBase
{
    public Vector3 increaseBy;
    protected override void Animation(ButtonController buttonController)
    {
        var transform = buttonController.gameObject.transform;
        var targetScale = transform.localScale + increaseBy;
        
        transform.DOScale(targetScale, 1f).SetEase(typeOfEase); 
    }
}
