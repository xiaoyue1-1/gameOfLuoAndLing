using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LanControl : MonoBehaviour
{
    private CanvasGroup selfCG;
    public AnimationCurve speedCurve;
    // Start is called before the first frame update
    void Awake()
    {
        selfCG = GetComponent<CanvasGroup>();
        selfCG.alpha = 0f;
        selfCG.Show(false,0f);
    }
    public void Move(Vector2 vector, float speed, float speed2)
    {
        selfCG.Show(true,0f);
        selfCG.alpha = 1f;

        GetComponent<RectTransform>().DOAnchorPos(vector, speed).SetEase(speedCurve);

        if (speed2 > 0f)
        {
            DOVirtual.DelayedCall(speed2, () =>
            {
                selfCG.alpha = 0f;
                selfCG.Show(false, 0f);
            });
        }
    }
}
