using DG.Tweening;
using UnityEngine;

public static class CanvasGroupExtensions
{
    public static void Show(this CanvasGroup cg, bool visible,float time)
    {
        if(visible)
        {
            cg.DOFade(1f, time);
        }
        else
        {
            cg.DOFade(0f, time);
        }

        cg.interactable = visible;
        cg.blocksRaycasts = visible;
    }
}