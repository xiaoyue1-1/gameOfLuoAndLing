using DG.Tweening;
using UnityEngine;

public static class toChangeXY
{
    public static void MoveTo(this Transform transform, float x, float y, float duration)
    {
        // 先杀死该对象上所有进行中的动画，防止冲突
        transform.DOKill();
        // 启动新动画，显式线性，并设置为绝对移动（Relative 默认 false）
        transform.DOMove(new Vector3(x, y, transform.position.z), duration)
                 .SetEase(Ease.Linear)
                 .SetRelative(false);
    }
}