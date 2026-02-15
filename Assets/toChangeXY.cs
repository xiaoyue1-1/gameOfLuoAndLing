using DG.Tweening;
using UnityEngine;

public static class toChangeXY
{
    public static void MoveTo(this Transform transform, float x, float y, float duration)
    {
        transform.DOKill();
        transform.DOMove(new Vector3(x, y, transform.position.z), duration)
                 .SetEase(Ease.Linear)
                 .SetRelative(false);
    }
}